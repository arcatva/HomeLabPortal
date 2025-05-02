FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HomeLabDashboard.csproj", "./"]
RUN dotnet restore "HomeLabDashboard.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "HomeLabDashboard.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HomeLabDashboard.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false


FROM node:22 AS node
WORKDIR /app
COPY --from=build /src/homelab-dashboard-spa ./
RUN yarn install
RUN yarn build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=node /app/dist ./wwwroot
ENTRYPOINT ["dotnet", "HomeLabDashboard.dll"]
