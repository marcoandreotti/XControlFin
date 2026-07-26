using xControlFin.Application.Features.Dashboard.Dtos;
using xControlFin.Application.Features.Dashboard.Queries;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Enums;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Dashboard.Handlers;

public sealed class DashboardHandler(
    IBaseRepository<UserFinancialInstitutionEntity> userInstitutionRepository,
    IBaseRepository<FinancialInstitutionEntity> institutionRepository,
    IBaseRepository<CostCenterEntity> costCenterRepository,
    IBaseRepository<FinancialReleaseEntity> releaseRepository,
    IBaseRepository<FinancialPlanningEntity> planningRepository)
    : IQueryHandler<GetDashboardQuery, DashboardDto>
{
    public async Task<DashboardDto> HandleAsync(
        GetDashboardQuery query,
        CancellationToken cancellationToken = default)
    {
        if (query.EndDate.Date < query.StartDate.Date)
        {
            throw new ArgumentException("A data final deve ser maior ou igual à data inicial.");
        }

        var links = await userInstitutionRepository.GetAllAsync(cancellationToken);
        var institutionIds = links
            .Where(link => link.UserId == query.UserId)
            .Select(link => link.FinancialInstitutionId)
            .ToHashSet();

        var institutions = (await institutionRepository.GetAllAsync(cancellationToken))
            .Where(item => item.IsActive && institutionIds.Contains(item.Id))
            .OrderBy(item => item.Sequence)
            .ThenBy(item => item.Name)
            .ToList();
        var activeInstitutionIds = institutions.Select(item => item.Id).ToHashSet();

        var costCenters = (await costCenterRepository.GetAllAsync(cancellationToken))
            .ToDictionary(item => item.Id, item => item.Name);
        var releases = (await releaseRepository.GetAllAsync(cancellationToken))
            .Where(item => activeInstitutionIds.Contains(item.FinancialInstitutionId))
            .ToList();
        var plannings = (await planningRepository.GetAllAsync(cancellationToken))
            .Where(item => item.IsActive && activeInstitutionIds.Contains(item.FinancialInstitutionId))
            .ToList();

        var dashboardRows = CreateRows(
            institutions,
            costCenters,
            releases,
            plannings,
            query.StartDate.Date,
            query.EndDate.Date.AddDays(1).AddTicks(-1));

        if (query.InstitutionId > 0)
        {
            dashboardRows = dashboardRows
                .Where(row => row.InstitutionId == query.InstitutionId)
                .ToList();
        }

        var balanceEnd = query.BalanceDate.Date.AddDays(1).AddTicks(-1);
        var balanceRows = CreateRows(
            institutions,
            costCenters,
            releases,
            plannings,
            DateTime.MinValue,
            balanceEnd);

        var balances = institutions.Select(institution =>
        {
            var accountRows = balanceRows.Where(row => row.InstitutionId == institution.Id);
            return new AccountBalanceDto(
                institution.Id,
                institution.Name,
                accountRows.Where(row => row.Realized).Sum(row => row.Value),
                accountRows.Where(row => !row.Realized).Sum(row => row.Value));
        }).ToList();

        return new DashboardDto
        {
            Accounts = balances,
            Releases = dashboardRows
                .OrderBy(row => row.PaymentDate)
                .ThenBy(row => row.Institution)
                .ToList()
        };
    }

    private static List<DashboardReleaseDto> CreateRows(
        IReadOnlyCollection<FinancialInstitutionEntity> institutions,
        IReadOnlyDictionary<long, string> costCenters,
        IReadOnlyCollection<FinancialReleaseEntity> releases,
        IReadOnlyCollection<FinancialPlanningEntity> plannings,
        DateTime startDate,
        DateTime endDate)
    {
        var institutionNames = institutions.ToDictionary(item => item.Id, item => item.Name);
        var rows = releases
            .Where(item => item.PaymentDate >= startDate && item.PaymentDate <= endDate)
            .Select(item => new DashboardReleaseDto(
                item.Id,
                item.FinancialPlanningId,
                item.FinancialInstitutionId,
                institutionNames.GetValueOrDefault(item.FinancialInstitutionId, "Conta"),
                costCenters.GetValueOrDefault(item.CostCenterId, "Sem categoria"),
                item.PaymentDate,
                item.ScheduledDate ?? item.PaymentDate,
                item.Historic,
                item.Value,
                item.Realized,
                false))
            .ToList();

        foreach (var planning in plannings)
        {
            foreach (var occurrence in GenerateDates(planning, startDate, endDate))
            {
                var alreadyMaterialized = releases.Any(release =>
                    release.FinancialPlanningId == planning.Id &&
                    (release.ScheduledDate ?? release.PaymentDate).Date == occurrence.Date);
                if (alreadyMaterialized)
                {
                    continue;
                }

                rows.Add(new DashboardReleaseDto(
                    null,
                    planning.Id,
                    planning.FinancialInstitutionId,
                    institutionNames.GetValueOrDefault(planning.FinancialInstitutionId, "Conta"),
                    costCenters.GetValueOrDefault(planning.CostCenterId, "Sem categoria"),
                    occurrence,
                    occurrence,
                    planning.Historic,
                    planning.Value,
                    false,
                    true));
            }
        }

        return rows;
    }

    private static IEnumerable<DateTime> GenerateDates(
        FinancialPlanningEntity planning,
        DateTime startDate,
        DateTime endDate)
    {
        var current = planning.StartDate.Date;
        while (current <= endDate)
        {
            if (planning.EndDate.HasValue && current > planning.EndDate.Value.Date)
            {
                yield break;
            }

            if (current >= startDate)
            {
                yield return current;
            }

            current = planning.TimeInterval switch
            {
                TimeIntervalEnum.Daily => current.AddDays(1),
                TimeIntervalEnum.Weekly => current.AddDays(7),
                TimeIntervalEnum.Monthly => current.AddMonths(1),
                TimeIntervalEnum.Yearly => current.AddYears(1),
                _ => current.AddMonths(1)
            };
        }
    }
}
