using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Dashboard.Commands;

public sealed record DashboardMovementSelection(
    long? ReleaseId,
    long? PlanningId,
    DateTime ScheduledDate);

public sealed record EffectuateDashboardMovementsCommand(
    IReadOnlyCollection<DashboardMovementSelection> Items,
    DateTime EffectiveDate) : ICommand;

public sealed record ChangeDashboardMovementDatesCommand(
    IReadOnlyCollection<DashboardMovementSelection> Items,
    DateTime NewDate) : ICommand;

public sealed record ReverseDashboardMovementsCommand(
    IReadOnlyCollection<long> ReleaseIds) : ICommand;
