using xControlFin.Application.Features.Financial.Commands;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Financial.Handlers;

public class UpdateFinancialPlanningCommandHandler : ICommandHandler<UpdateFinancialPlanningCommand>
{
    private readonly IBaseRepository<FinancialPlanningEntity> _repository;

    public UpdateFinancialPlanningCommandHandler(IBaseRepository<FinancialPlanningEntity> repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateFinancialPlanningCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(command.Id, cancellationToken);
        if (entity == null)
            throw new Exception("Planejamento não encontrado.");

        entity.CostCenterId = command.CostCenterId;
        entity.FinancialInstitutionId = command.FinancialInstitutionId;
        entity.StartDate = command.StartDate;
        entity.EndDate = command.EndDate;
        entity.TimeInterval = command.TimeInterval;
        entity.Historic = command.Historic;
        entity.Value = command.Value;
        entity.StartParcel = command.StartParcel;
        entity.TotalParcel = command.TotalParcel;
        entity.IsActive = command.IsActive;

        // O requisito é atualizar o planejamento SEM alterar a propriedade LastStartDate.
        // Portanto, entity.LastStartDate não é modificado aqui.

        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
