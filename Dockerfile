FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app

# copy csproj and restore as distinct layers
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AuthenticationService/AuthenticationService.csproj", "AuthenticationService/"]
COPY ["BusinessLogicLayer/BusinessLogicLayer.csproj", "BusinessLogicLayer/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
COPY ["DataTransferObject/DataTransferObject.csproj", "DataTransferObject/"]
RUN dotnet restore "AuthenticationService/AuthenticationService.csproj"
COPY . .
WORKDIR "/src/AuthenticationService"
RUN dotnet build "AuthenticationService.csproj" -c Release -o /app

FROM build as publish
RUN dotnet publish "AuthenticationService.csproj" -c Release -o /app

# final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthenticationService.dll"]