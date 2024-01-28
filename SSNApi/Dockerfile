#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SSNApi/SSNApi.csproj", "SSNApi/"]
COPY ["SSNApi.Domain/SSNApi.Domain.csproj", "SSNApi.Domain/"]
COPY ["SSNLib/SSNLib.csproj", "SSNLib/"]
RUN dotnet restore "./SSNApi/SSNApi.csproj"
COPY . .
WORKDIR "/src/SSNApi"
RUN dotnet build "./SSNApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SSNApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSNApi.dll"]