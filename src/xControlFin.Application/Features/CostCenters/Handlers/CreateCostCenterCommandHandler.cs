using xControlFin.Application.Features.CostCenters.Commands;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.CostCenters.Handlers;

public class CreateCostCenterCommandHandler : ICommandHandler<CreateCostCenterCommand, long>
{
    private readonly IBaseRepository<CostCenterEntity> _repository;

    public CreateCostCenterCommandHandler(IBaseRepository<CostCenterEntity> repository)
    {
        _repository = repository;
    }

    public async Task<long> HandleAsync(CreateCostCenterCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new CostCenterEntity
        {
            Name = command.Name,
            Description = command.Description,
            Sequence = command.Sequence,
            Image = command.Image,
            IsActive = true
        };

        var created = await _repository.AddAsync(entity, cancellationToken);
        return created.Id;
    }
}
