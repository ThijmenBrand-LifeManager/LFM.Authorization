FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./LFM.Authorization/LFM.Authorization.Endpoint/*.csproj ./LFM.Authorization.Endpoint/
COPY ./LFM.Authorization/LFM.Authorization.Application/*.csproj ./LFM.Authorization.Application/
COPY ./LFM.Authorization/LFM.Authorization.Repository/*.csproj ./LFM.Authorization.Repository/
COPY ./LFM.Authorization/LFM.Authorization.AspNetCore/*.csproj ./LFM.Authorization.AspNetCore/
COPY ./LFM.Authorization/LFM.Authorization.Core/*.csproj ./LFM.Authorization.Core/

ARG NUGET_PAT=""
ARG NUGET_USER=""

RUN dotnet nuget add source --username "$NUGET_USER" --password "$NUGET_PAT" --name "github" "https://nuget.pkg.github.com/ThijmenBrand-LifeManager/index.json"
RUN dotnet restore LFM.Authorization.Endpoint/LFM.Authorization.Endpoint.csproj

WORKDIR "/src/LFM.Authorization.Endpoint"

COPY . .
RUN dotnet build "LFM.Authorization.Endpoint.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LFM.Authorization.Endpoint.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LFM.Authorization.Endpoint.dll"]
