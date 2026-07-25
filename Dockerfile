# Сборка в одном образе, запуск в другом: в итоговый образ не попадают
# ни SDK, ни исходники — только готовое приложение.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Сначала только файлы проектов — тогда слой с восстановлением пакетов
# переиспользуется, пока не поменялись зависимости.
COPY Directory.Build.props Directory.Packages.props Portfolio.sln ./
COPY src/Portfolio.Domain/Portfolio.Domain.csproj src/Portfolio.Domain/
COPY src/Portfolio.Shared/Portfolio.Shared.csproj src/Portfolio.Shared/
COPY src/Portfolio.Application/Portfolio.Application.csproj src/Portfolio.Application/
COPY src/Portfolio.Infrastructure/Portfolio.Infrastructure.csproj src/Portfolio.Infrastructure/
COPY src/Portfolio.Api/Portfolio.Api.csproj src/Portfolio.Api/
COPY src/Portfolio.Client/Portfolio.Client.csproj src/Portfolio.Client/
COPY tests/Portfolio.Tests/Portfolio.Tests.csproj tests/Portfolio.Tests/
RUN dotnet restore src/Portfolio.Api/Portfolio.Api.csproj

COPY . .
RUN dotnet publish src/Portfolio.Api/Portfolio.Api.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./

# База лежит на отдельном томе, иначе данные пропадут при следующем деплое.
ENV ConnectionStrings__PortfolioDatabase="Data Source=/data/portfolio.db"
ENV ASPNETCORE_ENVIRONMENT=Production

# Приложение работает не от root.
RUN mkdir -p /data && useradd --uid 10001 --create-home app && chown -R app:app /app /data
USER app

VOLUME ["/data"]
EXPOSE 8080

# Render, Railway и подобные площадки сообщают порт через переменную PORT.
# Если её нет — слушаем 8080. exec нужен, чтобы сигналы остановки дошли до приложения.
ENTRYPOINT ["sh", "-c", "ASPNETCORE_HTTP_PORTS=${PORT:-8080} exec dotnet Portfolio.Api.dll"]
