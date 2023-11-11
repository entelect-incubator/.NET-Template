#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["../NuGet.Config", "."]
COPY ["../Api/Api.csproj", "Api/"]
COPY ["../Common/Common.csproj", "Common/"]
COPY ["../Services/Services.csproj", "Services/"]
COPY ["../DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["../DataAccess.Contracts/DataAccess.Contracts.csproj", "DataAccess.Contracts/"]
RUN dotnet restore "Api/Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]