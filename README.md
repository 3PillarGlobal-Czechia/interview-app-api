# Interview App - API

![.NET build and test](https://github.com/3PillarGlobal-Ostrava/interview-app-api/workflows/.NET%20build%20and%20test/badge.svg)  ![Release](https://github.com/3PillarGlobal-Ostrava/interview-app-api/workflows/Release%20app/badge.svg)

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

## Code standards and formatting

### CodeMaid
- we use a Visual Studio extension called [CodeMaid](https://www.codemaid.net/).
- there is a configuration file attached to this project
- for more information and setup check [CodeMaid setup guide](resources/codemaid-config/CODEMAID.md).

## Contribution
- I will be very happy for any help and contribution :blush:
- For more information check [Contribution guide](/CONTRIBUTING.md). 
