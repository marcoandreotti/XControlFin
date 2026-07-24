using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Scalar.AspNetCore;
using System.Text;
using xControlFin.Api.Middleware;
using xControlFin.Crosscutting.Common.Security;
using xControlFin.Crosscutting.IoC;

var builder = WebApplication.CreateBuilder(args);

//
// ──────────────────────────────────────────────
//  SERILOG
// ──────────────────────────────────────────────
//
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

//
// ──────────────────────────────────────────────
//  CONFIGURAÇÃO DE SERVIÇOS
// ──────────────────────────────────────────────
//
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

//
// ──────────────────────────────────────────────
//  CONFIG JWT
// ──────────────────────────────────────────────
//
var jwtSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSection);

var secret = jwtSection.GetValue<string>("Secret") ?? string.Empty;
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.Configure<Microsoft.AspNetCore.OpenApi.OpenApiOptions>(options =>
{
    options.AddDocumentTransformer<JwtSecuritySchemeTransformer>();
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        // Em Docker não devemos exigir HTTPS
        options.RequireHttpsMetadata = !builder.Environment.IsEnvironment("Docker");

        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateIssuer = true,
            ValidIssuer = jwtSection.GetValue<string>("Issuer"),

            ValidateAudience = true,
            ValidAudience = jwtSection.GetValue<string>("Audience"),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

//
// ──────────────────────────────────────────────
//  DEPENDENCY INJECTION (IoC)
// ──────────────────────────────────────────────
//
builder.Services.RegisterServices(builder.Configuration);

//
// ──────────────────────────────────────────────
//  BUILD APP
// ──────────────────────────────────────────────
//
var app = builder.Build();

//
// ──────────────────────────────────────────────
//  INICIALIZAÇÃO DO BANCO DE DADOS
// ──────────────────────────────────────────────
//
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<xControlFin.Infrastructure.Data.XControlFinDbContext>();
        await xControlFin.Infrastructure.Data.DatabaseSchemaInitializer
            .EnsureCompatibilityAsync(dbContext);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao inicializar e criar o banco de dados.");
    }
}

//
// ──────────────────────────────────────────────
//  MIDDLEWARE GLOBAL
// ──────────────────────────────────────────────
//
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging();

//
// ──────────────────────────────────────────────
//  HTTPS → DESATIVADO NO DOCKER
// ──────────────────────────────────────────────
//
if (!app.Environment.IsEnvironment("Docker"))
{
    app.UseHttpsRedirection();
}

//
// ──────────────────────────────────────────────
//  SWAGGER / SCALAR
// ──────────────────────────────────────────────
//
app.MapOpenApi();

app.MapScalarApiReference((options, context) =>
{
    var basePath = context.Request.PathBase.HasValue ? context.Request.PathBase.Value : "";
    options.OpenApiRoutePattern = $"{basePath}/openapi/{{documentName}}.json";
    options.Theme = ScalarTheme.Solarized;
    options.Title = "XControlFin - Controle Financeiro";
    options.Layout = ScalarLayout.Modern;
    options.OperationSorter = OperationSorter.Alpha;
    options.TagSorter = TagSorter.Alpha;
});

//
// ──────────────────────────────────────────────
//  AUTH
// ──────────────────────────────────────────────
//
app.UseAuthentication();
app.UseAuthorization();

//
// ──────────────────────────────────────────────
//  ROOT REDIRECT
// ──────────────────────────────────────────────
//
app.MapGet("/", context =>
{
    context.Response.Redirect("/scalar");
    return Task.CompletedTask;
});

//
// ──────────────────────────────────────────────
//  CONTROLLERS
// ──────────────────────────────────────────────
//
app.MapControllers();

//
// ──────────────────────────────────────────────
//  RUN
// ──────────────────────────────────────────────
//
app.Run();
