FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/SecureVault.Api/SecureVault.Api.csproj", "src/SecureVault.Api/"]
COPY ["src/SecureVault.Application/SecureVault.Application.csproj", "src/SecureVault.Application/"]
COPY ["src/SecureVault.Domain/SecureVault.Domain.csproj", "src/SecureVault.Domain/"]
COPY ["src/SecureVault.Infrastructure/SecureVault.Infrastructure.csproj", "src/SecureVault.Infrastructure/"]
RUN dotnet restore "src/SecureVault.Api/SecureVault.Api.csproj"
COPY . .
WORKDIR "/src/src/SecureVault.Api"
RUN dotnet build "SecureVault.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecureVault.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecureVault.Api.dll"]
