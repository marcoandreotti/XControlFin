using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.UserFinancialInstitutions.Commands;

public class CreateUserFinancialInstitutionCommand : ICommand<long>
{
    public long UserId { get; set; }
    public long FinancialInstitutionId { get; set; }
}

public class DeleteUserFinancialInstitutionCommand : ICommand
{
    public long Id { get; set; }
}