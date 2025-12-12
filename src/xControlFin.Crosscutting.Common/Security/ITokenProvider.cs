using System.Security.Claims;

namespace xControlFin.Crosscutting.Common.Security;

public interface ITokenProvider
{
    string GenerateAccessToken(long userId, string email, string name);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
