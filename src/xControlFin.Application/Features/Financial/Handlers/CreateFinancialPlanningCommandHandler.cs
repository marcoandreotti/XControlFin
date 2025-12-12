using xControlFin.Application.Features.Financial.Commands;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Financial.Handlers;

public class CreateFinancialPlanningCommandHandler : ICommandHandler<CreateFinancialPlanningCommand, long>
{
    private readonly IBaseRepository<FinancialPlanningEntity> _repository;

    public CreateFinancialPlanningCommandHandler(IBaseRepository<FinancialPlanningEntity> repository)
    {
        _repository = repository;
    }

    public async Task<long> HandleAsync(CreateFinancialPlanningCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new FinancialPlanningEntity
        {
            CostCenterId = command.CostCenterId,
            FinancialInstitutionId = command.FinancialInstitutionId,
            StartDate = command.StartDate,
            LastStartDate = command.StartDate,
            EndDate = command.EndDate,
            TimeInterval = command.TimeInterval,
            Historic = command.Historic,
            Value = command.Value,
            IsActive = true,
            StartParcel = 1,
            TotalParcel = 1
        };

        var created = await _repository.AddAsync(entity, cancellationToken);
        return created.Id;
    }
}
