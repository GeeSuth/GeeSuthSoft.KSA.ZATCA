FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Test/GeeSuthSoft.KSA.ZATCA.WebApiTest/GeeSuthSoft.KSA.ZATCA.WebApiTest.csproj", "Test/GeeSuthSoft.KSA.ZATCA.WebApiTest/"]
RUN dotnet restore "Test/GeeSuthSoft.KSA.ZATCA.WebApiTest/GeeSuthSoft.KSA.ZATCA.WebApiTest.csproj"
COPY . .
WORKDIR "/src/Test/GeeSuthSoft.KSA.ZATCA.WebApiTest"
RUN dotnet build "GeeSuthSoft.KSA.ZATCA.WebApiTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GeeSuthSoft.KSA.ZATCA.WebApiTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeeSuthSoft.KSA.ZATCA.WebApiTest.dll"]
