FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# restore using the API project only (no solution needed)
COPY LocalMarket.Api/LocalMarket.Api.csproj LocalMarket.Api/
RUN dotnet restore LocalMarket.Api/LocalMarket.Api.csproj

# copy the rest and publish
COPY . ./
RUN dotnet publish LocalMarket.Api/LocalMarket.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "LocalMarket.Api.dll"]
