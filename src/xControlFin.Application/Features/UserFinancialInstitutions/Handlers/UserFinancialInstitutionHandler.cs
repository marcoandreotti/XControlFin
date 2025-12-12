using xControlFin.Application.Features.UserFinancialInstitutions.Commands;
using xControlFin.Application.Features.UserFinancialInstitutions.Queries;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.UserFinancialInstitutions.Handlers;

public class UserFinancialInstitutionHandler :
    ICommandHandler<CreateUserFinancialInstitutionCommand, long>,
    ICommandHandler<DeleteUserFinancialInstitutionCommand>,
    IQueryHandler<GetFinancialInstitutionsByUserIdQuery, List<long>>
{
    private readonly IBaseRepository<UserFinancialInstitutionEntity> _repository;

    public UserFinancialInstitutionHandler(IBaseRepository<UserFinancialInstitutionEntity> repository)
    {
        _repository = repository;
    }

    public async Task<long> HandleAsync(CreateUserFinancialInstitutionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new UserFinancialInstitutionEntity
        {
            UserId = command.UserId,
            FinancialInstitutionId = command.FinancialInstitutionId
        };
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public async Task HandleAsync(DeleteUserFinancialInstitutionCommand command, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(command.Id, cancellationToken);
    }

    public async Task<List<long>> HandleAsync(GetFinancialInstitutionsByUserIdQuery query, CancellationToken cancellationToken = default)
    {
        var links = await _repository.GetAllAsync(cancellationToken);
        return links.Where(x => x.UserId == query.UserId).Select(x => x.FinancialInstitutionId).ToList();
    }
}
