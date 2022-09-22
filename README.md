# BagStore

## Project Structure

```
/src
- ApplicationCore
- Infrastructure
- Web

/tests
- UnitTests
```

## Packages
```
/ApplicationCore
Install-Package Ardalis.Specification


/Infrastructure
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore

/Web
Install-package Npgsql.EntityFrameworkCore.PostgreSQL

```
## Migrations
```
/Infrastructure
Add-Migration IdentityInitialCreate -Context AppIdentityDbContext -OutputDir "Identity\Migrations"
Update-Database -Context AppIdentityDbContext

Add-Migration BagStoreInitialCreate -Context BagStoreContext -OutputDir "Data\Migrations"
Update-Database -Context BagStoreContext

```
