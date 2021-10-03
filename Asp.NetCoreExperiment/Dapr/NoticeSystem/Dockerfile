FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NoticeSystem/NoticeSystem.csproj", "NoticeSystem/"]
RUN dotnet restore "NoticeSystem/NoticeSystem.csproj"
COPY . .
WORKDIR "/src/NoticeSystem"
RUN dotnet build "NoticeSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NoticeSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NoticeSystem.dll"]