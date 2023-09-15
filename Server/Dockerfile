FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Server/MockShop.Server.csproj", "Server/"]
COPY ["Client/MockShop.Client.csproj", "Client/"]
COPY ["Shared/MockShop.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/MockShop.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "MockShop.Server.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/Client"
RUN dotnet build "MockShop.Client.csproj" -c Release --no-restore
RUN dotnet publish "MockShop.Server.csproj" -c Release -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MockShop.Server.dll"]
