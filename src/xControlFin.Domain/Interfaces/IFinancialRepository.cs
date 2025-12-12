using xControlFin.Domain.Entities;

namespace xControlFin.Domain.Interfaces;

public interface IFinancialRepository
{
    Task<List<FinancialReleaseEntity>> GetRealizedReleasesAsync(long financialInstitutionId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    Task<List<FinancialPlanningEntity>> GetPlannedReleasesAsync(long financialInstitutionId, CancellationToken cancellationToken);
}
