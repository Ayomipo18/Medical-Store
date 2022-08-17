<h1  align="center">MedicalStore API </h1>

A WebApi for an medical store built with ASP.NET Core

* Admin and Customers are the users on the app

* Only Admin can create and delete products and categories

* Only Customers can order a product

## How to run

* Clone repo

* Open solution with visual studio

* Make sure sql server is installed(2019 is preferable)

* Set up your appsettings.development.json

HangFireSettings is not required to run locally, so you can skip

```bash
  "ConnectionStrings": {
    "sqlConnection": "server=.; database=MedicalStore; Integrated Security=true"
  },
  "JwtSettings": {
    "validIssuer": "MedicalStoreAPI",
    "validAudience": "https://localhost:7001",
    "secret":  "your secret key",
    "expires": 7
  },
  "Azure": {
    "blobConnectionString": "your azure storage connection string key",
    "blobContainerName": "your azure storage container name"
  },
  "HangFireSettings": {
    "Username": "your hangfire username",
    "Password": "your hangfire password"
  },
  "SENDER_EMAIL": "your sender email",
  "SENDGRID_KEY": "your sendgrid key"
```
* Run Migration

## With Package Manager Console
```bash
    Add-Migration InitialMigration
```
```bash
    Update-Database
```
## With dotnet Command Line Interface
```bash
    dotnet ef migrations add InitialMigration
```
```bash
    dotnet ef database update
```

* Run the app