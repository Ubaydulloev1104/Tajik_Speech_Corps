#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TSR_Accoun_Api/TSR_Accoun_Api.csproj", "TSR_Accoun_Api/"]
RUN dotnet restore "./TSR_Accoun_Api/./TSR_Accoun_Api.csproj"
COPY . .
WORKDIR "/src/TSR_Accoun_Api"
RUN dotnet build "./TSR_Accoun_Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TSR_Accoun_Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TSR_Accoun_Api.dll"]