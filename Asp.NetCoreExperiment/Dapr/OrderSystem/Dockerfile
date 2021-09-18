FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/OrderSystem/OrderSystem.csproj", "OrderSystem/"]
RUN dotnet restore "OrderSystem/OrderSystem.csproj"
COPY . .
WORKDIR "/src/OrderSystem"
RUN dotnet build "OrderSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderSystem.dll"]