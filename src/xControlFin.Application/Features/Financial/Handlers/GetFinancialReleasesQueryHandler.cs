using xControlFin.Application.Features.Financial.Dtos;
using xControlFin.Application.Features.Financial.Queries;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Enums;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Financial.Handlers;

public class GetFinancialReleasesQueryHandler : IQueryHandler<GetFinancialReleasesQuery, List<FinancialCheckDto>>
{
    private readonly IFinancialRepository _repository;

    public GetFinancialReleasesQueryHandler(IFinancialRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<FinancialCheckDto>> HandleAsync(GetFinancialReleasesQuery query, CancellationToken cancellationToken = default)
    {
        var result = new List<FinancialCheckDto>();

        // 1. Buscar realizados no período
        var realized = await _repository.GetRealizedReleasesAsync(query.FinancialInstitutionId, query.StartDate, query.EndDate, cancellationToken);

        result.AddRange(realized.Select(r => new FinancialCheckDto
        {
            Id = r.Id,
            CostCenterId = r.CostCenterId,
            FinancialInstitutionId = r.FinancialInstitutionId,
            PaymentDate = r.PaymentDate,
            Historic = r.Historic,
            Value = r.Value,
            Realized = r.Realized,
            IsPlanned = false
        }));

        // 2. Buscar planejamentos ativos
        var plannings = await _repository.GetPlannedReleasesAsync(query.FinancialInstitutionId, cancellationToken);

        // 3. Projetar lançamentos futuros
        foreach (var plan in plannings)
        {
            var projectedDates = GenerateDates(plan, query.StartDate, query.EndDate);

            foreach (var date in projectedDates)
            {
                // Verifica se já existe um realizado vinculado a este planejamento nesta data (lógica simplificada)
                // Na prática, precisaria verificar se já existe um FinancialRelease com FinancialPlanningId == plan.Id e data próxima.
                // Assumindo que o repositório GetRealized já trouxe, podemos checar em memória.
                bool alreadyRealized = realized.Any(r => r.FinancialPlanningId == plan.Id && r.PaymentDate.Date == date.Date);

                if (!alreadyRealized)
                {
                    result.Add(new FinancialCheckDto
                    {
                        Id = null,
                        CostCenterId = plan.CostCenterId,
                        FinancialInstitutionId = plan.FinancialInstitutionId,
                        PaymentDate = date,
                        Historic = $"{plan.Historic} (Previsto)",
                        Value = plan.Value,
                        Realized = false,
                        IsPlanned = true,
                        OriginPlanningId = plan.Id
                    });
                }
            }
        }

        return result.OrderBy(x => x.PaymentDate).ToList();
    }

    private IEnumerable<DateTime> GenerateDates(FinancialPlanningEntity plan, DateTime filterStart, DateTime filterEnd)
    {
        var currentDate = plan.StartDate; // Começa da data de início do plano

        // Se o plano já tem uma data de "último lançamento gerado", poderíamos usar, mas aqui é projeção lógica.
        // Vamos iterar a partir do StartDate do plano até passar do filterEnd.

        while (currentDate <= filterEnd)
        {
            if (currentDate >= filterStart)
            {
                if (!plan.EndDate.HasValue || currentDate <= plan.EndDate.Value)
                {
                    yield return currentDate;
                }
            }

            // Incrementa conforme periodicidade
            currentDate = plan.TimeInterval switch
            {
                TimeIntervalEnum.Daily => currentDate.AddDays(1),
                TimeIntervalEnum.Weekly => currentDate.AddDays(7),
                TimeIntervalEnum.Monthly => currentDate.AddMonths(1),
                TimeIntervalEnum.Yearly => currentDate.AddYears(1),
                _ => currentDate.AddMonths(1)
            };

            if (currentDate > filterEnd && (plan.EndDate.HasValue && currentDate > plan.EndDate.Value))
                break;
        }
    }
}
