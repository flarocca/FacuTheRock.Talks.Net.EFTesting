FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TMenos3.NetCore.EntityFramework.Testing.API/TMenos3.NetCore.EntityFramework.Testing.API.csproj", "TMenos3.NetCore.EntityFramework.Testing.API/"]
RUN dotnet restore "TMenos3.NetCore.EntityFramework.Testing.API/TMenos3.NetCore.EntityFramework.Testing.API.csproj"
COPY . .
WORKDIR "/src/TMenos3.NetCore.EntityFramework.Testing.API"
RUN dotnet build "TMenos3.NetCore.EntityFramework.Testing.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TMenos3.NetCore.EntityFramework.Testing.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TMenos3.NetCore.EntityFramework.Testing.API.dll"]