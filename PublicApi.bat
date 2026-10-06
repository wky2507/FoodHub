@echo off

cls

echo.

echo "FoodHub.PublicApi"

echo.
echo Cleaning......
echo dotnet clean

echo.
echo Building.......
echo dotnet build

echo Starting FoodHub.PublicApi...
start "FoodHub" cmd /k "cd src/FoodHub.PublicApi && dotnet watch run -lp https
--configuration Debug --environment Development --urls https://localhost:7072;http://localhost:5071"