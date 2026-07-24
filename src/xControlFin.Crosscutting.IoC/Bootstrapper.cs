using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using xControlFin.Application.Features.Auth.Commands;
using xControlFin.Application.Features.Auth;
using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Application.Features.Auth.Handlers;
using xControlFin.Application.Features.Auth.Queries;
using xControlFin.Application.Features.CostCenters.Commands;
using xControlFin.Application.Features.CostCenters.Handlers;
using xControlFin.Application.Features.CostCenters.Queries;
using xControlFin.Application.Features.Dashboard.Dtos;
using xControlFin.Application.Features.Dashboard.Commands;
using xControlFin.Application.Features.Dashboard.Handlers;
using xControlFin.Application.Features.Dashboard.Queries;
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
        var dbProvider = configuration["DatabaseProvider"] ?? "PostgreSQL";
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<XControlFinDbContext>(options =>
        {
            switch (dbProvider.ToLowerInvariant())
            {
                case "sqlite":
                    options.UseSqlite(connectionString, sqliteOptions => sqliteOptions.CommandTimeout(30));
                    break;

                case "msaccess":
                case "jet":
#pragma warning disable CA1416
                    options.UseJet(connectionString);
#pragma warning restore CA1416
                    break;

                case "postgresql":
                case "postgres":
                default:
                    options.UseNpgsql(connectionString);
                    break;
            }
        });

        // Repositories
        services.AddScoped<IFinancialRepository, FinancialRepository>();

        // Base Generic
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        // Dispatcher
        services.AddScoped<IDispatcher, InMemoryDispatcher>();

        // Handlers - Financial Read
        services.AddScoped<IQueryHandler<GetFinancialReleasesQuery, FInancialChecksDto>, GetFinancialReleasesQueryHandler>();
        services.AddScoped<IQueryHandler<GetAllFinancialPlanningsQuery, IEnumerable<FinancialPlanningEntity>>, GetAllFinancialPlanningsQueryHandler>();
        services.AddScoped<IQueryHandler<GetAllFinancialReleasesCrudQuery, IEnumerable<FinancialReleaseEntity>>, GetAllFinancialReleasesCrudQueryHandler>();

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
        services.AddScoped<ICommandHandler<EffectuateFinancialPlanningCommand, long>, EffectuateFinancialPlanningCommandHandler>();
        services.AddScoped<IQueryHandler<GetFinancialInstitutionByIdQuery, FinancialInstitutionEntity?>, FinancialInstitutionHandler>();
        services.AddScoped<IQueryHandler<GetAllFinancialInstitutionsQuery, List<FinancialInstitutionEntity>>, FinancialInstitutionHandler>();

        // Handlers - UserFinancialInstitution Links
        services.AddScoped<ICommandHandler<CreateUserFinancialInstitutionCommand, long>, UserFinancialInstitutionHandler>();
        services.AddScoped<ICommandHandler<DeleteUserFinancialInstitutionCommand>, UserFinancialInstitutionHandler>();
        services.AddScoped<IQueryHandler<GetFinancialInstitutionsByUserIdQuery, List<long>>, UserFinancialInstitutionHandler>();

        // Handlers - Auth
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IPasswordManager, PasswordManager>();
        services.AddScoped<ICredentialAuthenticationService, CredentialAuthenticationService>();
        services.AddScoped<ICommandHandler<LoginCommand, AuthResponseDto>, AuthHandler>();
        services.AddScoped<ICommandHandler<RefreshTokenCommand, AuthResponseDto>, AuthHandler>();
        services.AddScoped<ICommandHandler<LoginLocalCommand, LocalUserSessionDto?>, LocalAuthHandler>();
        services.AddScoped<IQueryHandler<GetActiveLoginUsersQuery, List<LoginUserDto>>, LocalAuthHandler>();
        services.AddScoped<IQueryHandler<GetDashboardQuery, DashboardDto>, DashboardHandler>();
        services.AddScoped<ICommandHandler<EffectuateDashboardMovementsCommand>, DashboardMovementHandler>();
        services.AddScoped<ICommandHandler<ChangeDashboardMovementDatesCommand>, DashboardMovementHandler>();
        services.AddScoped<ICommandHandler<ReverseDashboardMovementsCommand>, DashboardMovementHandler>();

        // Handlers - Financial Write CRUD
        services.AddScoped<ICommandHandler<CreateFinancialReleaseCommand, long>, CreateFinancialReleaseCommandHandler>();
        services.AddScoped<ICommandHandler<CreateFinancialPlanningCommand, long>, CreateFinancialPlanningCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateFinancialReleaseCommand>, FinancialCrudHandler>();
        services.AddScoped<ICommandHandler<DeleteFinancialReleaseCommand>, FinancialCrudHandler>();
        services.AddScoped<ICommandHandler<DeleteFinancialPlanningCommand>, FinancialCrudHandler>();
        services.AddScoped<IQueryHandler<GetFinancialReleaseByIdQuery, FinancialReleaseEntity?>, FinancialCrudHandler>();
    }
}
