namespace xControlFin.Crosscutting.Common.Security;

public interface IPasswordManager
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}
