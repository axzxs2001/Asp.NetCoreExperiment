FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PaymentSystem/PaymentSystem.csproj", "PaymentSystem/"]
RUN dotnet restore "PaymentSystem/PaymentSystem.csproj"
COPY . .
WORKDIR "/src/PaymentSystem"
RUN dotnet build "PaymentSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentSystem.dll"]