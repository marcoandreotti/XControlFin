using System.Text.Json;

namespace xControlFin.WForm;

internal sealed class DashboardSettings
{
    public int DaysBack { get; set; } = 7;
    public int MonthsAhead { get; set; } = 2;
}

internal static class DashboardSettingsStore
{
    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "XControlFin",
        "dashboard-settings.json");

    public static DashboardSettings Load()
    {
        try
        {
            if (!File.Exists(SettingsPath))
            {
                return new DashboardSettings();
            }

            var settings = JsonSerializer.Deserialize<DashboardSettings>(
                File.ReadAllText(SettingsPath)) ?? new DashboardSettings();
            settings.DaysBack = Math.Clamp(settings.DaysBack, 0, 365);
            settings.MonthsAhead = Math.Clamp(settings.MonthsAhead, 0, 36);
            return settings;
        }
        catch
        {
            return new DashboardSettings();
        }
    }

    public static void Save(DashboardSettings settings)
    {
        var directory = Path.GetDirectoryName(SettingsPath)!;
        Directory.CreateDirectory(directory);
        File.WriteAllText(
            SettingsPath,
            JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }));
    }
}
