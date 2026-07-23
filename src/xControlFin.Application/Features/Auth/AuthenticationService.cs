using xControlFin.Crosscutting.Common.Security;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;

namespace xControlFin.Application.Features.Auth;

public interface ICredentialAuthenticationService
{
    Task<UserEntity?> AuthenticateByEmailAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);

    Task<UserEntity?> AuthenticateByIdAsync(
        long userId,
        string password,
        CancellationToken cancellationToken = default);
}

public sealed class CredentialAuthenticationService(
    IBaseRepository<UserEntity> userRepository,
    IPasswordManager passwordManager) : ICredentialAuthenticationService
{
    public async Task<UserEntity?> AuthenticateByEmailAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var users = await userRepository.GetAllAsync(cancellationToken);
        var user = users.FirstOrDefault(item =>
            item.Active &&
            string.Equals(item.Email, email.Trim(), StringComparison.OrdinalIgnoreCase));

        return IsPasswordValid(user, password) ? user : null;
    }

    public async Task<UserEntity?> AuthenticateByIdAsync(
        long userId,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken);
        return user is { Active: true } && IsPasswordValid(user, password) ? user : null;
    }

    private bool IsPasswordValid(UserEntity? user, string password)
    {
        return user is not null &&
               !string.IsNullOrWhiteSpace(password) &&
               passwordManager.VerifyPassword(password, user.Password);
    }
}
