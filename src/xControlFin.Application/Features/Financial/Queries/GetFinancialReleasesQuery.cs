using xControlFin.Application.Features.Financial.Dtos;
using xControlFin.Domain.Entities;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Financial.Queries;

public class GetFinancialReleasesQuery : IQuery<List<FinancialCheckDto>>
{
    public long FinancialInstitutionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class GetFinancialReleaseByIdQuery : IQuery<FinancialReleaseEntity?>
{
    public long Id { get; set; }
}