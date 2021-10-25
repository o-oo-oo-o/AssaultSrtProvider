FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AssaultSrtProvider/AssaultSrtProvider.csproj", "AssaultSrtProvider/"]
RUN dotnet restore "AssaultSrtProvider/AssaultSrtProvider.csproj"
COPY . .
WORKDIR "/src/AssaultSrtProvider"
RUN dotnet build "AssaultSrtProvider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssaultSrtProvider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssaultSrtProvider.dll"]
