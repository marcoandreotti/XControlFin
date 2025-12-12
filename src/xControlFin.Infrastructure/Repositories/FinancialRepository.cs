using Microsoft.EntityFrameworkCore;
using xControlFin.Domain.Entities;
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

    public async Task<List<FinancialPlanningEntity>> GetPlannedReleasesAsync(long financialInstitutionId, CancellationToken cancellationToken)
    {
        // Pega planejamentos ativos que podem gerar lançamentos
        return await _context.FinancialPlannings
            .Where(x => x.FinancialInstitutionId == financialInstitutionId && x.IsActive)
            .ToListAsync(cancellationToken);
    }
}
