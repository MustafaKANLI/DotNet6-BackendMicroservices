#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices/AuthService/AuthService.API/AuthService.API.csproj", "Microservices/AuthService/AuthService.API/"]
COPY ["Microservices/AuthService/AuthService.Application/AuthService.Application.csproj", "Microservices/AuthService/AuthService.Application/"]
COPY ["Microservices/AuthService/AuthService.Domain/AuthService.Domain.csproj", "Microservices/AuthService/AuthService.Domain/"]
COPY ["Microservices/Common/Common.csproj", "Microservices/Common/"]
COPY ["Microservices/AuthService/AuthService.Infrastructure.Persistence/AuthService.Infrastructure.Persistence.csproj", "Microservices/AuthService/AuthService.Infrastructure.Persistence/"]
RUN dotnet restore "Microservices/AuthService/AuthService.API/AuthService.API.csproj"
COPY . .
WORKDIR "/src/Microservices/AuthService/AuthService.API"
RUN dotnet build "AuthService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.API.dll"]
