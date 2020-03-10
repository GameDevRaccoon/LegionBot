#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim-arm32v7 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster-arm32v7 AS build
WORKDIR /src
COPY ["LegionBot.csproj", ""]
RUN dotnet restore
COPY . .
WORKDIR "/src/."
RUN dotnet build "LegionBot.csproj" -c Release -o /app/build

WORKDIR /testModulesrc
COPY ["TestModule.csproj", ""]
RUN donet restore
COPY . .
WORKDIR "/testModulesrc/."
RUN dotnet build "TestModule.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LegionBot.csproj" -c Release -o /app/publish

RUN dotnet publish "TestModule.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LegionBot.dll"]
