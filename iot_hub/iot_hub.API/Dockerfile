FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8000
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["iot_hub.API/iot_hub.API.csproj", "iot_hub.API/"]
COPY ["iot_hub.Application/iot_hub.Application.csproj", "iot_hub.Application/"]
COPY ["iot_hub.Contracts/iot_hub.Contracts.csproj", "iot_hub.Contracts/"]
COPY ["iot_hub.Domain/iot_hub.Domain.csproj", "iot_hub.Domain/"]
RUN dotnet restore "iot_hub.API/iot_hub.API.csproj"
COPY . .
WORKDIR "/src/iot_hub.API"
RUN dotnet build "./iot_hub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./iot_hub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iot_hub.API.dll"]
