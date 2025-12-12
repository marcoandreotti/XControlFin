namespace xControlFin.Domain.Entities.Base;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Updated { get; set; }
}
