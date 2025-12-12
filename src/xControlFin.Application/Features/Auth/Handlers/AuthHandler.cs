using Microsoft.Extensions.Options;
using xControlFin.Application.Features.Auth.Commands;
using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Crosscutting.Common.Security;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;
using System.Security.Claims;

namespace xControlFin.Application.Features.Auth.Handlers;

public class AuthHandler :
    ICommandHandler<LoginCommand, AuthResponseDto>,
    ICommandHandler<RefreshTokenCommand, AuthResponseDto>
{
    private readonly IBaseRepository<UserEntity> _userRepo;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordManager _passwordManager;
    private readonly JwtSettings _jwtSettings;

    public AuthHandler(IBaseRepository<UserEntity> userRepo, ITokenProvider tokenProvider, IPasswordManager passwordManager, IOptions<JwtSettings> jwtSettings)
    {
        _userRepo = userRepo;
        _tokenProvider = tokenProvider;
        _passwordManager = passwordManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponseDto> HandleAsync(LoginCommand command, CancellationToken cancellationToken = default)
    {
        var users = await _userRepo.GetAllAsync(cancellationToken);
        var user = users.FirstOrDefault(u => u.Email == command.Email);

        if (user == null || !_passwordManager.VerifyPassword(command.Password, user.Password))
            throw new Exception("Invalid credentials");

        var accessToken = _tokenProvider.GenerateAccessToken(user.Id, user.Email, user.Name);
        var refreshToken = _tokenProvider.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);
        await _userRepo.UpdateAsync(user, cancellationToken);

        return new AuthResponseDto { AccessToken = accessToken, RefreshToken = refreshToken };
    }

    public async Task<AuthResponseDto> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken = default)
    {
        var principal = _tokenProvider.GetPrincipalFromExpiredToken(command.AccessToken);
        if (principal == null) throw new Exception("Invalid token");

        var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out long userId))
            throw new Exception("Invalid token claims");

        var user = await _userRepo.GetByIdAsync(userId, cancellationToken);
        if (user == null || user.RefreshToken != command.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new Exception("Invalid refresh token");

        var newAccessToken = _tokenProvider.GenerateAccessToken(user.Id, user.Email, user.Name);
        var newRefreshToken = _tokenProvider.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);
        await _userRepo.UpdateAsync(user, cancellationToken);

        return new AuthResponseDto { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
    }
}