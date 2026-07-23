using xControlFin.Application.Features.Auth.Commands;
using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Application.Features.Auth.Queries;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Auth.Handlers;

public sealed class LocalAuthHandler(
    IBaseRepository<UserEntity> userRepository,
    ICredentialAuthenticationService authenticationService) :
    ICommandHandler<LoginLocalCommand, LocalUserSessionDto?>,
    IQueryHandler<GetActiveLoginUsersQuery, List<LoginUserDto>>
{
    public async Task<LocalUserSessionDto?> HandleAsync(
        LoginLocalCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await authenticationService.AuthenticateByIdAsync(
            command.UserId,
            command.Password,
            cancellationToken);

        return user is null
            ? null
            : new LocalUserSessionDto(user.Id, user.Name, user.Email, user.Image);
    }

    public async Task<List<LoginUserDto>> HandleAsync(
        GetActiveLoginUsersQuery query,
        CancellationToken cancellationToken = default)
    {
        var users = await userRepository.GetAllAsync(cancellationToken);
        return users
            .Where(user => user.Active)
            .OrderBy(user => user.Name)
            .Select(user => new LoginUserDto(user.Id, user.Name, user.Email))
            .ToList();
    }
}
