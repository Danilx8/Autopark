#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AutoparkDataGenerator/AutoparkDataGenerator.csproj", "."]
RUN dotnet restore "./AutoparkDataGenerator/AutoparkDataGenerator.csproj"
COPY ["AutoparkPathsGenerator/AutoparkPathsGenerator.csproj", "."]
RUN dotnet restore "./AutoparkPathsGenerator/AutoparkPathsGenerator.csproj"
COPY ["Autopark/Autopark.csproj", "."]
RUN dotnet restore "./Autopark/Autopark.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Autopark.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Autopark.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Autopark.dll"]