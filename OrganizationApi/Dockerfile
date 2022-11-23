#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["OrganizationApi/OrganizationApi.csproj", "OrganizationApi/"]
COPY ["UserApi.Contract/UserApi.Contract.csproj", "UserApi.Contract/"]
RUN dotnet restore "OrganizationApi/OrganizationApi.csproj"
COPY . .
WORKDIR "/src/OrganizationApi"
RUN dotnet build "OrganizationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrganizationApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrganizationApi.dll"]