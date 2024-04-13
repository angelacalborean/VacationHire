
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Use runtime image from Microsoft
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

USER app

# Set the working directory to /app
WORKDIR /app

# Make ports available outside this container
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/VacationHire.InvoicesApi/VacationHire.InvoicesApi.csproj", "Src/VacationHire.InvoicesApi/"]
RUN dotnet restore "./Src/VacationHire.InvoicesApi/./VacationHire.InvoicesApi.csproj"
COPY . .
WORKDIR "/src/Src/VacationHire.InvoicesApi"
RUN dotnet build "./VacationHire.InvoicesApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VacationHire.InvoicesApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VacationHire.InvoicesApi.dll"]