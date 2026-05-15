using xControlFin.Application.Features.Financial.Queries;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Financial.Handlers;

public class GetAllFinancialPlanningsQueryHandler : IQueryHandler<GetAllFinancialPlanningsQuery, IEnumerable<FinancialPlanningEntity>>
{
    private readonly IBaseRepository<FinancialPlanningEntity> _repository;

    public GetAllFinancialPlanningsQueryHandler(IBaseRepository<FinancialPlanningEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FinancialPlanningEntity>> HandleAsync(GetAllFinancialPlanningsQuery query, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
