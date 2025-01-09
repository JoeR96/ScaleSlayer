FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ScaleSlayer.Web/ScaleSlayer.Web.Server/ScaleSlayer.Web.Server.csproj", "ScaleSlayer.Web/ScaleSlayer.Web.Server/"]
COPY ["ScaleSlayer.Domain/ScaleSlayer.Domain.csproj", "ScaleSlayer.Domain/"]
COPY ["ScaleSlayer.Application/ScaleSlayer.Application.csproj", "ScaleSlayer.Application/"]
COPY ["ScaleSlayer.Infrastructure/ScaleSlayer.Infrastructure.csproj", "ScaleSlayer.Infrastructure/"]
RUN dotnet restore "ScaleSlayer.Web/ScaleSlayer.Web.Server/ScaleSlayer.Web.Server.csproj"
COPY . .
WORKDIR "/src/ScaleSlayer.Web/ScaleSlayer.Web.Server"
RUN dotnet build "ScaleSlayer.Web.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ScaleSlayer.Web.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScaleSlayer.Web.Server.dll"]
