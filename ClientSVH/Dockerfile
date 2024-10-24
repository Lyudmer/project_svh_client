# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio 
#использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081



# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClientSVH/ClientSVH.csproj", "ClientSVH/"]
COPY ["ClienSVH.SendReceivServer/ClientSVH.SendReceivServer.csproj", "ClienSVH.SendReceivServer/"]
COPY ["ClientSVH.Application/ClientSVH.Application.csproj", "ClientSVH.Application/"]
COPY ["ClientSVH.Core/ClientSVH.Core.csproj", "ClientSVH.Core/"]
COPY ["ClientSVH.DocsRecordCore/ClientSVH.DocsRecordCore.csproj", "ClientSVH.DocsRecordCore/"]
COPY ["ClientSVH.DataAccess/ClientSVH.DataAccess.csproj", "ClientSVH.DataAccess/"]
COPY ["ClientSVH.DocsRecordDateAccess/ClientSVH.DocsRecordDataAccess.csproj", "ClientSVH.DocsRecordDateAccess/"]
COPY ["ClienSVH.XMLParser/ClienSVH.XMLParser.csproj", "ClienSVH.XMLParser/"]
COPY ["ClientSVH.Infrastructure/ClientSVH.Infrastructure.csproj", "ClientSVH.Infrastructure/"]
RUN dotnet restore "./ClientSVH/ClientSVH.csproj"
COPY . .
WORKDIR "/src/ClientSVH"
RUN dotnet build "./ClientSVH.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ClientSVH.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet", "ClientSVH.dll"]

#FROM  postgres:latest
#ADD ./initdb.sql /docker-entrypoint-initdb.d/
