
# Gateway Manager API

Small test project


## Environment Variables

To run the main GatewayManager.API project, you must search within this project for the Properties/launchSettings.json file, you must modify the following environment variables with the values corresponding to your database connection. The database name can be whatever you want. You can create the empty database and run the migrate-database.ps1 script (Read Migrate DB below) or if you prefer try loading the database backup directly.

`DATABASE_CONNECTION`


## Previous Requirements

Before you begin, make sure you have completed the following items

- You must have [.NET Core SDK 8](https://dotnet.microsoft.com/download) - Version 8.0.0
- You must have [MSSQL] - Version 15
- Right click on the Solution and Restore NuGet Packages, Clean Solution and Rebuild Solution
- See the Environment Variables section
- Run the following command in the Package Management Console to run the migration ./migrate-database.ps1 "Server=*** Database=*** TrustServerCertificate=True Trusted_Connection=True;"
- Upload the GatewayManager.API.json file to Postman
- See TestingAPI.docx

## API Reference

Use the swagger incorporated in the project for better understanding

#### Get all Gateway

```https
  GET /api/Gateway
```

#### Create Gateway

```https
  POST /api/Gateway
```

#### Update Gateway

```https
  PUT /api/Gateway
```
#### Get Gateway By Id

```https
  GET /api/Gateway/{id}
```

#### Delete Gateway By Id

```https
  DELETE /api/Gateway/{id}
```

#### Get Gateway using Pagination

```https
  GET /api/Gateway/GetPage
```


## Available Scripts

### Create migration
To create a new migration run the following PowerShell script.
```powershell
./create-migration.ps1 "Migration name"
```
### Migrate DB
To migrate DB run the following PowerShell script.
```powershell
./migrate-database.ps1 "Server=*** Database=*** TrustServerCertificate=True Trusted_Connection=True;"
```


## Authors

- [@amedif87](https://www.github.com/amedif87)


## Architecture
Architecture pattern: [Clean Architecture with DDD by Microsoft](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)