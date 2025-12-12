using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Users.Commands;

public class CreateUserCommand : ICommand<long>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Image { get; set; }
    public bool Active { get; set; } = true;
}

public class UpdateUserCommand : ICommand
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Image { get; set; }
    public bool Active { get; set; }
}

public class DeleteUserCommand : ICommand
{
    public long Id { get; set; }
}