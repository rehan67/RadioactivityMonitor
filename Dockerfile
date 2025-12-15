FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY RadioactivityMonitor.sln ./
COPY src/ ./src/
COPY tests/ ./tests/

RUN dotnet restore RadioactivityMonitor.sln
RUN dotnet build RadioactivityMonitor.sln -c Release --no-restore

FROM build AS test
WORKDIR /app
RUN dotnet test RadioactivityMonitor.sln -c Release --no-build --collect:"XPlat Code Coverage" --logger:"trx;LogFileName=test_results.trx"

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runner
WORKDIR /app
COPY --from=test /app/tests/RadioactivityMonitor.Tests/TestResults/ ./TestResults/
CMD ["powershell", "-Command", "Get-ChildItem -Recurse ./TestResults; Start-Sleep -Seconds 3600"]
