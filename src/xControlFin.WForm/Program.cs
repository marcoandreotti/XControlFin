using Microsoft.Extensions.DependencyInjection;
using xControlFin.Infrastructure.Data;
using xControlFin.Shared.Abstractions;

namespace xControlFin.WForm;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        GlobalExceptionHandler.Register();

        try
        {
            using var serviceProvider = DesktopServiceConfiguration.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<XControlFinDbContext>();
            DatabaseInitializer.Initialize(context);
            var dispatcher = scope.ServiceProvider.GetRequiredService<IDispatcher>();

            using var loginForm = new frmLogin(dispatcher);
            if (loginForm.ShowDialog() == DialogResult.OK && loginForm.AuthenticatedUser is not null)
            {
                System.Windows.Forms.Application.Run(new frmPrincipal(loginForm.AuthenticatedUser));
            }
        }
        catch (Exception exception)
        {
            GlobalExceptionHandler.Handle(exception, "inicialização do aplicativo");
        }
    }
}
