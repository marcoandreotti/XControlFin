using xControlFin.Domain.Entities.Base;
using xControlFin.Domain.Enums;

namespace xControlFin.Domain.Entities;

/// <summary>
/// Financial Planning Entity -> lançamentos recorrentes configuráveis
/// TimeIntervalEnum -> enumeração para definir o intervalo de tempo dos lançamentos (diário, semanal, mensal, anual)
/// Ex: O sistema através de um filtro de data e conta corrente, gera logicamente os lançamentos financeiros conforme o planejamento definido
/// </summary>
public class FinancialPlanningEntity : BaseEntity
{
    public required long CostCenterId { get; set; }
    public required long FinancialInstitutionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime LastStartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public TimeIntervalEnum TimeInterval { get; set; } = TimeIntervalEnum.Monthly;
    public int InitialPaymentDay { get; set; }
    public required string Historic { get; set; }
    public int StartParcel { get; set; }
    public int TotalParcel { get; set; }
    public long? Grouper { get; set; }
    public decimal Value { get; set; }
    public bool IsActive { get; set; } = false;
}
