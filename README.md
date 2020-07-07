

<p align="center">
  <img width="25%" height="25%" src="https://github.com/hassanhabib/EFxceptions/blob/master/EFxceptions.Shared/Resources/EFxceptions.png?raw=true">
</p>

# EFxceptions
We have designed and developed this library as a wrapper around the existing EntityFramework DbContext implementation to provide the following values:

<ol>
	<li>Meaningful Exceptions for SQL error codes.</li>
	<li>Simplified integrations</li>
	<li>Test-friendly implementation.</li>
</ol>

<br>

# EFxeptions.Identity

A dedicated EFxeptions port that provides an `IDentityDbContext` to inherit from, support Microsoft ASP.Core Identity using EF Core.
Available in the [EFxceptions.Identity](https://www.nuget.org/packages/EFxceptions.Identity) package.

## Installation 
You can get EFxceptions [Nuget](https://www.nuget.org/packages/EFxceptions/) package by typing:
```powershell
Install-Package EFxceptions
```

<br>

## Integration
Replace your existing ```DbContext``` class with ```EFxceptionsContext``` (or `EFxeption.IDentityDbContext`) as follows:

#### Before:
 
```csharp
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        public StorageBroker(DbContextOptions<StorageBroker> options)
            : base(options) => this.Database.Migrate();
    }

```

#### After:
```csharp
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        public StorageBroker(DbContextOptions<StorageBroker> options)
            : base(options) => this.Database.Migrate();
    }

```

<br>

## Supported SQL Error Codes
SQL server supports over [41,000 error codes](https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-events-and-errors?view=sql-server-ver15), here's the codes that this library supports so far:


|Code|Meanings|Exception|
|--- |--- |--- |
|207|Invalid column name '%.*ls'.|InvalidColumnNameException|
|208|Invalid object name '%.*ls'.|InvalidObjectNameException|
|547|The %ls statement conflicted with the %ls constraint "%.*ls". The conflict occurred in database "%.*ls", table "%.*ls"%ls%.*ls%ls.|ForeignKeyConstraintConflictException|
|2627|Violation of %ls constraint '%.*ls'. Cannot insert duplicate key in object '%.*ls'.|DuplicateKeyException|


<br >

This library is forever growing as we add more exceptions and codes into it, we appreciate any contributions as there are so many codes we need to cover, so please stay tuned.


<br />

If you have any suggestions, comments or questions, please feel free to contact me on:
<br />
Twitter: @hassanrezkhabib
<br />
LinkedIn: hassanrezkhabib
<br />
E-Mail: hassanhabib@live.com
<br />
