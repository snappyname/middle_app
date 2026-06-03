FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 9080
EXPOSE 9081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TemperatureSensor.Api/TemperatureSensor.Api.csproj", "TemperatureSensor.Api/"]
COPY ["TemperatureSensor.Application/TemperatureSensor.Application.csproj", "TemperatureSensor.Application/"]
COPY ["TemperatureSensor.Contracts/TemperatureSensor.Contracts.csproj", "TemperatureSensor.Contracts/"]
RUN dotnet restore "TemperatureSensor.Api/TemperatureSensor.Api.csproj"
COPY . .
WORKDIR "/src/TemperatureSensor.Api"
RUN dotnet build "./TemperatureSensor.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TemperatureSensor.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TemperatureSensor.Api.dll"]
