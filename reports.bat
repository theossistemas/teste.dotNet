@echo off

if not exist "%~dp0BuildReports" mkdir "%~dp0BuildReports"

rd /s /q %~dp0BuildReports

dotnet restore %~dp0LibraryStore.sln

dotnet build %~dp0LibraryStore.sln

dotnet test %~dp0LibraryStore.sln ^
            --logger "trx;LogFileName=TestResults.trx" ^
            --logger "xunit;LogFileName=TestResults.xml" ^
            --results-directory %~dp0BuildReports/UnitTests ^
            --collect:"XPlat Code Coverage" ^
            --no-build ^
            /p:CollectCoverage=true ^
            /p:CoverletOutput=%~dp0BuildReports\Coverage\ ^
            /p:CoverletOutputFormat=cobertura ^
            /p:Exclude="[xunit.*]*

REM dotnet reportgenerator ^
"%userprofile%\.nuget\packages\ReportGenerator\4.6.0\tools\netcoreapp3.0\ReportGenerator.exe" ^
       -reports:%~dp0BuildReports\Coverage\coverage.cobertura.xml ^
       -targetdir:%~dp0BuildReports\Coverage ^
       -reporttypes:HTML;HTMLSummary

start "report" "%~dp0BuildReports\Coverage\index.html"