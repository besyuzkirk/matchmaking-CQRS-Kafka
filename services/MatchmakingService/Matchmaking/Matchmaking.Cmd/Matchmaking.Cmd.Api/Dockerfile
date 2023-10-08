FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Matchmaking.Cmd/Matchmaking.Cmd.Api/Matchmaking.Cmd.Api.csproj", "Matchmaking.Cmd/Matchmaking.Cmd.Api/"]
COPY ["../EventSourcing/CQRS.Core/CQRS.Core.csproj", "../EventSourcing/CQRS.Core/"]
COPY ["Matchmaking.Cmd/Matchmaking.Cmd.Domain/Matchmaking.Cmd.Domain.csproj", "Matchmaking.Cmd/Matchmaking.Cmd.Domain/"]
COPY ["Matchmaking.Common/Matchmaking.Common.csproj", "Matchmaking.Common/"]
COPY ["Matchmaking.Cmd/Matchmaking.Cmd.Infrastructure/Matchmaking.Cmd.Infrastructure.csproj", "Matchmaking.Cmd/Matchmaking.Cmd.Infrastructure/"]
RUN dotnet restore "Matchmaking.Cmd/Matchmaking.Cmd.Api/Matchmaking.Cmd.Api.csproj"
COPY . .
WORKDIR "/src/Matchmaking.Cmd/Matchmaking.Cmd.Api"
RUN dotnet build "Matchmaking.Cmd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Matchmaking.Cmd.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Matchmaking.Cmd.Api.dll"]
