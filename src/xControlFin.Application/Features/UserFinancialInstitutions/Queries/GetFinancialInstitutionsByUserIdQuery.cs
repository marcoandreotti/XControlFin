using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.UserFinancialInstitutions.Queries;

public class GetFinancialInstitutionsByUserIdQuery : IQuery<List<long>>
{
    public long UserId { get; set; }
}