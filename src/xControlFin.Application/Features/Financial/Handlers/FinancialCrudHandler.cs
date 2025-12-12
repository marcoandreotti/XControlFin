using xControlFin.Application.Features.Financial.Commands;
using xControlFin.Application.Features.Financial.Queries;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Financial.Handlers;

public class FinancialCrudHandler :
    ICommandHandler<UpdateFinancialReleaseCommand>,
    ICommandHandler<DeleteFinancialReleaseCommand>,
    ICommandHandler<DeleteFinancialPlanningCommand>,
    IQueryHandler<GetFinancialReleaseByIdQuery, FinancialReleaseEntity?>
{
    private readonly IBaseRepository<FinancialReleaseEntity> _releaseRepo;
    private readonly IBaseRepository<FinancialPlanningEntity> _planningRepo;

    public FinancialCrudHandler(IBaseRepository<FinancialReleaseEntity> releaseRepo, IBaseRepository<FinancialPlanningEntity> planningRepo)
    {
        _releaseRepo = releaseRepo;
        _planningRepo = planningRepo;
    }

    public async Task HandleAsync(UpdateFinancialReleaseCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _releaseRepo.GetByIdAsync(command.Id, cancellationToken);
        if (entity != null)
        {
            entity.CostCenterId = command.CostCenterId;
            entity.PaymentDate = command.PaymentDate;
            entity.CompensationDate = command.PaymentDate;
            entity.Historic = command.Historic;
            entity.Value = command.Value;
            entity.Realized = command.Realized;
            await _releaseRepo.UpdateAsync(entity, cancellationToken);
        }
    }

    public async Task HandleAsync(DeleteFinancialReleaseCommand command, CancellationToken cancellationToken = default)
    {
        await _releaseRepo.DeleteAsync(command.Id, cancellationToken);
    }

    public async Task HandleAsync(DeleteFinancialPlanningCommand command, CancellationToken cancellationToken = default)
    {
        // Logic delete? Or physical? Standard physical for now as requested.
        await _planningRepo.DeleteAsync(command.Id, cancellationToken);
    }

    public async Task<FinancialReleaseEntity?> HandleAsync(GetFinancialReleaseByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await _releaseRepo.GetByIdAsync(query.Id, cancellationToken);
    }
}