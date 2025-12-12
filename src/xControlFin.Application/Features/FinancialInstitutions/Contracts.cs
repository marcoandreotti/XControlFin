using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.FinancialInstitutions.Commands;

public class UpdateFinancialInstitutionCommand : ICommand
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Sequence { get; set; }
    public bool IsActive { get; set; }
}

public class DeleteFinancialInstitutionCommand : ICommand
{
    public long Id { get; set; }
}