using xControlFin.Domain.Entities;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Users.Queries;

public class GetUserByIdQuery : IQuery<UserEntity?>
{
    public long Id { get; set; }
}

public class GetAllUsersQuery : IQuery<List<UserEntity>>
{
}