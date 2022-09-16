#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Microservices.Api.Products/Microservices.Api.Products.csproj", "Microservices.Api.Products/"]
RUN dotnet restore "Microservices.Api.Products/Microservices.Api.Products.csproj"
COPY . .
WORKDIR "/src/Microservices.Api.Products"
RUN dotnet build "Microservices.Api.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Api.Products.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Api.Products.dll"]