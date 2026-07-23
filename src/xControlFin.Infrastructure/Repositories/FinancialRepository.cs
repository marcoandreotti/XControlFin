using Microsoft.EntityFrameworkCore;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Enums;
using xControlFin.Domain.Interfaces;
using xControlFin.Infrastructure.Data;

namespace xControlFin.Infrastructure.Repositories;

public class FinancialRepository : IFinancialRepository
{
    private readonly XControlFinDbContext _context;

    public FinancialRepository(XControlFinDbContext context)
    {
        _context = context;
    }

    public async Task<List<FinancialReleaseEntity>> GetRealizedReleasesAsync(long financialInstitutionId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        return await _context.FinancialReleases
            .Where(x => x.FinancialInstitutionId == financialInstitutionId &&
                        x.PaymentDate >= startDate &&
                        x.PaymentDate <= endDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> SumPreviousBalancesRealizedAsync(long financialInstitutionId, DateTime startDate, CancellationToken cancellationToken)
    {
        return await _context.FinancialReleases
            .Where(x => x.FinancialInstitutionId == financialInstitutionId &&
                        x.PaymentDate <= startDate)
            .Select(x => x.Value)
            .SumAsync(cancellationToken);
    }

    public async Task<List<FinancialPlanningEntity>> GetPlannedReleasesAsync(long financialInstitutionId, CancellationToken cancellationToken)
    {
        // Pega planejamentos ativos que podem gerar lançamentos
        return await _context.FinancialPlannings
            .Where(x => x.FinancialInstitutionId == financialInstitutionId &&
                        x.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> SumPreviousBalancesPlannedAsync(long financialInstitutionId, DateTime startDate, CancellationToken cancellationToken)
    {
        // Pega planejamentos ativos que podem gerar lançamentos
        return await _context.FinancialPlannings
            .Where(x => x.FinancialInstitutionId == financialInstitutionId &&
                        x.IsActive &&
                        x.StartDate < startDate)
            .Select(x => x.Value)
            .SumAsync(cancellationToken);
    }

    public async Task<decimal> SumPreviousBalancesPlannedWithIntervalsAsync(long financialInstitutionId, DateTime startDate, CancellationToken cancellationToken)
    {
        var plannings = await _context.FinancialPlannings
            .Where(x => x.FinancialInstitutionId == financialInstitutionId && x.IsActive)
            .ToListAsync(cancellationToken);

        var realizedReleases = await _context.FinancialReleases
            .Where(x => x.FinancialInstitutionId == financialInstitutionId &&
                        x.FinancialPlanningId != null &&
                        x.PaymentDate < startDate)
            .ToListAsync(cancellationToken);

        decimal totalSum = 0;

        foreach (var plan in plannings)
        {
            var currentDate = plan.StartDate;

            while (currentDate < startDate)
            {
                if (plan.EndDate.HasValue && currentDate > plan.EndDate.Value)
                {
                    break;
                }

                bool alreadyRealized = realizedReleases.Any(r => r.FinancialPlanningId == plan.Id && r.PaymentDate.Date == currentDate.Date);
                if (!alreadyRealized)
                {
                    totalSum += plan.Value;
                }

                currentDate = plan.TimeInterval switch
                {
                    TimeIntervalEnum.Daily => currentDate.AddDays(1),
                    TimeIntervalEnum.Weekly => currentDate.AddDays(7),
                    TimeIntervalEnum.Monthly => currentDate.AddMonths(1),
                    TimeIntervalEnum.Yearly => currentDate.AddYears(1),
                    _ => currentDate.AddMonths(1)
                };
            }
        }

        return totalSum;
    }
}