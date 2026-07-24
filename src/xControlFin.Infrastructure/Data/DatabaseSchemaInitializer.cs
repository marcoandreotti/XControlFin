using Microsoft.EntityFrameworkCore;
using System.Data;

namespace xControlFin.Infrastructure.Data;

public static class DatabaseSchemaInitializer
{
    public static async Task EnsureCompatibilityAsync(
        XControlFinDbContext context,
        CancellationToken cancellationToken = default)
    {
        await context.Database.EnsureCreatedAsync(cancellationToken);

        var provider = context.Database.ProviderName ?? string.Empty;
        if (provider.Contains("Sqlite", StringComparison.OrdinalIgnoreCase))
        {
            await EnsureSqliteScheduledDateAsync(context, cancellationToken);
        }
        else if (provider.Contains("Npgsql", StringComparison.OrdinalIgnoreCase))
        {
            await context.Database.ExecuteSqlRawAsync(
                """
                ALTER TABLE "FinancialReleases"
                ADD COLUMN IF NOT EXISTS "ScheduledDate" timestamp NULL;
                """,
                cancellationToken);
        }
        else if (provider.Contains("Jet", StringComparison.OrdinalIgnoreCase))
        {
            await EnsureJetScheduledDateAsync(context, cancellationToken);
        }
    }

    private static async Task EnsureSqliteScheduledDateAsync(
        XControlFinDbContext context,
        CancellationToken cancellationToken)
    {
        var connection = context.Database.GetDbConnection();
        var shouldClose = connection.State != ConnectionState.Open;
        if (shouldClose)
            await connection.OpenAsync(cancellationToken);

        try
        {
            await using var checkCommand = connection.CreateCommand();
            checkCommand.CommandText = "PRAGMA table_info('FinancialReleases');";
            await using var reader = await checkCommand.ExecuteReaderAsync(cancellationToken);
            var exists = false;
            while (await reader.ReadAsync(cancellationToken))
            {
                if (string.Equals(
                        reader.GetString(1),
                        "ScheduledDate",
                        StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            await reader.CloseAsync();
            if (exists)
                return;

            await using var alterCommand = connection.CreateCommand();
            alterCommand.CommandText =
                "ALTER TABLE FinancialReleases ADD COLUMN ScheduledDate TEXT NULL;";
            await alterCommand.ExecuteNonQueryAsync(cancellationToken);
        }
        finally
        {
            if (shouldClose)
                await connection.CloseAsync();
        }
    }

    private static async Task EnsureJetScheduledDateAsync(
        XControlFinDbContext context,
        CancellationToken cancellationToken)
    {
        var connection = context.Database.GetDbConnection();
        var shouldClose = connection.State != ConnectionState.Open;
        if (shouldClose)
            await connection.OpenAsync(cancellationToken);

        try
        {
            var columns = connection.GetSchema(
                "Columns",
                [null, null, "FinancialReleases", "ScheduledDate"]);
            if (columns.Rows.Count > 0)
                return;

            await using var command = connection.CreateCommand();
            command.CommandText =
                "ALTER TABLE [FinancialReleases] ADD COLUMN [ScheduledDate] DATETIME NULL;";
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
        finally
        {
            if (shouldClose)
                await connection.CloseAsync();
        }
    }
}
