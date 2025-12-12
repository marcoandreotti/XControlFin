using xControlFin.Domain.Entities;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.FinancialInstitutions.Queries;

public class GetFinancialInstitutionByIdQuery : IQuery<FinancialInstitutionEntity?>
{
    public long Id { get; set; }
}

public class GetAllFinancialInstitutionsQuery : IQuery<List<FinancialInstitutionEntity>>
{
}