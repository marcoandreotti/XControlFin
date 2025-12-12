using xControlFin.Domain.Entities.Base;

namespace xControlFin.Domain.Entities;

public class CostCenterEntity : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int Sequence { get; set; } = 1;
    public string? Image { get; set; }
    public bool IsActive { get; set; } = true;
}
