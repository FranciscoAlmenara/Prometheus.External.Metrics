#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["sample/PrometheusNetSampleApi/PrometheusNetSampleApi.csproj", "sample/PrometheusNetSampleApi/"]
RUN dotnet restore "sample/PrometheusNetSampleApi/PrometheusNetSampleApi.csproj"
COPY . .
WORKDIR "/src/sample/PrometheusNetSampleApi"
RUN dotnet build "PrometheusNetSampleApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrometheusNetSampleApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrometheusNetSampleApi.dll"]