namespace xControlFin.Application.Features.Financial.Dtos;

public class FinancialCheckDto
{
    public long? Id { get; set; } // Null se for planejado (não persistido como release)
    public long CostCenterId { get; set; }
    public long FinancialInstitutionId { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Historic { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public bool Realized { get; set; }
    public bool IsPlanned { get; set; } // Indica se é uma projeção
    public long? OriginPlanningId { get; set; }
}
