FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HumiditySensor.API/HumiditySensor.API.csproj", "HumiditySensor.API/"]
COPY ["HumiditySensor.Application/HumiditySensor.Application.csproj", "HumiditySensor.Application/"]
COPY ["HumiditySensor.Contracts/HumiditySensor.Contracts.csproj", "HumiditySensor.Contracts/"]
RUN dotnet restore "HumiditySensor.API/HumiditySensor.API.csproj"
COPY . .
WORKDIR "/src/HumiditySensor.API"
RUN dotnet build "./HumiditySensor.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HumiditySensor.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HumiditySensor.API.dll"]
