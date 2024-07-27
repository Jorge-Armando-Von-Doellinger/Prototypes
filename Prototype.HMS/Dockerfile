#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HMS.API/HMS.API.csproj", "/HMS.API/"]
COPY ["HMS.Application/HMS.Application.csproj", "/HMS.Application/"]
COPY ["HMS.Core/HMS.Core.csproj", "/HMS.Core/"]
COPY ["HMS.Infrastructure/HMS.Infrastructure.csproj", "/HMS.Infrastructure/"]
RUN dotnet restore "/HMS.API/HMS.API.csproj"
COPY . .
#WORKDIR 
RUN dotnet build "HMS.API/HMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "HMS.API/HMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HMS.API.dll"]