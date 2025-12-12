# ============================
# BASE (runtime)
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# Expor apenas HTTP: HTTPS não será usado no container
EXPOSE 8080

# ============================
# BUILD (SDK)
# ============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copia somente os csproj primeiro (cache no restore)
COPY src/xControlFin.Api/xControlFin.Api.csproj src/xControlFin.Api/
COPY src/xControlFin.Crosscutting.IoC/xControlFin.Crosscutting.IoC.csproj src/xControlFin.Crosscutting.IoC/
COPY src/xControlFin.Infrastructure/xControlFin.Infrastructure.csproj src/xControlFin.Infrastructure/
COPY src/xControlFin.Domain/xControlFin.Domain.csproj src/xControlFin.Domain/
COPY src/xControlFin.Shared.Abstractions/xControlFin.Shared.Abstractions.csproj src/xControlFin.Shared.Abstractions/
COPY src/xControlFin.Crosscutting.Common/xControlFin.Crosscutting.Common.csproj src/xControlFin.Crosscutting.Common/
COPY src/xControlFin.Application/xControlFin.Application.csproj src/xControlFin.Application/
COPY src/xControlFin.Shared/xControlFin.Shared.csproj src/xControlFin.Shared/

# Restore antes de copiar o resto do projeto (melhor cache)
RUN dotnet restore src/xControlFin.Api/xControlFin.Api.csproj

# Copiar todo o código
COPY . .

WORKDIR /src/src/xControlFin.Api
RUN dotnet build xControlFin.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

# ============================
# PUBLISH
# ============================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish xControlFin.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ============================
# FINAL IMAGE
# ============================
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "xControlFin.Api.dll"]
