# Build and publish a release
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder

WORKDIR /app

COPY . ./
RUN dotnet restore src/EasyVPN.Api/*.csproj
RUN dotnet publish src/EasyVPN.Api/*.csproj -c Release -o build --no-restore

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runner

WORKDIR /usr/local/src/EasyVPN.Api
COPY --from=builder /app/build .

ENTRYPOINT ["dotnet", "EasyVPN.Api.dll"]