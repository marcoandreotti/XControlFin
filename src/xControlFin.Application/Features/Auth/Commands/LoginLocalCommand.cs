using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Shared.Abstractions.Commands;

namespace xControlFin.Application.Features.Auth.Commands;

public sealed record LoginLocalCommand(long UserId, string Password)
    : ICommand<LocalUserSessionDto?>;
