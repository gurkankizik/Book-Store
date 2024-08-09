#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Staj.Api/Staj.Api.csproj", "Staj.Api/"]
COPY ["StajWeb.DataAccess/StajWeb.DataAccess.csproj", "StajWeb.DataAccess/"]
COPY ["StajWeb.Models/StajWeb.Models.csproj", "StajWeb.Models/"]
COPY ["Staj.Utility/StajWeb.Utility.csproj", "Staj.Utility/"]
RUN dotnet restore "./Staj.Api/Staj.Api.csproj"
COPY . .
WORKDIR "/src/Staj.Api"
RUN dotnet build "./Staj.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Staj.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Staj.Api.dll"]