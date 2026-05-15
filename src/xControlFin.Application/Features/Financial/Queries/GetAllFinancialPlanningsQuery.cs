using xControlFin.Domain.Entities;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Financial.Queries;

public class GetAllFinancialPlanningsQuery : IQuery<IEnumerable<FinancialPlanningEntity>>
{
}

public class GetAllFinancialReleasesCrudQuery : IQuery<IEnumerable<FinancialReleaseEntity>>
{
}
