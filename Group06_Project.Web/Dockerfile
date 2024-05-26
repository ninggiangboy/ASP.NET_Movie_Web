FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Group06_Project.Web/Group06_Project.Web.csproj", "Group06_Project.Web/"]
RUN dotnet restore "Group06_Project.Web/Group06_Project.Web.csproj"
COPY . .
WORKDIR "/src/Group06_Project.Web"
RUN dotnet build "Group06_Project.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Group06_Project.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Group06_Project.Web.dll"]
