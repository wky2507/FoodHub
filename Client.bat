@echo off

cls

echo "启动FoodHub"
：：切换到当前目录
cd /d  "%~dp0"

echo .

echo "FoodHub.Client"

echo.
echo Cleaning......
echo dotnet clean

echo.
echo Building
echo build

echo Starting...
start "FoodHub" cmd /k "cd src/FoodHub.Blazor.Client && dotnet watch run -lp https
--configuration Debug --environment Development --urls https://localhost:7116;http://localhost:5286"