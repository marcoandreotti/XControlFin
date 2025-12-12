using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using xControlFin.Application.Features.Auth.Commands;
using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Application.Features.Auth.Handlers;
using xControlFin.Application.Features.CostCenters.Commands;
using xControlFin.Application.Features.CostCenters.Handlers;
using xControlFin.Application.Features.CostCenters.Queries;
using xControlFin.Application.Features.Financial.Commands;
using xControlFin.Application.Features.Financial.Dtos;
using xControlFin.Application.Features.Financial.Handlers;
using xControlFin.Application.Features.Financial.Queries;
using xControlFin.Application.Features.FinancialInstitutions.Commands;
using xControlFin.Application.Features.FinancialInstitutions.Handlers;
using xControlFin.Application.Features.FinancialInstitutions.Queries;
using xControlFin.Application.Features.UserFinancialInstitutions.Commands;
using xControlFin.Application.Features.UserFinancialInstitutions.Handlers;
using xControlFin.Application.Features.UserFinancialInstitutions.Queries;
using xControlFin.Application.Features.Users.Commands;
using xControlFin.Application.Features.Users.Handlers;
using xControlFin.Application.Features.Users.Queries;
using xControlFin.Crosscutting.Common.Security;
using xControlFin.Domain.Entities;
using xControlFin.Domain.Interfaces;
using xControlFin.Infrastructure.Data;
using xControlFin.Infrastructure.Repositories;
using xControlFin.Infrastructure.Services;
using xControlFin.Shared;
using xControlFin.Shared.Abstractions;
using xControlFin.Shared.Abstractions.Commands;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.Crosscutting.IoC;

public static class Bootstrapper
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Data
        services.AddDbContext<XControlFinDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IFinancialRepository, FinancialRepository>();

        // Base Generic
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        // Dispatcher
        services.AddScoped<IDispatcher, InMemoryDispatcher>();

        // Handlers - Financial Read
        services.AddScoped<IQueryHandler<GetFinancialReleasesQuery, List<FinancialCheckDto>>, GetFinancialReleasesQueryHandler>();

        // Handlers - Users
        services.AddScoped<ICommandHandler<CreateUserCommand, long>, UserHandler>();
        services.AddScoped<ICommandHandler<UpdateUserCommand>, UserHandler>();
        services.AddScoped<ICommandHandler<DeleteUserCommand>, UserHandler>();
        services.AddScoped<IQueryHandler<GetUserByIdQuery, UserEntity?>, UserHandler>();
        services.AddScoped<IQueryHandler<GetAllUsersQuery, List<UserEntity>>, UserHandler>();

        // Handlers - CostCenter
        services.AddScoped<ICommandHandler<CreateCostCenterCommand, long>, CreateCostCenterCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateCostCenterCommand>, CostCenterHandler>();
        services.AddScoped<ICommandHandler<DeleteCostCenterCommand>, CostCenterHandler>();
        services.AddScoped<IQueryHandler<GetCostCenterByIdQuery, CostCenterEntity?>, CostCenterHandler>();
        services.AddScoped<IQueryHandler<GetAllCostCentersQuery, List<CostCenterEntity>>, CostCenterHandler>();

        // Handlers - FinancialInstitution
        services.AddScoped<ICommandHandler<CreateFinancialInstitutionCommand, long>, CreateFinancialInstitutionCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateFinancialInstitutionCommand>, FinancialInstitutionHandler>();
        services.AddScoped<ICommandHandler<DeleteFinancialInstitutionCommand>, FinancialInstitutionHandler>();
        services.AddScoped<IQueryHandler<GetFinancialInstitutionByIdQuery, FinancialInstitutionEntity?>, FinancialInstitutionHandler>();
        services.AddScoped<IQueryHandler<GetAllFinancialInstitutionsQuery, List<FinancialInstitutionEntity>>, FinancialInstitutionHandler>();

        // Handlers - UserFinancialInstitution Links
        services.AddScoped<ICommandHandler<CreateUserFinancialInstitutionCommand, long>, UserFinancialInstitutionHandler>();
        services.AddScoped<ICommandHandler<DeleteUserFinancialInstitutionCommand>, UserFinancialInstitutionHandler>();
        services.AddScoped<IQueryHandler<GetFinancialInstitutionsByUserIdQuery, List<long>>, UserFinancialInstitutionHandler>();

        // Handlers - Auth
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IPasswordManager, PasswordManager>();
        services.AddScoped<ICommandHandler<LoginCommand, AuthResponseDto>, AuthHandler>();
        services.AddScoped<ICommandHandler<RefreshTokenCommand, AuthResponseDto>, AuthHandler>();

        // Handlers - Financial Write CRUD
        services.AddScoped<ICommandHandler<CreateFinancialReleaseCommand, long>, CreateFinancialReleaseCommandHandler>();
        services.AddScoped<ICommandHandler<CreateFinancialPlanningCommand, long>, CreateFinancialPlanningCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateFinancialReleaseCommand>, FinancialCrudHandler>();
        services.AddScoped<ICommandHandler<DeleteFinancialReleaseCommand>, FinancialCrudHandler>();
        services.AddScoped<ICommandHandler<DeleteFinancialPlanningCommand>, FinancialCrudHandler>();
        services.AddScoped<IQueryHandler<GetFinancialReleaseByIdQuery, FinancialReleaseEntity?>, FinancialCrudHandler>();
    }
}