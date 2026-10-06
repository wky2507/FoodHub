@echo off

cls

echo "启动FoodHub"
：：切换到当前目录
cd /d  "%~dp0"

echo.

echo "FoodHub"

echo.
echo Cleaning....
dotnet clean

echo.
echo Building
dotnet build

echo Starting FoodHub.PublicApi...
start "FoodHub" cmd /k "cd src/FoodHub.PublicApi && dotnet watch run -lp https
--configuration Debug --environment Development --urls https://localhost:7072;http://localhost:5071"

echo Starting...
start "FoodHub" cmd /k "cd src/FoodHub.Blazor.Client && dotnet watch run -lp https
--configuration Debug --environment Development --urls https://localhost:7116;http://localhost:5286"

echo.
echo "启动完成!"
