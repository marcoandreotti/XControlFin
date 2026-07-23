namespace xControlFin.WForm;

internal static class DatabaseFactory
{
    public static string ResolveDatabasePath()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, "xcontrolfin.db");
            if (File.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        var applicationData = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "XControlFin");
        Directory.CreateDirectory(applicationData);
        return Path.Combine(applicationData, "xcontrolfin.db");
    }
}
