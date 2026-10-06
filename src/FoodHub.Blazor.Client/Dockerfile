FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY src/FoodHub.Domain.Share/FoodHub.Domain.Share.csproj src/FoodHub.Domain.Share/
COPY src/FoodHub.BlazorShared/FoodHub.BlazorShared.csproj src/FoodHub.BlazorShared/
COPY src/FoodHub.Blazor.Client/FoodHub.Blazor.Client.csproj src/FoodHub.Blazor.Client/

RUN dotnet restore src/FoodHub.Blazor.Client/FoodHub.Blazor.Client.csproj

COPY src/ src/

RUN dotnet publish src/FoodHub.Blazor.Client/FoodHub.Blazor.Client.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


FROM nginx:alpine AS final

COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]