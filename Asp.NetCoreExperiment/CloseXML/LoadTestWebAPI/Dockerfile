FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Asp.NetCoreExperiment/CloseXML/LoadTestWebAPI/LoadTestWebAPI.csproj", "Asp.NetCoreExperiment/CloseXML/LoadTestWebAPI/"]
RUN dotnet restore "Asp.NetCoreExperiment/CloseXML/LoadTestWebAPI/LoadTestWebAPI.csproj"
COPY . .
WORKDIR "/src/Asp.NetCoreExperiment/CloseXML/LoadTestWebAPI"
RUN dotnet build "LoadTestWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoadTestWebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN apt-get update -y
RUN apt-get install procps -y
RUN dotnet tool install  --tool-path /app/tools  dotnet-counters -y

RUN apt-get install -y --allow-unauthenticated libgdiplus libc6-dev
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoadTestWebAPI.dll"]



