FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RaiffeisenClone.WebApi/RaiffeisenClone.WebApi.csproj", "RaiffeisenClone.WebApi/"]
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
