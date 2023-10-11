![EFxceptions](https://raw.githubusercontent.com/hassanhabib/EFxceptions/master/EFxceptions.Shared/Resources/images/github_logo.png")


[![.NET](https://github.com/hassanhabib/EFxceptions/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hassanhabib/EFxceptions/actions/workflows/dotnet.yml)
[![The Standard - COMPLIANT](https://img.shields.io/badge/The_Standard-COMPLIANT-2ea44f)](https://github.com/hassanhabib/The-Standard)
[![The Standard](https://img.shields.io/github/v/release/hassanhabib/The-Standard?style=default&label=Standard%20Version&color=2ea44f)](https://github.com/hassanhabib/The-Standard)
[![The Standard Community](https://img.shields.io/discord/934130100008538142?color=%237289da&label=The%20Standard%20Community&logo=Discord)](https://discord.gg/vdPZ7hS52X)

# EFxceptions

[![preview version](https://img.shields.io/nuget/vpre/EFxceptions)](https://www.nuget.org/packages/EFxceptions/absoluteLatest)
![Nuget](https://img.shields.io/nuget/dt/EFxceptions?color=blue&label=Downloads)

We have designed and developed this library as a wrapper around the existing EntityFramework DbContext implementation to provide the following values:

1. Meaningful Exceptions for SQL error codes.
2. Simplified integrations
3. Test-friendly implementation.

# EFxeptions.Identity
[![preview version](https://img.shields.io/nuget/vpre/EFxceptions.Identity)](https://www.nuget.org/packages/EFxceptions.Identity/absoluteLatest)
![Nuget](https://img.shields.io/nuget/dt/EFxceptions.Identity?color=blue&label=Downloads)

A dedicated EFxeptions port that provides an `EFxceptionContext` that inherits from `Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext` to inherit from, to support Microsoft ASP.Core Identity using EF Core.
Available in the [EFxceptions.Identity](https://www.nuget.org/packages/EFxceptions.Identity) package.

## Standard-Promise
The most important fulfillment aspect in a Standard complaint system is aimed towards contributing to people, its evolution, and principles.
An organization that systematically honors an environment of learning, training, and sharing knowledge is an organization that learns from the past, makes calculated risks for the future, 
and brings everyone within it up to speed on the current state of things as honestly, rapidly, and efficiently as possible. 
 
We believe that everyone has the right to privacy, and will never do anything that could violate that right.
We are committed to writing ethical and responsible software, and will always strive to use our skills, coding, and systems for the good.
We believe that these beliefs will help to ensure that our software(s) are safe and secure and that it will never be used to harm or collect personal data for malicious purposes.
 
The Standard Community as a promise to you is in upholding these values.

## Installation 
You can get EFxceptions [Nuget](https://www.nuget.org/packages/EFxceptions/) package by typing:
```powershell
Install-Package EFxceptions
```

## Integration
Replace your existing ```DbContext``` class with ```EFxceptionsContext``` (or your `IdentityDbContext` with `EFxeption.EFxceptionIdentityContext`) as follows:
### Before:
```csharp
	public partial class StorageBroker : DbContext, IStorageBroker
	{
		public StorageBroker(DbContextOptions<StorageBroker> options)
			: base(options) => this.Database.Migrate();
	}
```
### After:
```csharp
	public partial class StorageBroker : EFxceptionsContext, IStorageBroker
	{
		public StorageBroker(DbContextOptions<StorageBroker> options)
			: base(options) => this.Database.Migrate();
	}
```
## Supported SQL Error Codes
SQL server supports over [41,000 error codes](https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-events-and-errors?view=sql-server-ver15), here's the codes that this library supports so far:

|Code|Meanings|Exception|
|--- |--- |--- |
|207|Invalid column name '%.*ls'.|InvalidColumnNameException|
|208|Invalid object name '%.*ls'.|InvalidObjectNameException|
|547|The %ls statement conflicted with the %ls constraint "%.*ls". The conflict occurred in database "%.*ls", table "%.*ls"%ls%.*ls%ls.|ForeignKeyConstraintConflictException|
|2627|Violation of %ls constraint '%.*ls'. Cannot insert duplicate key in object '%.*ls'.|DuplicateKeyException|

### If you have any suggestions, comments or questions, please feel free to contact me on:

[Twitter](https://twitter.com/hassanrezkhabib)

[LinkedIn](https://www.linkedin.com/in/hassanrezkhabib/)

[E-Mail](mailto:hassanhabib@live.com)

### Important Notice
This library is forever growing as we add more exceptions and codes into it, we appreciate any contributions as there are so many codes we need to cover, so please stay tuned.
