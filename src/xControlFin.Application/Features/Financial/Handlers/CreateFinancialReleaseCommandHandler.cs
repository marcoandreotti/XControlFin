using xControlFin.Application.Features.Financial.Commands;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Financial.Handlers;

public class CreateFinancialReleaseCommandHandler : ICommandHandler<CreateFinancialReleaseCommand, long>
{
    private readonly IBaseRepository<FinancialReleaseEntity> _repository;

    public CreateFinancialReleaseCommandHandler(IBaseRepository<FinancialReleaseEntity> repository)
    {
        _repository = repository;
    }

    public async Task<long> HandleAsync(CreateFinancialReleaseCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new FinancialReleaseEntity
        {
            CostCenterId = command.CostCenterId,
            FinancialInstitutionId = command.FinancialInstitutionId,
            FinancialPlanningId = command.FinancialPlanningId,
            PaymentDate = command.PaymentDate,
            CompensationDate = command.PaymentDate, // Assumindo mesmo dia se não informado
            Historic = command.Historic,
            Value = command.Value,
            Realized = command.Realized,
            Parcel = 1,
            TotalParcel = 1
        };

        var created = await _repository.AddAsync(entity, cancellationToken);
        return created.Id;
    }
}
