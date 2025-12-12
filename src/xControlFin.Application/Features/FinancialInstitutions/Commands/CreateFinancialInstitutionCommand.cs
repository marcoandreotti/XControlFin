using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.FinancialInstitutions.Commands;

public class CreateFinancialInstitutionCommand : ICommand<long>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int Sequence { get; set; } = 1;
}
