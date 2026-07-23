using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using xControlFin.Application.Features.Auth;
using xControlFin.Application.Features.Auth.Commands;
using xControlFin.Application.Features.Auth.Dtos;
using xControlFin.Application.Features.Auth.Handlers;
using xControlFin.Application.Features.Auth.Queries;
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
        services.AddScoped<IPasswordManager, PasswordManager>();
        services.AddScoped<ICredentialAuthenticationService, CredentialAuthenticationService>();
        services.AddScoped<ICommandHandler<LoginLocalCommand, LocalUserSessionDto?>, LocalAuthHandler>();
        services.AddScoped<IQueryHandler<GetActiveLoginUsersQuery, List<LoginUserDto>>, LocalAuthHandler>();
        services.AddScoped<IDispatcher, InMemoryDispatcher>();

        return services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        });
    }
}
