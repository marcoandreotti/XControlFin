using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Financial.Commands;

public class UpdateFinancialReleaseCommand : ICommand
{
    public long Id { get; set; }
    public long CostCenterId { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Historic { get; set; }
    public decimal Value { get; set; }
    public bool Realized { get; set; }
}

public class DeleteFinancialReleaseCommand : ICommand
{
    public long Id { get; set; }
}

public class DeleteFinancialPlanningCommand : ICommand
{
    public long Id { get; set; }
}