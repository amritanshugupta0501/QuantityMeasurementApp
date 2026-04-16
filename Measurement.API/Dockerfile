# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files first to cache dependencies
COPY ["Measurement.API/Measurement.API.csproj", "Measurement.API/"]
COPY ["QuantityMeasurementModel/QuantityMeasurementModel.csproj", "QuantityMeasurementModel/"]
COPY ["QuantityMeasurementRepository/QuantityMeasurementRepository.csproj", "QuantityMeasurementRepository/"]
COPY ["QuantityMeasurementService/QuantityMeasurementService.csproj", "QuantityMeasurementService/"]
RUN dotnet restore "./Measurement.API/Measurement.API.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/Measurement.API"
RUN dotnet build "./Measurement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Measurement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Measurement.API.dll"]
