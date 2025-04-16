# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем файлы и собираем проект
COPY . ./
RUN dotnet publish -c Release -o out

# Этап запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Указываем входную точку
ENTRYPOINT ["dotnet", "RadioAmateurHelper.dll"]
