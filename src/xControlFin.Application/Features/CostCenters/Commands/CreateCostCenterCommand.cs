using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.CostCenters.Commands;

public class CreateCostCenterCommand : ICommand<long>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int Sequence { get; set; } = 1;
    public string? Image { get; set; }
}
