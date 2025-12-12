using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Financial.Commands;

public class CreateFinancialReleaseCommand : ICommand<long>
{
    public required long CostCenterId { get; set; }
    public required long FinancialInstitutionId { get; set; }
    public long? FinancialPlanningId { get; set; }
    public DateTime PaymentDate { get; set; }
    public required string Historic { get; set; }
    public decimal Value { get; set; }
    public bool Realized { get; set; } = false;
}
