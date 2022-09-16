#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Microservices.Api.Search/Microservices.Api.Search.csproj", "Microservices.Api.Search/"]
RUN dotnet restore "Microservices.Api.Search/Microservices.Api.Search.csproj"
COPY . .
WORKDIR "/src/Microservices.Api.Search"
RUN dotnet build "Microservices.Api.Search.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Api.Search.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Api.Search.dll"]