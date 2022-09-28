FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7086
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ["./RaiffeisenClone.Domain/RaiffeisenClone.Domain.csproj", "RaiffeisenClone.Domain/"]
COPY ["./RaiffeisenClone.Persistence/RaiffeisenClone.Persistence.csproj", "RaiffeisenClone.Persistence/"]
COPY ["./RaiffeisenClone.Application/RaiffeisenClone.Application.csproj", "RaiffeisenClone.Application/"]
COPY ["./RaiffeisenClone.WebApi/RaiffeisenClone.WebApi.csproj", "RaiffeisenClone.WebApi/"]
RUN dotnet restore "RaiffeisenClone.WebApi/RaiffeisenClone.WebApi.csproj"
COPY . .
WORKDIR "/src/RaiffeisenClone.WebApi"
RUN dotnet build "RaiffeisenClone.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RaiffeisenClone.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RaiffeisenClone.WebApi.dll"]