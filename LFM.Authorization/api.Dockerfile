FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY ./LFM.Authorization.Api/*.csproj ./LFM.Authorization.Api/
COPY ./LFM.Authorization.Core/*.csproj ./LFM.Authorization.Core/
COPY ./LFM.Authorization.Application/*.csproj ./LFM.Authorization.Application/
COPY ./LFM.Authorization.AspNetCore/*.csproj ./LFM.Authorization.AspNetCore/
COPY ./LFM.Authorization.Repository/*.csproj ./LFM.Authorization.Repository/

ARG NUGET_PAT=""
ARG NUGET_USER=""

RUN dotnet nuget add source --username "$NUGET_USER" --password "$NUGET_PAT" --name "github" "https://nuget.pkg.github.com/ThijmenBrand-LifeManager/index.json"
RUN dotnet restore LFM.Authorization.Api/LFM.Authorization.Api.csproj

COPY . .

WORKDIR "/app/LFM.Authorization.Api"
RUN dotnet build "LFM.Authorization.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LFM.Authorization.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LFM.Authorization.Api.dll"]
