#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices/OtherService/OtherService.API/OtherService.API.csproj", "Microservices/OtherService/OtherService.API/"]
COPY ["Microservices/OtherService/OtherService.Application/OtherService.Application.csproj", "Microservices/OtherService/OtherService.Application/"]
COPY ["Microservices/Common/Common.csproj", "Microservices/Common/"]
COPY ["Microservices/OtherService/OtherService.Domain/OtherService.Domain.csproj", "Microservices/OtherService/OtherService.Domain/"]
COPY ["Microservices/OtherService/OtherService.Infrastructure.Persistence/OtherService.Infrastructure.Persistence.csproj", "Microservices/OtherService/OtherService.Infrastructure.Persistence/"]
RUN dotnet restore "Microservices/OtherService/OtherService.API/OtherService.API.csproj"
COPY . .
WORKDIR "/src/Microservices/OtherService/OtherService.API"
RUN dotnet build "OtherService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OtherService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OtherService.API.dll"]
