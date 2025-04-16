# Используем .NET 8.0 образы
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем .csproj и восстанавливаем зависимости
COPY ["RadioAmateurHelper.csproj", "./"]
RUN dotnet restore "RadioAmateurHelper.csproj"

# Копируем остальные файлы и собираем проект
COPY . .
RUN dotnet build "RadioAmateurHelper.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RadioAmateurHelper.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RadioAmateurHelper.dll"]
