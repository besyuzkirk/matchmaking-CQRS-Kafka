FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Matchmaking.Query/Matchmaking.Query.Api/Matchmaking.Query.Api.csproj", "Matchmaking.Query/Matchmaking.Query.Api/"]
COPY ["../EventSourcing/CQRS.Core/CQRS.Core.csproj", "../EventSourcing/CQRS.Core/"]
COPY ["Matchmaking.Common/Matchmaking.Common.csproj", "Matchmaking.Common/"]
COPY ["Matchmaking.Query/Matchmaking.Query.Infrastructure/Matchmaking.Query.Infrastructure.csproj", "Matchmaking.Query/Matchmaking.Query.Infrastructure/"]
COPY ["Matchmaking.Query/Matchmaking.Query.Domain/Matchmaking.Query.Domain.csproj", "Matchmaking.Query/Matchmaking.Query.Domain/"]
RUN dotnet restore "Matchmaking.Query/Matchmaking.Query.Api/Matchmaking.Query.Api.csproj"
COPY . .
WORKDIR "/src/Matchmaking.Query/Matchmaking.Query.Api"
RUN dotnet build "Matchmaking.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Matchmaking.Query.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Matchmaking.Query.Api.dll"]
