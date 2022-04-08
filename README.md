# Interview App - API

![.NET build and test](https://github.com/3PillarGlobal-Ostrava/interview-app-api/workflows/.NET%20build%20and%20test/badge.svg)

[![Deploy to an Azure Web App](https://github.com/3PillarGlobal-Czechia/interview-app-api/actions/workflows/dotnet-deploy.yml/badge.svg)](https://github.com/3PillarGlobal-Czechia/interview-app-api/actions/workflows/dotnet-deploy.yml)

## Documentation

- [API Documentation](docs/API_DOCUMENTATION.md)
- [Architecture](docs/ARCHITECTURE.md)
- [Code Quality](docs/CODE_QUALITY.md)
- [Environments](docs/ENVIRONMENTS.md)


## Build Dependencies

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

To verify your .NET 6 installation run the following in your terminal:

```bash
dotnet --version
```

## How to run

```
cd ./src/WebApi/WebApi
dotnet run
```

## How to create and seed the database

Install the `dotnet-ef` tool if you don't have it:

```
dotnet tool install --global dotnet-ef
```

Run a Database Update:

```
dotnet ef database update --startup-project .\src\WebApi\WebApi\
```

## Contribution
- I will be very happy for any help and contribution :blush:
- For more information check [Contribution guide](/CONTRIBUTING.md). 
