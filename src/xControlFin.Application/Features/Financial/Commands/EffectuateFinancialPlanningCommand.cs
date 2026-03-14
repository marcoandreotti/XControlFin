using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Financial.Commands;

public class EffectuateFinancialPlanningCommand : ICommand<long>
{
    public long FinancialPlanningId { get; set; }
    
    /// <summary>
    /// Data opcional do pagamento/compensação da despesa/receita.
    /// Se não for informada, será usada a data de início atual (StartDate) do planejamento.
    /// </summary>
    public DateTime? PaymentDate { get; set; }
    
    /// <summary>
    /// Valor opcional caso o valor efetivado seja diferente do planejado.
    /// Se não informado, utiliza o valor do planejamento (Value).
    /// </summary>
    public decimal? OverrideValue { get; set; }
    
    /// <summary>
    /// Histórico opcional caso seja diferente do planejado.
    /// </summary>
    public string? OverrideHistoric { get; set; }
}
