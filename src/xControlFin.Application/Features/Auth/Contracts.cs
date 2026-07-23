namespace xControlFin.Application.Features.Auth.Dtos;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

public sealed record LoginUserDto(long Id, string Name, string Email)
{
    public override string ToString() => $"{Name} ({Email})";
}

public sealed record LocalUserSessionDto(
    long UserId,
    string Name,
    string Email,
    string? Image);
