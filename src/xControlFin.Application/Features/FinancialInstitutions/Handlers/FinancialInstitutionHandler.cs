using xControlFin.Application.Features.FinancialInstitutions.Commands;
using xControlFin.Application.Features.FinancialInstitutions.Queries;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.FinancialInstitutions.Handlers;

public class FinancialInstitutionHandler :
    ICommandHandler<UpdateFinancialInstitutionCommand>,
    ICommandHandler<DeleteFinancialInstitutionCommand>,
    IQueryHandler<GetFinancialInstitutionByIdQuery, FinancialInstitutionEntity?>,
    IQueryHandler<GetAllFinancialInstitutionsQuery, List<FinancialInstitutionEntity>>
{
    private readonly IBaseRepository<FinancialInstitutionEntity> _repository;

    public FinancialInstitutionHandler(IBaseRepository<FinancialInstitutionEntity> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateFinancialInstitutionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity != null)
        {
            entity.Name = command.Name;
            entity.Description = command.Description;
            entity.Sequence = command.Sequence;
            entity.IsActive = command.IsActive;
            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }

    public async Task HandleAsync(DeleteFinancialInstitutionCommand command, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(command.Id, cancellationToken);
    }

    public async Task<FinancialInstitutionEntity?> HandleAsync(GetFinancialInstitutionByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(query.Id, cancellationToken);
    }

    public async Task<List<FinancialInstitutionEntity>> HandleAsync(GetAllFinancialInstitutionsQuery query, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}