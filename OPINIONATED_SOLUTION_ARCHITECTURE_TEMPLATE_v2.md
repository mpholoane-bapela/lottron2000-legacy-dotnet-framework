# Opinionated .NET Solution Architecture Template v2

**Version:** 2.0  
**Based on:** Lottron2000 Solution  
**Target Framework:** .NET 10+  
**Last Updated:** 20xx-01-01  
**Changes in v2:** Added local NuGet repository configuration and runtime package discovery

---

## Table of Contents

1. [Solution Structure Overview](#solution-structure-overview)
2. [Project Types and Dependencies](#project-types-and-dependencies)
3. [Naming Conventions](#naming-conventions)
4. [Local NuGet Package Configuration](#local-nuget-package-configuration) ? **NEW**
5. [Domain Layer Patterns](#domain-layer-patterns)
6. [Infrastructure Layer Patterns](#infrastructure-layer-patterns)
7. [Application Services Layer](#application-services-layer)
8. [Composition Root (DI Configuration)](#composition-root-di-configuration)
9. [Design-Time Tools](#design-time-tools)
10. [Test Structure](#test-structure)
11. [Code Generation Templates](#code-generation-templates)
12. [Migration & Modernization Workflow](#migration--modernization-workflow)

---

## Solution Structure Overview

### Folder Hierarchy

```
<SolutionName>/
??? A_Src/
?   ??? 00_Common/
?   ?   ??? <Solution>.CompositionRoot/
?   ??? 01_Domain/
?   ?   ??? <Solution>.Domain/
?   ??? 02_Infrastructure/
?   ?   ??? <Solution>.Infra.DbAccess/
?   ?   ??? <Solution>.Infra.Utilities/
?   ??? 03_AppServices/
?   ?   ??? <Solution>.AppServices/
?   ??? 04_UI/
?       ??? <Solution>.Infra.DsgnTmTls/ (ConsoleApp for EF migrations)
??? B_Tests/
?   ??? 01_Unit/
?   ?   ??? <Solution>.Domain.UnitTests/
?   ??? 02_Integration/
?       ??? <Solution>.Infra.Utilities.IntTests/
?       ??? <Solution>.AppServices.IntTests/
??? C_Benchmarks/
?   ??? <Solution>.Benchmarks/ (optional)
??? nuget.config ? **NEW** - Local NuGet source configuration
```

### Key Characteristics

- **Numbered prefixes** (A_, B_, C_, 00_, 01_, etc.) enforce build order and organization
- **Solution-level segregation** between source (`A_Src`), tests (`B_Tests`), and benchmarks (`C_Benchmarks`)
- **Layered architecture**: Domain ? Infrastructure ? AppServices ? UI
- **All projects target .NET 10+** with `ImplicitUsings` enabled
- **Local NuGet repository** for Alexis packages at `C:\WORK\00_Nuget_Repos\Final_Packages\Alexis`

---

## Project Types and Dependencies

### Project Dependency Graph

```
???????????????????????????????????????????????????????????????
?                     <Solution>.Domain                       ?
?         Depends on: Alexis.CrossCutters (local)            ?
???????????????????????????????????????????????????????????????
                           ?
        ???????????????????????????????????????
        ?                                     ?
????????????????????              ???????????????????????
? <Solution>.Infra ?              ?  <Solution>.Infra.  ?
?    .DbAccess     ?              ?    Utilities        ?
? Alexis.Infra.    ?              ? Alexis.Infra.       ?
? DatabaseAccess   ?              ? General (local)     ?
????????????????????              ???????????????????????
        ?                                    ?
        ??????????????????????????????????????
                       ?
            ???????????????????????
            ? <Solution>.AppServices?
            ???????????????????????
                       ?
        ???????????????????????????????????
        ?                                 ?
????????????????????          ???????????????????????
? <Solution>.      ?          ? <Solution>.Infra.   ?
? CompositionRoot  ?          ?   DsgnTmTls         ?
? Autofac (public) ?          ? EF Core (public)    ?
????????????????????          ???????????????????????
```

### Required Alexis Packages (from Local NuGet)

| Package Name | Target Projects | Typical Version Format |
|--------------|----------------|------------------------|
| `Alexis.CrossCutters` | Domain | `2025.11.26.1854` |
| `Alexis.Infrastructure.DatabaseAccess` | Infra.DbAccess | `2025.11.26.1841` |
| `Alexis.Infrastructure.General` | Infra.Utilities, CompositionRoot | `2025.11.26.1841` |

**Note:** Versions follow the pattern: `YYYY.MM.DD.HHmm` (timestamp-based versioning)

### Project Definitions

#### 1. `<Solution>.Domain` (Class Library)
- **Purpose:** Core business logic, entities, value objects, domain services
- **Dependencies:** 
  - Local: `Alexis.CrossCutters` (from `C:\WORK\00_Nuget_Repos\Final_Packages\Alexis`)
- **Key Folders:**
  - `00_Constants/` - Domain constants
  - `01_Logic/` - Domain services and business logic
  - `02_Models/`
    - `00_Logic/` - Value objects, DTOs used in logic
    - `01_Entities/` - Database entities (organized by bounded context)
    - `02_DTOs/` - Data transfer objects

#### 2. `<Solution>.Infra.DbAccess` (Class Library)
- **Purpose:** Entity Framework Core data access layer
- **Dependencies:** 
  - Project: `<Solution>.Domain`
  - Local: `Alexis.Infrastructure.DatabaseAccess` (from local NuGet)
  - Public: `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.SqlServer`
- **Key Folders:**
  - `DbContext/` - AppDbContext and configuration
    - `Configuration/`
      - `Tables/` - EF entity configurations (`*_DbConfig.cs`)
      - `DbModelsConfiguration.cs` - Master configurator
  - `Data_Accessors/` - Repository pattern implementations
    - `<BoundedContext>/`
      - `<Entity>_DA/`
        - `<Entity>DA.cs` - Main data accessor
        - `<Entity>Queries.cs` - Read operations
        - `<Entity>Commands.cs` - Write operations
  - `Migrations/` - EF Core migrations

#### 3. `<Solution>.Infra.Utilities` (Class Library)
- **Purpose:** Cross-cutting infrastructure concerns (logging, caching, external services)
- **Dependencies:** 
  - Project: `<Solution>.Domain`
  - Local: `Alexis.Infrastructure.General` (from local NuGet)
- **Key Folders:**
  - Service-specific folders (e.g., `RandomNumbers/`, `FileHandling/`)

#### 4. `<Solution>.AppServices` (Class Library)
- **Purpose:** Application use cases, orchestration, business workflows
- **Dependencies:** 
  - Project: `<Solution>.Domain`, `<Solution>.Infra.DbAccess`, `<Solution>.Infra.Utilities`
- **Key Folders:**
  - Feature-based organization
  - `00_DataAccessFacade/` - Facade over data accessors (optional)

#### 5. `<Solution>.CompositionRoot` (Class Library)
- **Purpose:** Dependency injection configuration, bootstrapping
- **Dependencies:** 
  - Project: All other projects
  - Local: `Alexis.Infrastructure.General` (from local NuGet)
  - Public: `Autofac`, `Autofac.Extensions.DependencyInjection`
- **Key Folders:**
  - `00_IOC_MainApp/` - Main IoC configuration
    - `IocConfig_MainApp.cs` - Main partial class
    - `Partials/` - Separated configuration concerns
      - `IoC_App_DataAccessors.cs`
      - `IoC_App_BasicServices.cs`
  - `02_driverutils/` - Test/app initialization utilities
    - `DriverUtilities.cs` - Static initialization helper
  - `_Docs/` - Architecture documentation

#### 6. `<Solution>.Infra.DsgnTmTls` (Console App)
- **Purpose:** Design-time tools for EF Core migrations
- **Dependencies:** 
  - Project: `<Solution>.Infra.DbAccess`
  - Public: `Microsoft.EntityFrameworkCore.Design`
- **Key Files:**
  - `Program.cs` - Implements `IDesignTimeDbContextFactory<AppDbContext>`

#### 7. Test Projects (xUnit Test Projects)
- `<Solution>.Domain.UnitTests` - Pure unit tests (no mocks, no DB)
- `<Solution>.Infra.Utilities.IntTests` - Integration tests for utilities
- `<Solution>.AppServices.IntTests` - Full integration tests with DB
- **Dependencies:** 
  - Project: References respective projects under test + `CompositionRoot`
  - Public: `xunit`, `FluentAssertions`, `Microsoft.NET.Test.Sdk`

---

## Naming Conventions

### General Patterns

| Item | Convention | Example |
|------|------------|---------|
| Solution Name | `<DomainName>` or `<ProjectName>` | `Lottron2000` |
| Project Name | `<Solution>.<Layer>[.<Sublayer>]` | `Lottron2000.Infra.DbAccess` |
| Namespace | Same as project name | `namespace Lottron2000.Infra.DbAccess;` |
| Entity Class | PascalCase singular noun | `Inventory`, `SimulatedDraw` |
| Entity ID | `<EntityName>ID` (string, GUID) | `InventoryID`, `SimulatedDrawID` |
| Auto-increment ID | `DbID`, `ItemID`, or `ID` (int) | `DbID`, `ItemID` |
| DbConfig Class | `<EntityName>_DbConfig` | `Inventory_DbConfig` |
| Data Accessor | `<EntityName>DA` | `InventoryDA` |
| Queries Class | `<EntityName>Queries` | `InventoryQueries` |
| Commands Class | `<EntityName>Commands` | `InventoryCommands` |
| DTOs | `<EntityName>_<Type>DTO` | `Inventory_AddDTO`, `Inventory_ViewDTO` |
| Test Class | `<MethodName>_Should` | `GenerateFromPureRandom_Should` |

### Bounded Context Organization

Entities are grouped by domain concepts (bounded contexts):

```
01_Entities/
??? CheckSum/
?   ??? ResultCheckSumSa.cs
?   ??? SALottoResultCheckSum.cs
??? SimulatedDraw/
?   ??? SimulatedDraw.cs
?   ??? SimulatedDrawResult.cs
?   ??? PlayingSession.cs
??? WinningChances/
?   ??? WinningChance.cs
??? WinningPrize/
?   ??? MockWinningPrize.cs
??? zOthers/
?   ??? SALottoResult.cs
??? _ExampleProperEntity/
    ??? Inventory.cs (reference implementation)
```

---

## Local NuGet Package Configuration

### ? Overview

This architecture uses **local Alexis packages** stored at:
```
C:\WORK\00_Nuget_Repos\Final_Packages\Alexis\
```

The scaffolding generator must:
1. **Scan** this directory at runtime
2. **Detect** the latest version of each Alexis package
3. **Install** the appropriate packages to each project
4. **Configure** `nuget.config` with the local source

### Package Naming Convention

Alexis packages follow this naming pattern:
```
<PackageName>.<Version>.nupkg

Examples:
- Alexis.CrossCutters.2025.11.26.1854.nupkg
- Alexis.Infrastructure.DatabaseAccess.2025.11.26.1841.nupkg
- Alexis.Infrastructure.General.2025.11.26.1841.nupkg
```

**Version Format:** `YYYY.MM.DD.HHmm` (timestamp-based)

### nuget.config Template

**Location:** Solution root (same level as `.sln` file)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <!-- Public NuGet feed -->
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
    
    <!-- Local Alexis packages -->
    <add key="LocalAlexis" value="C:\WORK\00_Nuget_Repos\Final_Packages\Alexis" />
  </packageSources>
  
  <packageSourceMapping>
    <packageSource key="nuget.org">
      <package pattern="*" />
    </packageSource>
    <packageSource key="LocalAlexis">
      <package pattern="Alexis.*" />
    </packageSource>
  </packageSourceMapping>
</configuration>
```

### Runtime Package Discovery Algorithm

The scaffolding generator uses this algorithm to find latest packages:

```csharp
// Pseudocode for package discovery
function GetLatestLocalNugetPackages(folderPath)
{
    1. Scan directory for all *.nupkg files
    2. Parse filename using regex: ^(.+?)\.(\d+\.\d+\.\d+\.\d+)\.nupkg$
       - Group 1: PackageName (e.g., "Alexis.CrossCutters")
       - Group 2: Version (e.g., "2025.11.26.1854")
    3. Convert version string to System.Version for comparison
    4. Group by PackageName
    5. For each group, select package with highest Version
    6. Return list of latest packages
}
```

**PowerShell Implementation:**

```powershell
function Get-LatestLocalNugetPackages {
    param([string]$FolderPath)
    
    $packages = Get-ChildItem "$FolderPath\*.nupkg" | ForEach-Object {
        if ($_.Name -match '^(.+?)\.(\d+\.\d+\.\d+\.\d+)\.nupkg$') {
            [PSCustomObject]@{
                PackageName = $Matches[1]
                Version = [Version]$Matches[2]
                VersionString = $Matches[2]
                FullPath = $_.FullName
            }
        }
    }
    
    return $packages | 
        Group-Object PackageName | 
        ForEach-Object {
            $_.Group | Sort-Object Version -Descending | Select-Object -First 1
        }
}
```

### Package Installation Mapping

| Alexis Package | Target Projects |
|----------------|----------------|
| `Alexis.CrossCutters` | `<Solution>.Domain` |
| `Alexis.Infrastructure.DatabaseAccess` | `<Solution>.Infra.DbAccess` |
| `Alexis.Infrastructure.General` | `<Solution>.Infra.Utilities`<br>`<Solution>.CompositionRoot` |

### Installation Commands

After scanning and detecting latest versions:

```powershell
# Example: Installing Alexis.CrossCutters v2025.11.26.1854 to Domain project
dotnet add A_Src\01_Domain\<Solution>.Domain\<Solution>.Domain.csproj `
    package Alexis.CrossCutters `
    --version 2025.11.26.1854 `
    --source C:\WORK\00_Nuget_Repos\Final_Packages\Alexis

# Example: Installing Alexis.Infrastructure.DatabaseAccess to DbAccess project
dotnet add A_Src\02_Infrastructure\<Solution>.Infra.DbAccess\<Solution>.Infra.DbAccess.csproj `
    package Alexis.Infrastructure.DatabaseAccess `
    --version 2025.11.26.1841 `
    --source C:\WORK\00_Nuget_Repos\Final_Packages\Alexis

# Example: Installing Alexis.Infrastructure.General to CompositionRoot
dotnet add A_Src\00_Common\<Solution>.CompositionRoot\<Solution>.CompositionRoot.csproj `
    package Alexis.Infrastructure.General `
    --version 2025.11.26.1841 `
    --source C:\WORK\00_Nuget_Repos\Final_Packages\Alexis
```

### Expected .csproj References

After installation, project files should contain:

**Domain Project:**
```xml
<ItemGroup>
  <PackageReference Include="Alexis.CrossCutters" Version="2025.11.26.1854" />
</ItemGroup>
```

**DbAccess Project:**
```xml
<ItemGroup>
  <PackageReference Include="Alexis.Infrastructure.DatabaseAccess" Version="2025.11.26.1841" />
</ItemGroup>
```

**Utilities Project:**
```xml
<ItemGroup>
  <PackageReference Include="Alexis.Infrastructure.General" Version="2025.11.26.1841" />
</ItemGroup>
```

**CompositionRoot Project:**
```xml
<ItemGroup>
  <PackageReference Include="Autofac" Version="9.0.0" />
  <PackageReference Include="Autofac.Extensions.DependencyInjection" Version="10.0.0" />
  <PackageReference Include="Alexis.Infrastructure.General" Version="2025.11.26.1841" />
</ItemGroup>
```

### Troubleshooting Local NuGet Packages

#### Issue: Package Not Found

**Symptom:**
```
error NU1101: Unable to find package Alexis.CrossCutters
```

**Solutions:**
1. Verify `nuget.config` exists at solution root
2. Check local NuGet path: `C:\WORK\00_Nuget_Repos\Final_Packages\Alexis`
3. Verify `.nupkg` files exist in the folder
4. Clear NuGet cache: `dotnet nuget locals all --clear`
5. Restore packages: `dotnet restore --configfile nuget.config`

#### Issue: Wrong Version Installed

**Symptom:**
Older version installed instead of latest

**Solutions:**
1. Re-run package discovery to get latest versions
2. Remove existing package: `dotnet remove package Alexis.CrossCutters`
3. Reinstall with explicit version and source
4. Check for multiple `.nupkg` files with different versions

#### Issue: Package Source Conflict

**Symptom:**
```
error NU1100: Multiple packages with the ID 'Alexis.CrossCutters' found
```

**Solutions:**
1. Use `packageSourceMapping` in `nuget.config` (shown above)
2. Specify `--source` explicitly in `dotnet add package` commands
3. Clear local NuGet cache

---

## Domain Layer Patterns

### Entity Structure (Ideal Pattern)

**Reference:** See `Inventory.cs` in `_ExampleProperEntity` folder

```csharp
using Alexis.CrossCutters;

namespace <Solution>.Domain;

public class <Entity> : DbEntity_AlexisBase
{
    // Primary Key (string GUID)
    public string <Entity>ID { get; private set; }
    
    // Auto-increment DB ID
    public int DbID { get; private set; }
    
    // Business Properties (private setters for encapsulation)
    public string Title { get; private set; }
    public int Quantity { get; private set; }
    public decimal Amount { get; private set; }
    
    // Version tracking (for properly encapsulated entities)
    public VersionInfo VersionInfo { get; private set; }
    
    // Required: EF Core constructor (parameterless, protected)
    protected <Entity>() { }
    
    // Business constructor (public, with required parameters)
    public <Entity>(int quantity, decimal amount, VersionInfo versionInfo)
    {
        <Entity>ID = Guid.NewGuid().ToString();
        Quantity = quantity;
        Amount = amount;
        VersionInfo = versionInfo;
    }
    
    // Business methods (optional)
    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity < 0)
            throw new ArgumentException("Quantity cannot be negative");
        Quantity = newQuantity;
    }
}
```

### Key Entity Principles

1. **Dual ID Pattern:**
   - String GUID (`<Entity>ID`) as primary key for business logic
   - Integer auto-increment (`DbID`, `ItemID`, or `ID`) for database indexing

2. **Encapsulation:**
   - Properties have `private set` or `init`
   - Business logic in methods, not property setters
   - Validation in constructors and methods

3. **EF Core Requirements:**
   - Protected parameterless constructor for EF
   - Navigation properties marked `virtual` if lazy loading needed

4. **VersionInfo Pattern:**
   - Use `VersionInfo` owned entity for audit trail
   - Contains: `Created`, `Updated`, `PublishStatus`, `UpdatedByUserID`, etc.

---

## Infrastructure Layer Patterns

### DbContext Configuration

#### AppDbContext.cs

```csharp
using Alexis.Infrastructure.DatabaseAccess;
using Microsoft.EntityFrameworkCore;

namespace <Solution>.Infra.DbAccess;

public class AppDbContext : DbContext_AlexisBase
{
    #region BoundedContext1
    public DbSet<Entity1> dbo__Entity1 { get; set; }
    public DbSet<Entity2> dbo__Entity2 { get; set; }
    #endregion
    
    #region BoundedContext2
    public DbSet<Entity3> dbo__Entity3 { get; set; }
    #endregion
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }
    
    public AppDbContext() { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DbModelsConfigurator.ConfigureAll(modelBuilder);
    }
}
```

#### DbModelsConfiguration.cs

```csharp
using Microsoft.EntityFrameworkCore;

namespace <Solution>.Infra.DbAccess;

public static class DbModelsConfigurator
{
    public static void ConfigureAll(ModelBuilder modelBuilder)
    {
        // BoundedContext1 entities
        new Entity1_DbConfig().ConfigureModel(modelBuilder);
        new Entity2_DbConfig().ConfigureModel(modelBuilder);
        
        // BoundedContext2 entities
        new Entity3_DbConfig().ConfigureModel(modelBuilder);
    }
}
```

### Entity Configuration Pattern

#### Base Configuration Class

```csharp
namespace <Solution>.Infra.DbAccess;

public class <Solution>_DbTableConfig_Base
{
    protected string SchemaName = "dbo";
}
```

#### Entity DbConfig Template

**Location:** `DbContext/Configuration/Tables/<Entity>_DbConfig.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using <Solution>.Domain;

namespace <Solution>.Infra.DbAccess;

public class <Entity>_DbConfig : <Solution>_DbTableConfig_Base
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<<Entity>>();
        entity.ToTable("<Entity>", SchemaName);
        
        #region DB ID Configuration
        // Primary Key (string GUID)
        entity.Property(x => x.<Entity>ID).HasMaxLength(50);
        modelBuilder.Entity<<Entity>>().HasKey(p => p.<Entity>ID);
        
        // Auto-increment integer ID
        entity.Property(x => x.DbID).HasColumnName("DbID");
        entity.Property(x => x.DbID).HasColumnType("int");
        
        const int DbID_SEED = 1;
        const int DbID_INCREMENT = 1;
        
        modelBuilder.Entity<<Entity>>()
            .Property(p => p.DbID)
            .UseIdentityColumn(DbID_SEED, DbID_INCREMENT);
        
        modelBuilder.Entity<<Entity>>()
            .Property(u => u.DbID)
            .Metadata
            .SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        #endregion
        
        #region Column Configurations
        entity.Property(x => x.Title).HasMaxLength(200);
        entity.Property(x => x.Amount).HasColumnType("decimal(12,4)");
        entity.Property(x => x.Quantity).HasColumnType("int");
        // ... other properties
        #endregion
        
        #region VersionInfo (Owned Entity Pattern)
        entity.OwnsOne(
            o => o.VersionInfo,
            sa =>
            {
                sa.Property(p => p.Created).HasColumnName("Created");
                sa.Property(p => p.Updated).HasColumnName("Updated");
                sa.Property(p => p.PublishStatus).HasColumnName("PublishStatus");
                sa.Property(x => x.PublishStatus).HasMaxLength(10);
                sa.Property(x => x.UpdatedByUserID).HasColumnName("UpdatedByUserID");
                sa.Property(x => x.UpdatedByUserID).HasMaxLength(50);
                sa.Property(p => p.SystemCreated).HasColumnName("SystemCreated");
                sa.Property(x => x.SystemUpdated).HasColumnName("SystemUpdated");
                sa.ToTable("Bot_99_VersionInfo");
            });
        #endregion
        
        #region Relationships (if any)
        // entity.HasOne<RelatedEntity>()
        //     .WithMany()
        //     .HasForeignKey(x => x.RelatedEntityID);
        #endregion
    }
}
```

### Data Accessor Pattern (Repository)

**Location:** `Data_Accessors/<BoundedContext>/<Entity>_DA/`

#### 1. Main Data Accessor (`<Entity>DA.cs`)

```csharp
using Alexis.Infrastructure.DatabaseAccess;
using System.Threading.Tasks;

namespace <Solution>.Infra.DbAccess;

public class <Entity>DA : DataAccessorBase<<Entity>Queries, <Entity>Commands>
{
    public <Entity>DA(<Entity>Queries queries, <Entity>Commands commands) 
        : base(queries, commands) { }
    
    public <Entity>DA(DbAccessPatternWrapper dbPatternWrapper) 
        : base(dbPatternWrapper) { }
    
    public void DisposeContext() 
    { 
        _dbPatternWrapper.Dispose(); 
    }
    
    public async Task DisposeContextAsync() 
    { 
        await _dbPatternWrapper.DisposeAsync(); 
    }
    
    public <Entity>DA CreateNewInstance<C>() where C : DbContext_AlexisBase
    {
        var daPatternWrapper = Create_DAWrapper<C>();
        return new <Entity>DA(daPatternWrapper);
    }
}
```

#### 2. Queries Class (`<Entity>Queries.cs`)

```csharp
using Alexis.Infrastructure.DatabaseAccess;
using <Solution>.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace <Solution>.Infra.DbAccess;

public class <Entity>Queries
{
    private readonly IGenericDbRepository _repository;
    
    public <Entity>Queries(IGenericDbRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IReadOnlyList<<Entity>>> GetAll()
    {
        return await GetAllQuery().ToListAsync() as IReadOnlyList<<Entity>>;
    }
    
    public async Task<<Entity>> GetByID(string <entity>ID)
    {
        return await GetAllQuery()
            .FirstOrDefaultAsync(x => x.<Entity>ID == <entity>ID);
    }
    
    public async Task<<Entity>> GetByDbID(int dbID)
    {
        return await GetAllQuery()
            .FirstOrDefaultAsync(x => x.DbID == dbID);
    }
    
    private IQueryable<<Entity>> GetAllQuery()
    {
        var itemsQuery = _repository.GetAll<<Entity>>()
            .AsNoTracking();
        return itemsQuery;
    }
    
    // Add custom query methods as needed
}
```

#### 3. Commands Class (`<Entity>Commands.cs`)

```csharp
using Alexis.Infrastructure.DatabaseAccess;
using <Solution>.Domain;

namespace <Solution>.Infra.DbAccess;

public class <Entity>Commands : GenericDbCommandsExec<<Entity>>
{
    public <Entity>Commands(DbAccessPatternWrapper dbPatternWrapper) 
        : base(dbPatternWrapper)
    { 
        _dbPatternWrapper = dbPatternWrapper; 
    }
    
    // Inherits:
    // - Add(entity)
    // - Update(entity)
    // - Delete(entity)
    // - AddRange(entities)
    // - SaveChanges()
    // - SaveChangesAsync()
    
    // Add custom command methods if needed
}
```

---

## Application Services Layer

### Service Structure

```csharp
namespace <Solution>.AppServices;

public class <Feature>Service
{
    private readonly <Entity>DA _<entity>DA;
    private readonly IOtherDependency _otherDependency;
    
    public <Feature>Service(
        <Entity>DA <entity>DA,
        IOtherDependency otherDependency)
    {
        _<entity>DA = <entity>DA;
        _otherDependency = otherDependency;
    }
    
    public async Task<ResultDTO> ExecuteUseCase(InputDTO input)
    {
        // 1. Validate input
        // 2. Orchestrate domain logic
        // 3. Call data accessors
        // 4. Map to DTOs
        // 5. Return result
    }
}
```

### DataAccessFacade Pattern (Optional)

Provides a single entry point to all data accessors:

```csharp
namespace <Solution>.AppServices;

public class DataAccessFacade
{
    public Entity1DA Entity1DA { get; }
    public Entity2DA Entity2DA { get; }
    
    public DataAccessFacade(
        Entity1DA entity1DA,
        Entity2DA entity2DA)
    {
        Entity1DA = entity1DA;
        Entity2DA = entity2DA;
    }
}
```

---

## Composition Root (DI Configuration)

### IocConfigManager_MainApp.cs (Main Partial Class)

**Location:** `00_IOC_MainApp/IocConfigManager_MainApp.cs`

```csharp
using Autofac;

namespace <Solution>.CompositionRoot;

public partial class IocConfigManager_MainApp
{
    #region Members
    protected ContainerBuilder _autoFacBuilder_allServices;
    protected IContainer _autoFacContainer_allServices;
    #endregion
    
    public IocConfigManager_MainApp(string connectionString)
    {
        _autoFacBuilder_allServices = new ContainerBuilder();
        Configure(connectionString);
    }
    
    private void Configure(string connectionString)
    {
        ConfigureDbManagement(connectionString);
        ConfigureCommonServices();
        ConfigureUtilities();
        ConfigureAppServices();
        ConfigureDomain();
    }
    
    public void BuildAllContainers()
    { 
        _autoFacContainer_allServices = _autoFacBuilder_allServices.Build(); 
    }
    
    public T ResolveService<T>()
    { 
        return _autoFacContainer_allServices.Resolve<T>(); 
    }
    
    public IContainer GetContainer()
    {
        return _autoFacContainer_allServices;
    }
}
```

### IoC_App_DataAccessors.cs (Partial)

**Location:** `00_IOC_MainApp/Partials/IoC_App_DataAccessors.cs`

```csharp
using Alexis.Infrastructure.DatabaseAccess;
using Autofac;
using <Solution>.Infra.DbAccess;

namespace <Solution>.CompositionRoot;

public partial class IocConfigManager_MainApp
{
    private void ConfigureDbManagement(string connectionString = null)
    {
        ConfigureDataAccessors(connectionString);
    }
    
    private void ConfigureDataAccessors(string connectionString = null)
    {
        #region DATA ACCESSORS
        
        // Initialize DB Access Pattern
        if (String.IsNullOrEmpty(connectionString))
        { 
            DbAccessDIResolver<AppDbContext>.Initialize(); 
        }
        else
        { 
            DbAccessDIResolver<AppDbContext>.Initialize(connectionString); 
        }
        
        var dbAccessPatternWrapper = DbAccessDIResolver<AppDbContext>.GetAccessPattern();
        
        // Register Data Accessors (grouped by bounded context)
        
        // BoundedContext1 DA classes
        _autoFacBuilder_allServices
            .Register(c => new Entity1DA(dbAccessPatternWrapper))
            .As<Entity1DA>()
            .SingleInstance();
        
        _autoFacBuilder_allServices
            .Register(c => new Entity2DA(dbAccessPatternWrapper))
            .As<Entity2DA>()
            .SingleInstance();
        
        // BoundedContext2 DA classes
        _autoFacBuilder_allServices
            .Register(c => new Entity3DA(dbAccessPatternWrapper))
            .As<Entity3DA>()
            .SingleInstance();
        
        // DataAccessFacade (optional)
        _autoFacBuilder_allServices
            .RegisterType<DataAccessFacade>()
            .As<DataAccessFacade>()
            .SingleInstance();
        
        #endregion
    }
}
```

### IoC_App_BasicServices.cs (Partial)

**Location:** `00_IOC_MainApp/Partials/IoC_App_BasicServices.cs`

```csharp
using Alexis.Infrastructure.General;
using Autofac;
using <Solution>.AppServices;
using <Solution>.Domain;
using <Solution>.Infra.Utilities;

namespace <Solution>.CompositionRoot;

public partial class IocConfigManager_MainApp
{
    private void ConfigureCommonServices()
    {
        var setDateTime = DateTime.Now;
        _autoFacBuilder_allServices
            .Register(c => new StandardDateTimeProvider(setDateTime))
            .As<IDateTimeProvider>();
    }
    
    private void ConfigureUtilities()
    {
        _autoFacBuilder_allServices
            .RegisterType<RandomNumbersGenerator>()
            .As<IRandomNumberGenerator>()
            .SingleInstance();
    }
    
    private void ConfigureAppServices()
    {
        _autoFacBuilder_allServices
            .RegisterType<<Feature>Service>()
            .As<<Feature>Service>()
            .SingleInstance();
    }
    
    private void ConfigureDomain()
    {
        _autoFacBuilder_allServices
            .RegisterType<<DomainService>>()
            .As<<DomainService>>()
            .SingleInstance();
    }
}
```

### DriverUtilities.cs (Test Initialization)

**Location:** `02_driverutils/DriverUtilities.cs`

```csharp
using Alexis.Infrastructure.General;

namespace <Solution>.CompositionRoot;

public static class DriverUtilities
{
    private static IocConfigManager_MainApp _iocConfigManager;
    private static bool _initialized = false;
    
    public static void Initialize()
    {
        AppSettingsJsonReader.Initialize();
        Initialize_Native();
    }
    
    private static void Initialize_Native()
    {
        const string configKey_mainDb = "ConnectionStrings:AppContextConnectionString";
        string connectionString_botDb = AppSettingsJsonReader.GetValue_String(configKey_mainDb);
        
        _iocConfigManager = new IocConfigManager_MainApp(connectionString_botDb);
        _iocConfigManager.BuildAllContainers();
    }
    
    public static IocConfigManager_MainApp GetConfigManager()
    {
        if (_iocConfigManager == null)
            Initialize();
        
        return _iocConfigManager;
    }
}
```

---

## Design-Time Tools

### Program.cs (EF Migrations Console App)

**Project:** `<Solution>.Infra.DsgnTmTls`  
**Location:** `Program.cs`

```csharp
using Alexis.Infrastructure.DatabaseAccess;
using <Solution>.Infra.DbAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace <Solution>.Infra.DsgnTmTls;

class Program : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        DbAccessDIResolver<AppDbContext>.Initialize();
        return DbAccessDIResolver<AppDbContext>.GetDbContext();
    }
    
    static void Main(string[] args)
    {
        Program p = new Program();
        
        using (AppDbContext appDbContext = p.CreateDbContext(null))
        {
            appDbContext.Database.Migrate();
        }
    }
}
```

### Usage

```bash
# Add migration
dotnet ef migrations add <MigrationName> --project A_Src\04_UI\<Solution>.Infra.DsgnTmTls

# Update database
dotnet ef database update --project A_Src\04_UI\<Solution>.Infra.DsgnTmTls

# Or run the console app
dotnet run --project A_Src\04_UI\<Solution>.Infra.DsgnTmTls
```

---

## Test Structure

### Folder Hierarchy

```
<Solution>.<Layer>.Tests.<Type>/
??? 0_BaseTests/
?   ??? BaseTest.cs
?   ??? DependencyInjection.cs (if needed)
??? <BoundedContext>/ (optional, for large solutions)
?   ??? <ClassName>_Tests/
?       ??? <MethodName>/
?           ??? Positive/
?           ?   ??? POS_<MethodName>_Should.cs
?           ??? Negative/
?           ?   ??? NEG_<MethodName>_Should.cs
?           ??? Boundary/
?           ?   ??? BND_<MethodName>_Should.cs
?           ??? Exceptions/
?               ??? EXC_<MethodName>_Should.cs
??? <ClassName>_Tests/
    ??? <MethodName>_Should.cs (simplified structure)
```

### BaseTest.cs (Integration Tests)

**Location:** `0_BaseTests/BaseTest.cs`

```csharp
using <Solution>.CompositionRoot;

namespace <Solution>.AppServices.Tests.Int;

public class BaseTest
{
    protected <Service> _service;
    protected <OtherService> _otherService;
    
    public BaseTest()
    {
        SetUpContext();
    }
    
    protected void SetUpContext()
    {
        DriverUtilities.Initialize();
        var container = DriverUtilities.GetConfigManager();
        
        _service = container.ResolveService<<Service>>();
        _otherService = container.ResolveService<<OtherService>>();
    }
}
```

### Test Class Template

**Location:** `<ClassName>_Tests/<MethodName>_Should.cs`

```csharp
using FluentAssertions;
using <Solution>.Domain;
using <Solution>.CompositionRoot;

namespace <Solution>.AppServices.Tests.Int.<ClassName>_Tests;

/// <summary>
/// Integration tests for <ClassName>.<MethodName>
/// 
/// These tests verify:
/// 1. [Test objective 1]
/// 2. [Test objective 2]
/// 3. [Test objective 3]
/// </summary>
public class <MethodName>_Should : BaseTest
{
    private <Class> _classUnderTest;
    
    public <MethodName>_Should()
    {
        _classUnderTest = new <Class>(_service);
    }
    
    #region Positive Tests - Valid Input
    
    [Fact]
    public async Task ReturnExpectedResult_WhenCalledWithValidInput()
    {
        // ARRANGE
        var input = new InputData();
        
        // ACT
        var result = await _classUnderTest.<MethodName>(input);
        
        // ASSERT
        result.Should().NotBeNull();
        result.Should().BeOfType<ExpectedType>();
    }
    
    #endregion
    
    #region Negative Tests - Invalid Input
    
    [Fact]
    public async Task ThrowArgumentException_WhenCalledWithNullInput()
    {
        // ARRANGE
        InputData input = null;
        
        // ACT
        Func<Task> act = async () => await _classUnderTest.<MethodName>(input);
        
        // ASSERT
        await act.Should().ThrowAsync<ArgumentException>();
    }
    
    #endregion
    
    #region Boundary Tests
    
    [Fact]
    public async Task HandleMinimumValue_WhenCalledWithBoundaryInput()
    {
        // ARRANGE
        var input = new InputData { Value = int.MinValue };
        
        // ACT
        var result = await _classUnderTest.<MethodName>(input);
        
        // ASSERT
        result.Should().NotBeNull();
    }
    
    #endregion
}
```

### Test Project Configuration (csproj)

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.4" />
    <PackageReference Include="FluentAssertions" Version="8.8.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.0.1" />
    <PackageReference Include="xunit" Version="2.9.3" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.5">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\..\A_Src\00_Common\<Solution>.CompositionRoot\<Solution>.CompositionRoot.csproj" />
    <ProjectReference Include="..\..\..\A_Src\03_AppServices\<Solution>.AppServices\<Solution>.AppServices.csproj" />
  </ItemGroup>

  <ItemGroup>
    <None Update="appsettings.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
  </ItemGroup>
</Project>
```

### appsettings.json (Test Projects)

```json
{
  "ConnectionStrings": {
    "AppContextConnectionString": "Server=(localdb)\\mssqllocaldb;Database=<Solution>TestDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

---

## Code Generation Templates

### Quick Reference Commands

#### 1. Create New Entity (Full Stack)

**Domain Entity:**
```bash
# Create entity file
# Location: A_Src/01_Domain/<Solution>.Domain/02_Models/01_Entities/<BoundedContext>/<Entity>.cs
```

**DbConfig:**
```bash
# Create DbConfig file
# Location: A_Src/02_Infrastructure/<Solution>.Infra.DbAccess/DbContext/Configuration/Tables/<Entity>_DbConfig.cs
```

**Data Accessor:**
```bash
# Create DA folder and files
# Location: A_Src/02_Infrastructure/<Solution>.Infra.DbAccess/Data_Accessors/<BoundedContext>/<Entity>_DA/
#   - <Entity>DA.cs
#   - <Entity>Queries.cs
#   - <Entity>Commands.cs
```

**Register in:**
1. `AppDbContext.cs` - Add `DbSet<<Entity>>` property
2. `DbModelsConfiguration.cs` - Add `new <Entity>_DbConfig().ConfigureModel(modelBuilder);`
3. `IoC_App_DataAccessors.cs` - Register `<Entity>DA` with Autofac

#### 2. Create New Use Case

**AppService:**
```bash
# Create service file
# Location: A_Src/03_AppServices/<Solution>.AppServices/<Feature>/<Service>.cs
```

**Register in:**
```bash
# Location: A_Src/00_Common/<Solution>.CompositionRoot/00_IOC_MainApp/Partials/IoC_App_BasicServices.cs
```

#### 3. Create Integration Test

**Test Class:**
```bash
# Create test file
# Location: B_Tests/02_Integration/<Solution>.AppServices.Tests.Int/<ClassName>_Tests/<MethodName>_Should.cs
```

---

## Migration & Modernization Workflow

### Use Case 1: Create New Solution from Entity List

**Input:** List of entities with their bounded contexts

**Steps:**

1. **Create Solution Structure**
   - Create folder hierarchy (A_Src, B_Tests, C_Benchmarks)
   - Create all projects using templates above

2. **Scan Local NuGet Repository** ? **NEW**
   - Run package discovery on `C:\WORK\00_Nuget_Repos\Final_Packages\Alexis`
   - Identify latest versions of all Alexis packages
   - Create `nuget.config` with local source

3. **Install Alexis Packages** ? **NEW**
   - Install `Alexis.CrossCutters` to Domain project
   - Install `Alexis.Infrastructure.DatabaseAccess` to DbAccess project
   - Install `Alexis.Infrastructure.General` to Utilities and CompositionRoot projects

4. **Create Domain Entities**
   - For each entity, create class in `01_Entities/<BoundedContext>/`
   - Follow dual ID pattern (string GUID + int auto-increment)
   - Add protected constructor + business constructor
   - Add VersionInfo for properly encapsulated entities

5. **Create Infrastructure**
   - Create `<Entity>_DbConfig.cs` for each entity
   - Create data accessors (DA, Queries, Commands)
   - Update `AppDbContext.cs` with DbSets
   - Update `DbModelsConfiguration.cs` with entity configs

6. **Configure DI**
   - Register all data accessors in `IoC_App_DataAccessors.cs`
   - Configure app services in `IoC_App_BasicServices.cs`

7. **Create Migrations**
   - Run `dotnet ef migrations add Initial` from DsgnTmTls project

8. **Create Tests**
   - Set up BaseTest class
   - Create test classes for each service

### Use Case 2: Migrate Legacy Code to This Architecture

**Input:** Existing codebase with different structure

**Process:**

#### Phase 1: Analysis & Planning
1. **Identify Entities**
   - Extract all entity classes
   - Identify bounded contexts/domains
   - Map relationships

2. **Identify Use Cases**
   - Extract business logic from controllers/services
   - Group by feature/domain

3. **Assess Dependencies**
   - Identify external libraries
   - Check for cross-cutting concerns

#### Phase 2: Domain Migration
1. **Create Domain Project**
   - Migrate entities to `01_Entities/<BoundedContext>/`
   - Refactor to dual ID pattern
   - Add encapsulation (private setters)
   - Extract domain logic to domain services

2. **Create Value Objects**
   - Move DTOs/VOs to `02_Models/00_Logic/`

#### Phase 3: Infrastructure Migration
1. **Create DbAccess Project**
   - Create DbConfigs for all entities
   - Migrate repositories to Data Accessor pattern
   - Split into Queries/Commands

2. **Create Utilities Project**
   - Move cross-cutting concerns (logging, caching, etc.)

#### Phase 4: Application Services Migration
1. **Create AppServices Project**
   - Migrate use cases/business workflows
   - Refactor controllers to thin layers
   - Create DataAccessFacade if needed

#### Phase 5: DI & Testing
1. **Create CompositionRoot**
   - Configure Autofac DI
   - Separate into partials by concern

2. **Create Test Projects**
   - Set up BaseTest infrastructure
   - Migrate existing tests
   - Add integration tests

#### Phase 6: Validation
1. **Run All Tests**
2. **Create Migration**
3. **Verify Database Schema**
4. **Performance Testing**

### Code Review Checklist

When migrating or creating new code, verify:

**Local NuGet Configuration:** ? **NEW**
- [ ] `nuget.config` exists at solution root
- [ ] Local source path is correct: `C:\WORK\00_Nuget_Repos\Final_Packages\Alexis`
- [ ] `packageSourceMapping` configured for Alexis packages
- [ ] Latest versions of Alexis packages installed
- [ ] All Alexis packages reference local source, not public NuGet

**Domain Layer:**
- [ ] Entities inherit from `DbEntity_AlexisBase`
- [ ] Dual ID pattern implemented (string GUID + int)
- [ ] Protected parameterless constructor for EF
- [ ] Business constructor with required parameters
- [ ] Properties have private setters
- [ ] VersionInfo included for audit entities
- [ ] Entities organized by bounded context

**Infrastructure Layer:**
- [ ] DbConfig inherits from `<Solution>_DbTableConfig_Base`
- [ ] Table name and schema configured
- [ ] Primary key configured
- [ ] Auto-increment ID with `UseIdentityColumn`
- [ ] Column types specified (especially decimals, strings)
- [ ] VersionInfo configured with `OwnsOne` pattern
- [ ] Data Accessor split into DA/Queries/Commands
- [ ] Queries use `AsNoTracking()` for read operations
- [ ] Commands inherit from `GenericDbCommandsExec`

**Composition Root:**
- [ ] All DAs registered as `SingleInstance`
- [ ] DbAccessDIResolver initialized before DA registration
- [ ] Configuration split into logical partials
- [ ] DriverUtilities provides test initialization

**Tests:**
- [ ] BaseTest sets up DI container
- [ ] Tests organized by class/method hierarchy
- [ ] Integration tests use actual DB (no mocks)
- [ ] Unit tests are pure (no dependencies)
- [ ] FluentAssertions used for readability
- [ ] XML doc comments explain test purpose

**General:**
- [ ] Namespace matches folder structure
- [ ] Naming conventions followed
- [ ] No circular dependencies
- [ ] All projects target .NET 10+
- [ ] ImplicitUsings enabled

---

## Best Practices & Guidelines

### Encapsulation

? **DO:**
```csharp
public class Order : DbEntity_AlexisBase
{
    public decimal TotalAmount { get; private set; }
    
    public void AddItem(OrderItem item)
    {
        // Business logic here
        TotalAmount += item.Price;
    }
}
```

? **DON'T:**
```csharp
public class Order : DbEntity_AlexisBase
{
    public decimal TotalAmount { get; set; } // Public setter!
}
```

### Async/Await

? **DO:**
```csharp
public async Task<IReadOnlyList<Entity>> GetAllAsync()
{
    return await _repository.GetAll<Entity>().ToListAsync();
}
```

? **DON'T:**
```csharp
public IReadOnlyList<Entity> GetAll()
{
    return _repository.GetAll<Entity>().ToList(); // Blocking
}
```

### Dependency Injection

? **DO:**
```csharp
public class Service
{
    private readonly IRepository _repository;
    
    public Service(IRepository repository)
    {
        _repository = repository;
    }
}
```

? **DON'T:**
```csharp
public class Service
{
    private readonly IRepository _repository = new Repository(); // Tight coupling
}
```

### Test Naming

? **DO:**
```csharp
[Fact]
public async Task ReturnEmptyList_WhenNoRecordsExist()
{
    // Clear test intent from name
}
```

? **DON'T:**
```csharp
[Fact]
public async Task Test1()
{
    // Unclear purpose
}
```

---

## Summary

This template provides a comprehensive, opinionated architecture for .NET solutions with:

- **Clear separation of concerns** (Domain, Infrastructure, AppServices)
- **Consistent naming conventions** across all layers
- **Repository pattern** with explicit Queries/Commands separation
- **Encapsulated domain entities** with proper constructors
- **EF Core best practices** (DbConfig per entity, VersionInfo pattern)
- **Dependency injection** with Autofac, organized in partials
- **Comprehensive test structure** with BaseTest setup
- **Design-time tools** for migrations
- **? Local NuGet repository integration** with runtime package discovery algorithm, and installation guidance
- **v1.0 (2025-11-26)** - Initial template based on Lottron2000 architecture

---

## Appendix: Scaffolding Generator Requirements

When implementing the scaffolding generator, ensure it:

1. ? Scans `C:\WORK\00_Nuget_Repos\Final_Packages\Alexis` for `.nupkg` files
2. ? Parses filenames using regex: `^(.+?)\.(\d+\.\d+\.\d+\.\d+)\.nupkg$`
3. ? Groups packages by name and selects latest version
4. ? Creates `nuget.config` with local source and package source mapping
5. ? Installs packages to correct projects with `--source` parameter
6. ? Verifies all required Alexis packages are available
7. ? Reports discovered packages and versions to user
8. ? Handles errors gracefully (missing packages, invalid paths, etc.)

**See companion document:** `USAGE_WORKFLOWS.md` for detailed PowerShell implementation examples.
