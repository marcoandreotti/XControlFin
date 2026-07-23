using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Auth.Queries;

public sealed record GetActiveLoginUsersQuery
    : IQuery<List<LoginUserDto>>;
