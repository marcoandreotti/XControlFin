using xControlFin.Crosscutting.Common.Security;
using xControlFin.Domain.Entities;
using xControlFin.Infrastructure.Data;

namespace xControlFin.WForm;

internal static class DatabaseInitializer
{
    public static void Initialize(XControlFinDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return;
        }

        var passwordManager = new PasswordManager();
        context.Users.Add(new UserEntity
        {
            Name = "Administrador",
            Email = "admin@xcontrol.com",
            Password = passwordManager.HashPassword("admin123"),
            Active = true
        });
        context.SaveChanges();
    }
}
