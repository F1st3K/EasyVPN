# Build and publish a release
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

WORKDIR /app

COPY . ./
RUN dotnet restore src/EasyZsV.Api/*.csproj
RUN dotnet publish src/EasyZsV.Api/*.csproj -c Release -o build --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runner

WORKDIR /usr/local/src/EasyZsV.Api
COPY --from=builder /app/build .

ENTRYPOINT ["dotnet", "EasyZsV.Api.dll"]
