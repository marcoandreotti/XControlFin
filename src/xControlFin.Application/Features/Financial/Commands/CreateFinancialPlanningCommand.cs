using xControlFin.Domain.Enums;
using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Financial.Commands;

public class CreateFinancialPlanningCommand : ICommand<long>
{
    public required long CostCenterId { get; set; }
    public required long FinancialInstitutionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeIntervalEnum TimeInterval { get; set; }
    public required string Historic { get; set; }
    public decimal Value { get; set; }
}
