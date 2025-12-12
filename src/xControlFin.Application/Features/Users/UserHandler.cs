using xControlFin.Application.Features.Users.Commands;
using xControlFin.Application.Features.Users.Queries;
using xControlFin.Crosscutting.Common.Security;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Shared.Abstractions.Commands;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Application.Features.Users.Handlers;

public class UserHandler :
    ICommandHandler<CreateUserCommand, long>,
    ICommandHandler<UpdateUserCommand>,
    ICommandHandler<DeleteUserCommand>,
    IQueryHandler<GetUserByIdQuery, UserEntity?>,
    IQueryHandler<GetAllUsersQuery, List<UserEntity>>
{
    private readonly IBaseRepository<UserEntity> _userRepo;
    private readonly IPasswordManager _passwordManager;

    public UserHandler(IBaseRepository<UserEntity> userRepo, IBaseRepository<UserEntity> userPwdRepo, IPasswordManager passwordManager)
    {
        _userRepo = userRepo;
        _passwordManager = passwordManager;
    }

    public async Task<long> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        // Salva com senha na tabela herdada (UserEntity é uma UserEntity)
        var entity = new UserEntity
        {
            Name = command.Name,
            Email = command.Email,
            Password = _passwordManager.HashPassword(command.Password),
            Image = command.Image,
            Active = command.Active,
            Created = DateTime.UtcNow
        };
        await _userRepo.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public async Task HandleAsync(UpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        var entity = await _userRepo.GetByIdAsync(command.Id, cancellationToken);
        if (entity != null)
        {
            entity.Name = command.Name;
            entity.Email = command.Email;
            entity.Image = command.Image;
            entity.Active = command.Active;
            await _userRepo.UpdateAsync(entity, cancellationToken);
        }
    }

    public async Task HandleAsync(DeleteUserCommand command, CancellationToken cancellationToken = default)
    {
        await _userRepo.DeleteAsync(command.Id, cancellationToken);
    }

    public async Task<UserEntity?> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken = default)
    {
        return await _userRepo.GetByIdAsync(query.Id, cancellationToken);
    }

    public async Task<List<UserEntity>> HandleAsync(GetAllUsersQuery query, CancellationToken cancellationToken = default)
    {
        return await _userRepo.GetAllAsync(cancellationToken);
    }
}