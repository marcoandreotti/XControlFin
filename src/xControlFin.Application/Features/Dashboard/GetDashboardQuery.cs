using xControlFin.Application.Features.Dashboard.Dtos;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Dashboard.Queries;

public sealed record GetDashboardQuery(
    long UserId,
    DateTime StartDate,
    DateTime EndDate,
    DateTime BalanceDate) : IQuery<DashboardDto>;
