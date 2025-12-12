using xControlFin.Domain.Entities.Base;

namespace xControlFin.Domain.Entities;

public class FinancialInstitutionEntity : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int Sequence { get; set; } = 1;
    public bool IsActive { get; set; } = true;
}

public class UserFinancialInstitutionEntity : BaseEntity
{
    public required long UserId { get; set; }
    public required long FinancialInstitutionId { get; set; }
}
