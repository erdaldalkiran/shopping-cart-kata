FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
# Change region to TR
ENV LANG="tr_TR.UTF-8"

WORKDIR /app

# copy csproj and restore as distinct layers
COPY src/ShoppingCart.API/*.csproj ./ShoppingCart.API/
COPY src/ShoppingCart.Business/*.csproj ./ShoppingCart.Business/
COPY src/ShoppingCart.Infra/*.csproj ./ShoppingCart.Infra/
WORKDIR /app/ShoppingCart.API
RUN dotnet restore

# copy and publish app and libraries
WORKDIR /app
COPY src/ShoppingCart.API/. ./ShoppingCart.API/
COPY src/ShoppingCart.Business/. ./ShoppingCart.Business/
COPY src/ShoppingCart.Infra/. ./ShoppingCart.Infra/
WORKDIR /app/ShoppingCart.API
RUN dotnet publish -c Release -o out

# test application
FROM build AS testrunner
WORKDIR /app/tests

COPY src/tests/ShoppingCart.UnitTests/ ./ShoppingCart.UnitTests
RUN dotnet test ./ShoppingCart.UnitTests

COPY src/tests/ShoppingCart.APITests/ ./ShoppingCart.APITests
RUN dotnet test ./ShoppingCart.APITests


# runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
# Change region to TR
ENV LANG="tr_TR.UTF-8"

WORKDIR /app
COPY --from=build /app/ShoppingCart.API/out ./

ENTRYPOINT ["dotnet", "ShoppingCart.API.dll"]

