# First stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY src/ClinicaOnline.Application/*.csproj ./src/ClinicaOnline.Application/
COPY src/ClinicaOnline.Core/*.csproj ./src/ClinicaOnline.Core/
COPY src/ClinicaOnline.Domain/*.csproj ./src/ClinicaOnline.Domain/
COPY src/ClinicaOnline.Infrastructure/*.csproj ./src/ClinicaOnline.Infrastructure/
COPY src/ClinicaOnline.Web/*.csproj ./src/ClinicaOnline.Web/
COPY test/ClinicaOnline.Infrastructure.Tests/*.csproj ./test/ClinicaOnline.Infrastructure.Tests/
COPY test/ClinicaOnline.Application.Tests/*.csproj ./test/ClinicaOnline.Application.Tests/
RUN dotnet restore

# Copy everything else and build website
COPY src/ClinicaOnline.Application/. ./src/ClinicaOnline.Application/
COPY src/ClinicaOnline.Core/. ./src/ClinicaOnline.Core/
COPY src/ClinicaOnline.Domain/. ./src/ClinicaOnline.Domain/
COPY src/ClinicaOnline.Infrastructure/. ./src/ClinicaOnline.Infrastructure/
COPY src/ClinicaOnline.Web/. ./src/ClinicaOnline.Web/
COPY test/ClinicaOnline.Infrastructure.Tests/. ./test/ClinicaOnline.Infrastructure.Tests/
COPY test/ClinicaOnline.Application.Tests/. ./test/ClinicaOnline.Application.Tests/
WORKDIR /app/src/ClinicaOnline.Web
RUN dotnet publish -c release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ClinicaOnline.Web.dll"]