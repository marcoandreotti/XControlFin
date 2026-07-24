using xControlFin.Application.Features.Financial.Commands;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Enums;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Financial.Handlers;

public class EffectuateFinancialPlanningCommandHandler : ICommandHandler<EffectuateFinancialPlanningCommand, long>
{
    private readonly IBaseRepository<FinancialPlanningEntity> _planningRepository;
    private readonly IBaseRepository<FinancialReleaseEntity> _releaseRepository;

    public EffectuateFinancialPlanningCommandHandler(
        IBaseRepository<FinancialPlanningEntity> planningRepository,
        IBaseRepository<FinancialReleaseEntity> releaseRepository)
    {
        _planningRepository = planningRepository;
        _releaseRepository = releaseRepository;
    }

    public async Task<long> HandleAsync(EffectuateFinancialPlanningCommand command, CancellationToken cancellationToken = default)
    {
        var planning = await _planningRepository.GetByIdAsync(command.FinancialPlanningId, cancellationToken);
        if (planning == null)
            throw new Exception("Planejamento não encontrado.");
            
        var paymentDate = command.PaymentDate ?? planning.StartDate;
        
        var release = new FinancialReleaseEntity
        {
            CostCenterId = planning.CostCenterId,
            FinancialInstitutionId = planning.FinancialInstitutionId,
            FinancialPlanningId = planning.Id,
            PaymentDate = paymentDate,
            CompensationDate = paymentDate,
            ScheduledDate = planning.StartDate,
            Historic = command.OverrideHistoric ?? planning.Historic,
            Parcel = planning.StartParcel,
            TotalParcel = planning.TotalParcel,
            Grouper = planning.Grouper,
            Value = command.OverrideValue ?? planning.Value,
            Realized = true
        };

        var createdRelease = await _releaseRepository.AddAsync(release, cancellationToken);

        planning.LastStartDate = planning.StartDate;
        planning.StartDate = GetNextDate(planning.StartDate, planning.TimeInterval);
        
        if (planning.TotalParcel > 0)
        {
            planning.StartParcel++;
            if (planning.StartParcel > planning.TotalParcel)
            {
                planning.IsActive = false;
            }
        }

        await _planningRepository.UpdateAsync(planning, cancellationToken);

        return createdRelease.Id;
    }

    private DateTime GetNextDate(DateTime currentDate, TimeIntervalEnum interval)
    {
        return interval switch
        {
            TimeIntervalEnum.Daily => currentDate.AddDays(1),
            TimeIntervalEnum.Weekly => currentDate.AddDays(7),
            TimeIntervalEnum.Monthly => currentDate.AddMonths(1),
            TimeIntervalEnum.Yearly => currentDate.AddYears(1),
            _ => currentDate.AddMonths(1)
        };
    }
}
