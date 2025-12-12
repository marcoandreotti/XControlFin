using xControlFin.Domain.Entities;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.CostCenters.Queries;

public class GetCostCenterByIdQuery : IQuery<CostCenterEntity?>
{
    public long Id { get; set; }
}

public class GetAllCostCentersQuery : IQuery<List<CostCenterEntity>>
{
}