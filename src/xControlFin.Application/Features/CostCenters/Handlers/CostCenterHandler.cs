using xControlFin.Application.Features.CostCenters.Commands;
using xControlFin.Application.Features.CostCenters.Queries;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.CostCenters.Handlers;

public class CostCenterHandler :
    ICommandHandler<UpdateCostCenterCommand>,
    ICommandHandler<DeleteCostCenterCommand>,
    IQueryHandler<GetCostCenterByIdQuery, CostCenterEntity?>,
    IQueryHandler<GetAllCostCentersQuery, List<CostCenterEntity>>
{
    private readonly IBaseRepository<CostCenterEntity> _repository;

    public CostCenterHandler(IBaseRepository<CostCenterEntity> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateCostCenterCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity != null)
        {
            entity.Name = command.Name;
            entity.Description = command.Description;
            entity.Sequence = command.Sequence;
            entity.Image = command.Image;
            entity.IsActive = command.IsActive;
            await _repository.UpdateAsync(entity, cancellationToken);
        }
    }

    public async Task HandleAsync(DeleteCostCenterCommand command, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(command.Id, cancellationToken);
    }

    public async Task<CostCenterEntity?> HandleAsync(GetCostCenterByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(query.Id, cancellationToken);
    }

    public async Task<List<CostCenterEntity>> HandleAsync(GetAllCostCentersQuery query, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}