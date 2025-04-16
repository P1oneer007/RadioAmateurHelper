# Используем базовый образ SDK
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RadioAmateurHelper/RadioAmateurHelper.csproj", "RadioAmateurHelper/"]
RUN dotnet restore "RadioAmateurHelper/RadioAmateurHelper.csproj"
COPY . .
WORKDIR "/src/RadioAmateurHelper"
RUN dotnet build "RadioAmateurHelper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RadioAmateurHelper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RadioAmateurHelper.dll"]
