using xControlFin.Application.Features.Financial.Queries;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Financial.Handlers;

public class GetAllFinancialReleasesCrudQueryHandler : IQueryHandler<GetAllFinancialReleasesCrudQuery, IEnumerable<FinancialReleaseEntity>>
{
    private readonly IBaseRepository<FinancialReleaseEntity> _repository;

    public GetAllFinancialReleasesCrudQueryHandler(IBaseRepository<FinancialReleaseEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FinancialReleaseEntity>> HandleAsync(GetAllFinancialReleasesCrudQuery query, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
