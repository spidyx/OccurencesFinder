FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OccurrencesFinder.IntegrationTestServer/OccurrencesFinder.IntegrationTestServer.csproj", "OccurrencesFinder.IntegrationTestServer/"]
RUN dotnet restore "OccurrencesFinder.IntegrationTestServer/OccurrencesFinder.IntegrationTestServer.csproj"
COPY . .
WORKDIR "/src/OccurrencesFinder.IntegrationTestServer"
RUN dotnet build "OccurrencesFinder.IntegrationTestServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OccurrencesFinder.IntegrationTestServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OccurrencesFinder.IntegrationTestServer.dll"]
