using xControlFin.Domain.Entities.Base;

namespace xControlFin.Domain.Entities;

/// <summary>
/// Financial Release Entity -> lançamentos financeiros individuais
/// Ex: Lançamento de uma despesa ou receita única, sem recorrência
/// Aqui poderão ser salvas a compensações de lançamentos recorrentes (Financial Planning)
/// </summary>
public class FinancialReleaseEntity : BaseEntity
{
    public required long CostCenterId { get; set; }
    public required long FinancialInstitutionId { get; set; }
    public long? FinancialPlanningId { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime CompensationDate { get; set; }
    public required string Historic { get; set; }
    public int Parcel { get; set; }
    public int TotalParcel { get; set; }
    public long? Grouper { get; set; }
    public decimal Value { get; set; }
    public bool Realized { get; set; } = false;
}
