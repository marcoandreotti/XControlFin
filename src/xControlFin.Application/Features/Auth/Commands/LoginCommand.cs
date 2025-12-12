using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Auth.Commands;

public class LoginCommand : ICommand<AuthResponseDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RefreshTokenCommand : ICommand<AuthResponseDto>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}