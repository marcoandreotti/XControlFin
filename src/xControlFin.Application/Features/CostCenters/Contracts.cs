using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.CostCenters.Commands;

public class UpdateCostCenterCommand : ICommand
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Sequence { get; set; }
    public string? Image { get; set; }
    public bool IsActive { get; set; }
}

public class DeleteCostCenterCommand : ICommand
{
    public long Id { get; set; }
}