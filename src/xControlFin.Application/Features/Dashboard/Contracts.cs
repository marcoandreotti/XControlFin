namespace xControlFin.Application.Features.Dashboard.Dtos;

public sealed class DashboardDto
{
    public List<AccountBalanceDto> Accounts { get; init; } = [];
    public List<DashboardReleaseDto> Releases { get; init; } = [];
    public decimal RealizedTotal => Accounts.Sum(account => account.RealizedBalance);
    public decimal PlannedTotal => Accounts.Sum(account => account.PlannedBalance);
    public decimal GrandTotal => Accounts.Sum(account => account.TotalBalance);
}

public sealed record AccountBalanceDto(
    long InstitutionId,
    string InstitutionName,
    decimal RealizedBalance,
    decimal PlannedBalance)
{
    public decimal TotalBalance => RealizedBalance + PlannedBalance;
}

public sealed record DashboardReleaseDto(
    long? ReleaseId,
    long? PlanningId,
    long InstitutionId,
    string Institution,
    string CostCenter,
    DateTime PaymentDate,
    DateTime ScheduledDate,
    string Historic,
    decimal Value,
    bool Realized,
    bool Projected)
{
    public string Status => Realized ? "Realizado" : "Previsto";
}
