using xControlFin.Application.Features.Dashboard.Commands;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Dashboard.Handlers;

public sealed class DashboardMovementHandler(
    IBaseRepository<FinancialReleaseEntity> releaseRepository,
    IBaseRepository<FinancialPlanningEntity> planningRepository,
    IUnitOfWork unitOfWork) :
    ICommandHandler<EffectuateDashboardMovementsCommand>,
    ICommandHandler<ChangeDashboardMovementDatesCommand>,
    ICommandHandler<ReverseDashboardMovementsCommand>
{
    public Task HandleAsync(
        EffectuateDashboardMovementsCommand command,
        CancellationToken cancellationToken = default)
    {
        EnsureSelection(command.Items);
        return unitOfWork.ExecuteInTransactionAsync(async token =>
        {
            foreach (var item in command.Items)
            {
                if (item.ReleaseId.HasValue)
                {
                    var release = await GetReleaseAsync(item.ReleaseId.Value, token);
                    if (release.Realized)
                        throw new InvalidOperationException($"O lançamento '{release.Historic}' já está realizado.");

                    release.PaymentDate = command.EffectiveDate.Date;
                    release.CompensationDate = command.EffectiveDate.Date;
                    release.Realized = true;
                    await releaseRepository.UpdateAsync(release, token);
                    continue;
                }

                var planning = await GetPlanningAsync(item.PlanningId, token);
                await EnsureOccurrenceDoesNotExistAsync(planning.Id, item.ScheduledDate, token);
                await releaseRepository.AddAsync(
                    CreateRelease(planning, item.ScheduledDate, command.EffectiveDate, true), token);
            }
        }, cancellationToken);
    }

    public Task HandleAsync(
        ChangeDashboardMovementDatesCommand command,
        CancellationToken cancellationToken = default)
    {
        EnsureSelection(command.Items);
        return unitOfWork.ExecuteInTransactionAsync(async token =>
        {
            foreach (var item in command.Items)
            {
                if (item.ReleaseId.HasValue)
                {
                    var release = await GetReleaseAsync(item.ReleaseId.Value, token);
                    release.PaymentDate = command.NewDate.Date;
                    if (release.Realized)
                        release.CompensationDate = command.NewDate.Date;
                    await releaseRepository.UpdateAsync(release, token);
                    continue;
                }

                var planning = await GetPlanningAsync(item.PlanningId, token);
                await EnsureOccurrenceDoesNotExistAsync(planning.Id, item.ScheduledDate, token);
                await releaseRepository.AddAsync(
                    CreateRelease(planning, item.ScheduledDate, command.NewDate, false), token);
            }
        }, cancellationToken);
    }

    public Task HandleAsync(
        ReverseDashboardMovementsCommand command,
        CancellationToken cancellationToken = default)
    {
        if (command.ReleaseIds.Count == 0)
            throw new ArgumentException("Selecione ao menos um lançamento realizado.");

        return unitOfWork.ExecuteInTransactionAsync(async token =>
        {
            foreach (var releaseId in command.ReleaseIds.Distinct())
            {
                var release = await GetReleaseAsync(releaseId, token);
                if (!release.Realized)
                    throw new InvalidOperationException($"O lançamento '{release.Historic}' ainda não foi realizado.");

                release.Realized = false;
                await releaseRepository.UpdateAsync(release, token);
            }
        }, cancellationToken);
    }

    private async Task<FinancialReleaseEntity> GetReleaseAsync(long id, CancellationToken token) =>
        await releaseRepository.GetByIdAsync(id, token)
        ?? throw new InvalidOperationException("Lançamento não encontrado.");

    private async Task<FinancialPlanningEntity> GetPlanningAsync(long? id, CancellationToken token)
    {
        if (!id.HasValue)
            throw new InvalidOperationException("A previsão não possui planejamento de origem.");
        return await planningRepository.GetByIdAsync(id.Value, token)
            ?? throw new InvalidOperationException("Planejamento não encontrado.");
    }

    private async Task EnsureOccurrenceDoesNotExistAsync(
        long planningId,
        DateTime scheduledDate,
        CancellationToken token)
    {
        var releases = await releaseRepository.GetAllAsync(token);
        if (releases.Any(release =>
                release.FinancialPlanningId == planningId &&
                (release.ScheduledDate ?? release.PaymentDate).Date == scheduledDate.Date))
            throw new InvalidOperationException($"A previsão de {scheduledDate:dd/MM/yyyy} já foi materializada.");
    }

    private static FinancialReleaseEntity CreateRelease(
        FinancialPlanningEntity planning,
        DateTime scheduledDate,
        DateTime paymentDate,
        bool realized) => new()
    {
        CostCenterId = planning.CostCenterId,
        FinancialInstitutionId = planning.FinancialInstitutionId,
        FinancialPlanningId = planning.Id,
        ScheduledDate = scheduledDate.Date,
        PaymentDate = paymentDate.Date,
        CompensationDate = paymentDate.Date,
        Historic = planning.Historic,
        Parcel = planning.StartParcel,
        TotalParcel = planning.TotalParcel,
        Grouper = planning.Grouper,
        Value = planning.Value,
        Realized = realized
    };

    private static void EnsureSelection(IReadOnlyCollection<DashboardMovementSelection> items)
    {
        if (items.Count == 0)
            throw new ArgumentException("Selecione ao menos um lançamento.");
    }
}
