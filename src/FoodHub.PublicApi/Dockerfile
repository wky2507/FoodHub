FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY src/FoodHub.Domain/FoodHub.Domain.csproj src/FoodHub.Domain/
COPY src/FoodHub.Domain.Share/FoodHub.Domain.Share.csproj src/FoodHub.Domain.Share/
COPY src/FoodHub.Application/FoodHub.Application.csproj src/FoodHub.Application/
COPY src/FoodHub.Infrastructure/FoodHub.Infrastructure.csproj src/FoodHub.Infrastructure/
COPY src/FoodHub.BlazorShared/FoodHub.BlazorShared.csproj src/FoodHub.BlazorShared/
COPY src/FoodHub.Blazor.Client/FoodHub.Blazor.Client.csproj src/FoodHub.Blazor.Client/
COPY src/FoodHub.PublicApi/FoodHub.PublicApi.csproj src/FoodHub.PublicApi/

RUN dotnet restore src/FoodHub.PublicApi/FoodHub.PublicApi.csproj

COPY src/ src/

RUN dotnet publish src/FoodHub.PublicApi/FoodHub.PublicApi.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "FoodHub.PublicApi.dll"]