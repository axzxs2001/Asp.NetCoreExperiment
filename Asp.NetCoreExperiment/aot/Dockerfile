# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY AOTDemo04_docker/*.csproj .
RUN dotnet restore -r linux-x64

# copy and publish app and libraries
COPY AOTDemo04_docker/. .
RUN dotnet publish -r linux-x64 --no-restore -o /app


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
USER $APP_UID
ENTRYPOINT ["./AOTDemo04_docker"]