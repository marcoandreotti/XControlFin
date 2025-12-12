using xControlFin.Application.Features.FinancialInstitutions.Commands;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.FinancialInstitutions.Handlers;

public class CreateFinancialInstitutionCommandHandler : ICommandHandler<CreateFinancialInstitutionCommand, long>
{
    private readonly IBaseRepository<FinancialInstitutionEntity> _repository;

    public CreateFinancialInstitutionCommandHandler(IBaseRepository<FinancialInstitutionEntity> repository)
    {
        _repository = repository;
    }

    public async Task<long> HandleAsync(CreateFinancialInstitutionCommand command, CancellationToken cancellationToken = default)
    {
        var entity = new FinancialInstitutionEntity
        {
            Name = command.Name,
            Description = command.Description,
            Sequence = command.Sequence,
            IsActive = true
        };

        var created = await _repository.AddAsync(entity, cancellationToken);
        return created.Id;
    }
}
