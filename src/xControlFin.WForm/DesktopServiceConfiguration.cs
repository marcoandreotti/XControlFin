using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using xControlFin.Application.Features.Auth;
using xControlFin.Application.Features.Auth.Commands;
using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Application.Features.Auth.Handlers;
using xControlFin.Application.Features.Auth.Queries;
using xControlFin.Application.Features.Dashboard.Dtos;
using xControlFin.Application.Features.Dashboard.Commands;
using xControlFin.Application.Features.Dashboard.Handlers;
using xControlFin.Application.Features.Dashboard.Queries;
using xControlFin.Crosscutting.Common.Security;
using xControlFin.Domain.Interfaces;
using xControlFin.Infrastructure.Data;
using xControlFin.Infrastructure.Repositories;
using xControlFin.Shared;
using xControlFin.Shared.Abstractions;
using xControlFin.Shared.Abstractions.Commands;
using xControlFin.Shared.Abstractions.Queries;

namespace xControlFin.WForm;

internal static class DesktopServiceConfiguration
{
    public static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
        var databasePath = DatabaseFactory.ResolveDatabasePath();

        services.AddDbContext<XControlFinDbContext>(options =>
            options.UseSqlite($"Data Source={databasePath};Cache=Shared;Mode=ReadWriteCreate;"));

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        services.AddScoped<IPasswordManager, PasswordManager>();
        services.AddScoped<ICredentialAuthenticationService, CredentialAuthenticationService>();
        services.AddScoped<ICommandHandler<LoginLocalCommand, LocalUserSessionDto?>, LocalAuthHandler>();
        services.AddScoped<IQueryHandler<GetActiveLoginUsersQuery, List<LoginUserDto>>, LocalAuthHandler>();
        services.AddScoped<IQueryHandler<GetDashboardQuery, DashboardDto>, DashboardHandler>();
        services.AddScoped<ICommandHandler<EffectuateDashboardMovementsCommand>, DashboardMovementHandler>();
        services.AddScoped<ICommandHandler<ChangeDashboardMovementDatesCommand>, DashboardMovementHandler>();
        services.AddScoped<ICommandHandler<ReverseDashboardMovementsCommand>, DashboardMovementHandler>();
        services.AddScoped<IDispatcher, InMemoryDispatcher>();

        return services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        });
    }
}
