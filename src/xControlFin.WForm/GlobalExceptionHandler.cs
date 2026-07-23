using System.Diagnostics;

namespace xControlFin.WForm;

internal static class GlobalExceptionHandler
{
    private static readonly string LogDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "XControlFin",
        "Logs");

    public static void Register()
    {
        System.Windows.Forms.Application.ThreadException += (_, args) =>
            Handle(args.Exception, "interface");
        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            Handle(args.ExceptionObject as Exception ?? new Exception("Erro não gerenciado."), "aplicativo");
        TaskScheduler.UnobservedTaskException += (_, args) =>
        {
            Handle(args.Exception, "tarefa em segundo plano");
            args.SetObserved();
        };
    }

    public static void Handle(Exception exception, string operation)
    {
        try
        {
            Directory.CreateDirectory(LogDirectory);
            var logPath = Path.Combine(LogDirectory, $"xcontrolfin-{DateTime.Now:yyyy-MM-dd}.log");
            File.AppendAllText(
                logPath,
                $"[{DateTime.Now:O}] Erro durante {operation}.{Environment.NewLine}{exception}{Environment.NewLine}{Environment.NewLine}");
        }
        catch (Exception logException)
        {
            Debug.WriteLine(logException);
        }

        MessageBox.Show(
            $"Não foi possível concluir a {operation}.\n\n" +
            "O problema foi registrado. Se ele persistir, contate o suporte.",
            "XControlFin",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}
