FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SmartDoorSensor.Api/SmartDoorSensor.Api.csproj", "SmartDoorSensor.Api/"]
COPY ["SmartDoorSensor.Application/SmartDoorSensor.Application.csproj", "SmartDoorSensor.Application/"]
COPY ["SmartDoorSensor.Contracts/SmartDoorSensor.Contracts.csproj", "SmartDoorSensor.Contracts/"]
COPY ["SmartDoorSensor.Domain/SmartDoorSensor.Domain.csproj", "SmartDoorSensor.Domain/"]
COPY ["SmartDoorSensor.Infrastructure/SmartDoorSensor.Infrastructure.csproj", "SmartDoorSensor.Infrastructure/"]
RUN dotnet restore "SmartDoorSensor.Api/SmartDoorSensor.Api.csproj"
COPY . .
WORKDIR "/src/SmartDoorSensor.Api"
RUN dotnet build "./SmartDoorSensor.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SmartDoorSensor.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartDoorSensor.Api.dll"]
