#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices/UsersService/UsersService.API/UsersService.API.csproj", "Microservices/UsersService/UsersService.API/"]
COPY ["Microservices/Common/Common.csproj", "Microservices/Common/"]
COPY ["Microservices/UsersService/UsersService.Application/UsersService.Application.csproj", "Microservices/UsersService/UsersService.Application/"]
COPY ["Microservices/UsersService/UsersService.Domain/UsersService.Domain.csproj", "Microservices/UsersService/UsersService.Domain/"]
COPY ["Microservices/UsersService/UsersService.Infrastructure.Persistence/UsersService.Infrastructure.Persistence.csproj", "Microservices/UsersService/UsersService.Infrastructure.Persistence/"]
RUN dotnet restore "Microservices/UsersService/UsersService.API/UsersService.API.csproj"
COPY . .
WORKDIR "/src/Microservices/UsersService/UsersService.API"
RUN dotnet build "UsersService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UsersService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersService.API.dll"]