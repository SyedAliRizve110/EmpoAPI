# EmpoAPI — Architecture Guide

> **Purpose**: This document is the authoritative reference for any developer or AI agent working in the EmpoAPI solution. It covers the architecture, naming conventions, folder structure, patterns, and step-by-step instructions for building new APIs.

---

## Table of Contents

1. [Solution Overview](#1-solution-overview)
2. [Project Structure & Layers](#2-project-structure--layers)
3. [Folder Layout](#3-folder-layout)
4. [Naming Conventions](#4-naming-conventions)
5. [CQRS Pattern with MediatR](#5-cqrs-pattern-with-mediatr)
6. [How to Add a New Feature End-to-End](#6-how-to-add-a-new-feature-end-to-end)
7. [Authentication & Authorization](#7-authentication--authorization)
8. [Entity Base & Audit Trail](#8-entity-base--audit-trail)
9. [DbContext Patterns](#9-dbcontext-patterns)
10. [AutoMapper Profiles](#10-automapper-profiles)
11. [Dependency Injection](#11-dependency-injection)
12. [Exception Handling](#12-exception-handling)
13. [Startup & Program.cs Pattern](#13-startup--programcs-pattern)
14. [API Route Conventions](#14-api-route-conventions)
15. [Shared Models & Pagination](#15-shared-models--pagination)
16. [Adding a New Microservice](#16-adding-a-new-microservice)

---

## 1. Solution Overview

EmpoAPI is a **.NET microservices** solution targeting **`net10.0`**. It follows a **Clean Architecture** with **CQRS** (Command Query Responsibility Segregation) using **MediatR**.

### Services

| Service | Port | Purpose |
|---------|------|---------|
| **EmployeeService** | 5003 | HR core — employees, departments, branches, designations, attendance, leave, roles, permissions, auth (JWT issuer) |
| **MediaService** | 5004 | Social/document features — posts, comments, likes, employee documents |
| **ApiGateway** | — | Reserved (not yet implemented) |

### Technology Stack

| Concern | Library |
|---------|---------|
| Framework | ASP.NET Core (`net10.0`), `IWebHostBuilder` + `Startup` class |
| CQRS / Mediator | MediatR 14.x |
| ORM | Entity Framework Core + Npgsql (PostgreSQL) |
| Mapping | AutoMapper 17.x |
| Validation | FluentValidation 11.x, DataAnnotations |
| Auth | JWT Bearer (HS256), shared symmetric key |
| Logging | Serilog (console + compact JSON file) |
| Docs | Swagger / Swashbuckle |

### Shared Libraries

| Project | SDK | Role |
|---------|-----|------|
| `Empo.BuildingBlocks.Application` | `Microsoft.NET.Sdk` | CQRS contracts (`ICommand`, `IQuery`, `CommandBase`, `QueryBase`), handler interfaces, shared models |
| `Empo.BuildingBlocks.Domain` | `Microsoft.NET.Sdk` | Domain interfaces (`IUnitOfWork`) — lightweight |
| `Empo.BuildingBlocks.Infrastructure` | `Microsoft.NET.Sdk` | `EntityBase`, Auth (JWT + permissions), exception hierarchy, `IUserContextService`, constants |
| `Empo.Shared.Model` | `Microsoft.NET.Sdk` | Shared DTOs across services |
| `Empo.Shared.Utility` | `Microsoft.NET.Sdk` | Shared utilities |

> **Important**: `BuildingBlocks.Infrastructure` uses `Microsoft.NET.Sdk` (NOT the Web SDK), so ASP.NET types like `HttpContext`, `IApplicationBuilder`, `IHostEnvironment` are **not available** there. Any middleware that needs those types must live in each API project.

---

## 2. Project Structure & Layers

Each microservice follows a strict **3-layer** architecture:

```
Empo.{Service}.Api            → Presentation layer (controllers, startup, middleware)
Empo.{Service}.Application    → Business logic (CQRS commands, queries, handlers, service interfaces)
Empo.{Service}.Infrastructure → Data access (EF Core, entities, repositories, DbContext, mappers)
```

### Dependency Direction

```
Api  →  Application  →  (no project reference to Infrastructure)
Api  →  Infrastructure  →  Application
```

The **Application layer** defines service interfaces. The **Infrastructure layer** implements them (repositories). The **Api layer** wires everything via DI.

---

## 3. Folder Layout

### Full Solution Tree (source files only)

```
EmpoAPI/
├── Architecture/
│   └── architecture.md                           ← this file
│
├── Empo.BuildingBlocks.Application/
│   ├── Configuration/
│   │   └── ICommandHandler.cs                    ← ICommandHandler<TCmd> and ICommandHandler<TCmd,TResult>
│   ├── Contracts/
│   │   ├── CommandBase.cs                        ← abstract base for commands
│   │   ├── ICommand.cs                           ← ICommand : IRequest, ICommand<T> : IRequest<T>
│   │   ├── IQuery.cs                             ← IQuery<T> : IRequest<T>
│   │   ├── IQueryHandler.cs                      ← IQueryHandler<TQuery, TResult>
│   │   └── QueryBase.cs                          ← abstract base for queries
│   └── SharedModels/
│       ├── GuidExtensions.cs
│       ├── ListModelBase.cs                      ← generic list wrapper (Collection + Filter + Paging)
│       ├── ModelFilterBase.cs                    ← pagination/sorting base (PageSize, PageNumber, SortBy)
│       ├── PagingModel.cs                        ← paging metadata returned to client
│       └── ViewModelBase.cs                      ← empty abstract base for view models
│
├── Empo.BuildingBlocks.Domain/
│   └── IUnitOfWork.cs
│
├── Empo.BuildingBlocks.Infrastructure/
│   ├── Configuration/
│   │   ├── Auth/
│   │   │   ├── HasPermissionAttribute.cs         ← [HasPermission("Entity.Action")]
│   │   │   ├── JwtExtensions.cs                  ← AddSharedJwtAuth() + AddPermissionAuthorization()
│   │   │   ├── PermissionAuthorizationHandler.cs ← reads "permission" claims from JWT
│   │   │   ├── PermissionPolicyProvider.cs       ← dynamic policy provider (prefix: "Permission:")
│   │   │   └── PermissionRequirement.cs
│   │   ├── ExceptionModel/
│   │   │   ├── AppException.cs                   ← abstract base (StatusCode, ErrorCode)
│   │   │   ├── AlreadyExistsException.cs
│   │   │   ├── AuthenticationException.cs
│   │   │   ├── BadRequestException.cs
│   │   │   ├── ConflictException.cs
│   │   │   ├── ErrorResponse.cs                  ← JSON shape returned to clients
│   │   │   ├── ForbiddenException.cs
│   │   │   ├── InvalidOperationAppException.cs
│   │   │   ├── MediaException.cs
│   │   │   └── NotFoundException.cs
│   │   └── UserConfiguration/
│   │       └── IUserContextService.cs            ← GetUserId(), GetEmployeeId(), GetEmail(), GetRole()
│   └── Data/
│       ├── Constants.cs                          ← seed GUIDs (AdminEmployeeId, AdminRoleId, etc.)
│       └── EntityBase.cs                         ← Id, DateCreated, DateModifieed, CreatedBy, ModifiedBy
│
├── Empo.EmloyeeService.Api/                      ← note: "Emloyee" typo in folder name
│   ├── Authorization/
│   │   └── Permissions.cs                        ← all permission string constants
│   ├── Controllers/
│   │   ├── AttendenceController.cs
│   │   ├── AuthController.cs                     ← login, register, refresh token, forgot password
│   │   ├── BranchController.cs
│   │   ├── DepartmentController.cs
│   │   ├── DesignationController.cs
│   │   ├── EmployeeBankDetailsController.cs
│   │   ├── EmployeeController.cs
│   │   ├── EmployeeEducationController.cs
│   │   ├── LeaveBalanceController.cs
│   │   ├── LeaveTypeController.cs
│   │   ├── PermissionController.cs
│   │   ├── RoleController.cs
│   │   └── WorkHistoryController.cs
│   ├── DependencyInjection.cs                    ← AddInfrastructureServices()
│   ├── GlobalExceptionMiddleware.cs
│   ├── Program.cs
│   ├── ServiceExtensions.cs                      ← InitializeDataBase() — runs EF migrations
│   └── Startup.cs
│
├── Empo.EmployeeService.Application/
│   ├── AssemblyReference.cs                      ← marker for MediatR assembly scanning
│   ├── Interface/                                ← cross-cutting service interfaces
│   ├── Models/                                   ← DTOs (BranchDto, DepartmentDto, etc.)
│   ├── Attendence/
│   │   ├── ClockIn/                              ← ClockInCommand + ClockInCommandHandler
│   │   ├── ClockOut/
│   │   ├── AttendenceList/
│   │   └── ActiveEmployeesList/
│   ├── AuthService/
│   │   ├── Login/
│   │   ├── ForgotPassword/
│   │   ├── RefreshToken/
│   │   ├── Permission/                           ← CreatePermission/, PermissionList/
│   │   └── Role/                                 ← CreateRole/, GetRoleDetails/, RoleList/, UpdateRole/
│   ├── Branch/
│   │   ├── CreateBranch/                         ← Command + Handler + Request
│   │   ├── GetBranch/                            ← Query + Handler
│   │   ├── ListBranch/                           ← Query + Handler + Request + Response
│   │   ├── UpdateBranch/
│   │   ├── AssignBranchEmployee/
│   │   ├── AssignBranchManager/
│   │   ├── BranchEmployeeList/
│   │   └── ServiceInterface/
│   │       └── IBranchService.cs                 ← interface implemented by BranchRepository
│   ├── Department/                               ← same pattern as Branch
│   ├── Designations/
│   ├── EmployeeBank/
│   ├── EmployeeEducation/
│   ├── EmployeeWorkHistory/
│   └── Employees/
│       ├── CreateEmployee/
│       ├── GetEmployeeDetails/
│       ├── GetEmployeeList/
│       ├── UpdateEmployee/
│       ├── EmployeesModel/
│       └── ServiceInterface/
│
├── Empo.EmployeeService.Infrastructure/
│   ├── EmployeeContext.cs                        ← DbContext with SaveChangesAsync override
│   ├── Database/
│   │   └── SchemaNames.cs                        ← const "employee"
│   ├── Data/
│   │   ├── Entities/
│   │   │   ├── Branch/BranchEntity.cs
│   │   │   ├── Department/DepartmentEntity.cs
│   │   │   ├── Designation/DesignationEntity.cs
│   │   │   ├── Education/EmployeeEducationEntity.cs
│   │   │   ├── Employee/EmployeeEntity.cs, AddressEntity.cs, EmployeePhoneEntity.cs, etc.
│   │   │   ├── Leave/LeaveTypeEntity.cs, LeaveRequestEntity.cs, etc.
│   │   │   ├── User/UserEntity.cs, RoleEntity.cs, PermissionEntity.cs, RefreshTokenEntity.cs
│   │   │   └── WorkHistory/EmployeeWorkHistoryEntity.cs
│   │   ├── Mappers/
│   │   │   └── EmployeeMapper.cs                 ← single AutoMapper Profile for all entities
│   │   ├── Repositories/                         ← one repository per aggregate
│   │   ├── Services/
│   │   │   ├── JwtServices.cs                    ← token generation (EmployeeService only)
│   │   │   ├── PasswordHasherService.cs
│   │   │   └── UserContextService.cs
│   │   └── DataSeeder.cs
│
├── Empo.MediaService.Api/
│   ├── Controllers/
│   │   ├── MediaController.cs
│   │   └── EmployeeDocumentController.cs
│   ├── GlobalExceptionMiddleware.cs
│   ├── Program.cs
│   └── Startup.cs
│
├── Empo.MediaService.Application/
│   ├── Authorization/
│   │   └── MediaPermissions.cs                   ← media-specific permission constants
│   ├── Media/
│   │   ├── CreatePost/
│   │   ├── GetFeed/
│   │   ├── AddComment/
│   │   ├── GetComment/
│   │   ├── ToggleLike/
│   │   └── DeletePost/
│   └── EmployeeDocument/
│       ├── UploadDocument/
│       ├── DownloadDocument/
│       ├── DeleteDocument/
│       ├── DocumentList/
│       ├── VerifyDocument/
│       └── GetDocument/
│
├── Empo.MediaService.Infrastructure/
│   ├── MediaContext.cs
│   ├── Data/Entities/
│   ├── Data/Services/
│   └── Data/Mappers/
│
├── Empo.Shared.Model/
├── Empo.Shared.Utility/
├── Empo.ApiGateway/                              ← placeholder, not yet implemented
└── Utility/
```

---

## 4. Naming Conventions

### Projects & Namespaces

| Item | Pattern | Example |
|------|---------|---------|
| API project | `Empo.{Service}.Api` | `Empo.MediaService.Api` |
| Application project | `Empo.{Service}.Application` | `Empo.EmployeeService.Application` |
| Infrastructure project | `Empo.{Service}.Infrastructure` | `Empo.EmployeeService.Infrastructure` |
| Namespace | matches folder path | `Empo.EmployeeService.Application.Branch.CreateBranch` |

### Classes

| Type | Pattern | Example |
|------|---------|---------|
| Entity | `{Name}Entity` | `BranchEntity`, `EmployeeEntity` |
| DTO (simple response) | `{Name}Dto` | `BranchDto`, `DepartmentDto` |
| Model (rich data) | `{Name}Model` | `BranchModel`, `EmployeeModel` |
| Request (write) | `Create{Entity}Request`, `Update{Entity}Request` | `CreateBranchRequest` |
| Request (read/list) | `{Entity}ListRequest`, `Get{Entity}ListRequest` | `BranchListRequest` |
| Response (read) | `{Entity}ListResponse`, `Get{Entity}DetailsResponse` | `BranchListResponse` |
| Command | `{Verb}{Entity}Command` | `CreateBranchCommand` |
| Command Handler | `{Verb}{Entity}CommandHandler` | `CreateBranchCommandHandler` |
| Query | `{Verb}{Entity}Query` or `{Entity}ListQuery` | `GetBranchQuery`, `BranchListQuery` |
| Query Handler | `{Verb}{Entity}QueryHandler` or `{Entity}ListQueryHandler` | `BranchListQueryHandler` |
| Controller | `{Entity}Controller` | `BranchController` |
| Repository | `{Entity}Repository` | `BranchRepository` |
| Service Interface | `I{Entity}Service` | `IBranchService` |
| AutoMapper Profile | `{Service}Mapper` | `EmployeeMapper` |
| DbContext | `{Service}Context` | `EmployeeContext`, `MediaContext` |
| Permission constant | `{Entity}{Action}` | `BranchCreate`, `EmployeeGet` |
| Permission value | `"{Entity}.{Action}"` | `"Branch.Create"`, `"Employee.Get"` |
| DbSet property | singular `{Entity}` name (no "s") | `public DbSet<BranchEntity> Branch { get; set; }` |

### Folders

| Folder | Contents |
|--------|----------|
| `Controllers/` | One controller per aggregate/entity |
| `Authorization/` | `Permissions.cs` (or `MediaPermissions.cs`) with string constants |
| `{Feature}/{Action}/` | One folder per CQRS operation (e.g., `Branch/CreateBranch/`) |
| `{Feature}/ServiceInterface/` | Service interface for the feature (implemented in Infrastructure) |
| `Data/Entities/{Entity}/` | Entity classes grouped by aggregate |
| `Data/Repositories/` | Repository implementations |
| `Data/Services/` | Infrastructure services (JWT, password hasher, user context) |
| `Data/Mappers/` | AutoMapper Profile classes |
| `Database/` | Schema names and DB configuration |
| `Models/` | DTOs shared across features within the service |

---

## 5. CQRS Pattern with MediatR

The project uses **CQRS** — every operation is either a **Command** (write) or a **Query** (read), dispatched through **MediatR**.

### Base Contracts (in `BuildingBlocks.Application`)

```csharp
// Commands
public interface ICommand : IRequest { Guid Id { get; } }
public interface ICommand<out TResult> : IRequest<TResult> { Guid Id { get; } }

public abstract class CommandBase<TResult> : ICommand<TResult>
{
    public Guid Id { get; }
    protected CommandBase() { this.Id = Guid.NewGuid(); }
}

// Queries
public interface IQuery<out TResult> : IRequest<TResult> { }

public abstract class QueryBase<TResult> : IQuery<TResult>
{
    public Guid Id { get; }
    protected QueryBase() { this.Id = Guid.NewGuid(); }
}

// Handlers
public interface ICommandHandler<in TCommand, TResult>
    : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult> { }

public interface IQueryHandler<in TQuery, TResult>
    : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult> { }
```

### Command Example — `CreateBranch`

**1. Request model** (`CreateBranchRequest.cs`):
```csharp
namespace Empo.EmployeeService.Application.Branch.CreateBranch;

public class CreateBranchRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Code is required")]
    public string BranchCode { get; set; }

    public CreateBranchAddressModel Address { get; set; }
}
```

**2. Command** (`CreateBranchCommand.cs`):
```csharp
namespace Empo.EmployeeService.Application.Branch.CreateBranch;

public class CreateBranchCommand : CommandBase<BranchDto>
{
    public CreateBranchRequest _request { get; set; }

    public static CreateBranchCommand Create(CreateBranchRequest request)
    {
        return new CreateBranchCommand { _request = request };
    }
}
```

> **Pattern**: Commands always have a `public static {Command} Create({Request} request)` factory method.

**3. Command Handler** (`CreateBranchCommandHandler.cs`):
```csharp
namespace Empo.EmployeeService.Application.Branch.CreateBranch;

public class CreateBranchCommandHandler : ICommandHandler<CreateBranchCommand, BranchDto>
{
    public IBranchService _service { get; }

    public CreateBranchCommandHandler(IBranchService service)
    {
        _service = service;
    }

    public async Task<BranchDto> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var branch = await _service.CreateBranchAsync(request);
        return new BranchDto { Id = branch };
    }
}
```

### Query Example — `BranchList`

**1. Request** (`BranchListRequest.cs`):
```csharp
public class BranchListRequest : ModelFilterBase
{
    public string search { get; set; }
}
```

> **Pattern**: List requests extend `ModelFilterBase` to inherit `PageSize`, `PageNumber`, `SortBy`, `SortDirection`.

**2. Query** (`BranchListQuery.cs`):
```csharp
public class BranchListQuery : IQuery<BranchListResponse>
{
    public BranchListRequest request { get; private set; }

    public static BranchListQuery Get(BranchListRequest request)
    {
        return new BranchListQuery() { request = request };
    }
}
```

> **Pattern**: Queries have a `public static {Query} Get(...)` factory method.

**3. Query Handler** (`BranchListQueryHandler.cs`):
```csharp
public class BranchListQueryHandler : IQueryHandler<BranchListQuery, BranchListResponse>
{
    public IBranchService _repo { get; }

    public BranchListQueryHandler(IBranchService repo)
    {
        _repo = repo;
    }

    public async Task<BranchListResponse> Handle(BranchListQuery query, CancellationToken cancellationToken)
    {
        var response = await _repo.BranchListAsync(query.request);
        return response;
    }
}
```

**4. Response** (`BranchListResponse.cs`):
```csharp
public class BranchListResponse
{
    public IEnumerable<BranchModel> Collection { get; set; }
    public long TotalRecords { get; set; }
}
```

---

## 6. How to Add a New Feature End-to-End

This is the step-by-step checklist for adding a new CRUD feature (e.g., "LeaveType" in EmployeeService). Follow the **Branch** feature as the reference implementation.

### Step 1: Create the Entity (Infrastructure)

**File**: `Infrastructure/Data/Entities/LeaveType/LeaveTypeEntity.cs`

```csharp
namespace Empo.EmployeeService.Infrastructure.Data.Entities.LeaveType;

public class LeaveTypeEntity : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int DefaultDays { get; set; }
    public bool IsActive { get; set; } = true;
}
```

- **Must** extend `EntityBase` to get `Id`, `DateCreated`, `DateModifieed`, `CreatedBy`, `ModifiedBy`.
- Place in `Data/Entities/{EntityName}/` folder.

### Step 2: Register DbSet (Infrastructure)

In `{Service}Context.cs`, add:

```csharp
public DbSet<LeaveTypeEntity> LeaveType { get; set; }
```

### Step 3: Create the Service Interface (Application)

**File**: `Application/LeaveType/ServiceInterface/ILeaveTypeService.cs`

```csharp
namespace Empo.EmployeeService.Application.LeaveType.ServiceInterface;

public interface ILeaveTypeService
{
    Task<Guid> CreateLeaveTypeAsync(CreateLeaveTypeRequest request);
    Task<LeaveTypeModel> GetLeaveTypeDetails(Guid id);
    Task<LeaveTypeListResponse> LeaveTypeListAsync(LeaveTypeListRequest request);
    Task<Guid> UpdateLeaveTypeAsync(UpdateLeaveTypeRequest request);
}
```

### Step 4: Create CQRS Files (Application)

For each operation, create a folder under `Application/LeaveType/`:

```
Application/LeaveType/
├── CreateLeaveType/
│   ├── CreateLeaveTypeRequest.cs       ← input model with [Required] etc.
│   ├── CreateLeaveTypeCommand.cs       ← extends CommandBase<LeaveTypeDto>
│   └── CreateLeaveTypeCommandHandler.cs ← implements ICommandHandler<...>
├── GetLeaveType/
│   ├── GetLeaveTypeQuery.cs            ← implements IQuery<LeaveTypeModel>
│   └── GetLeaveTypeQueryHandler.cs
├── ListLeaveType/
│   ├── LeaveTypeListRequest.cs         ← extends ModelFilterBase
│   ├── LeaveTypeListResponse.cs
│   ├── LeaveTypeListQuery.cs           ← implements IQuery<LeaveTypeListResponse>
│   └── LeaveTypeListQueryHandler.cs
├── UpdateLeaveType/
│   ├── UpdateLeaveTypeRequest.cs
│   ├── UpdateLeaveTypeCommand.cs
│   └── UpdateLeaveTypeCommandHandler.cs
└── ServiceInterface/
    └── ILeaveTypeService.cs
```

### Step 5: Implement the Repository (Infrastructure)

**File**: `Infrastructure/Data/Repositories/LeaveTypeRepository.cs`

```csharp
public class LeaveTypeRepository : ILeaveTypeService
{
    private readonly EmployeeContext _context;
    private readonly IMapper _mapper;

    public LeaveTypeRepository(EmployeeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> CreateLeaveTypeAsync(CreateLeaveTypeRequest request)
    {
        var entity = _mapper.Map<LeaveTypeEntity>(request);
        await _context.LeaveType.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }
    // ... other methods
}
```

### Step 6: Add AutoMapper Mappings (Infrastructure)

In `Data/Mappers/{Service}Mapper.cs`, add a region:

```csharp
#region LeaveType
CreateMap<LeaveTypeEntity, LeaveTypeModel>().ReverseMap();
CreateMap<CreateLeaveTypeRequest, LeaveTypeEntity>().ReverseMap();
CreateMap<UpdateLeaveTypeRequest, LeaveTypeEntity>().ReverseMap();
#endregion
```

### Step 7: Register DI (Api)

In `DependencyInjection.cs`:

```csharp
services.AddScoped<ILeaveTypeService, LeaveTypeRepository>();
```

### Step 8: Add Permission Constants (Api)

In `Authorization/Permissions.cs`:

```csharp
#region LeaveType
public const string LeaveTypeManage = "LeaveType.Manage";
public const string LeaveTypeGet    = "LeaveType.Get";
#endregion
```

### Step 9: Create the Controller (Api)

**File**: `Controllers/LeaveTypeController.cs`

```csharp
[Route("api/leave-type")]
[ApiController]
[Authorize]
public class LeaveTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("")]
    [HttpPost]
    [HasPermission(Permissions.LeaveTypeManage)]
    [ProducesResponseType(typeof(LeaveTypeDto), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateLeaveType([FromBody] CreateLeaveTypeRequest request)
    {
        var dto = await _mediator.Send(CreateLeaveTypeCommand.Create(request));
        return Ok(dto);
    }

    [Route("{id}")]
    [HttpGet]
    [HasPermission(Permissions.LeaveTypeGet)]
    [ProducesResponseType(typeof(LeaveTypeModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetLeaveType([FromRoute] Guid id)
    {
        var details = await _mediator.Send(GetLeaveTypeQuery.Get(id));
        return Ok(details);
    }

    [Route("list")]
    [HttpPost]
    [HasPermission(Permissions.LeaveTypeGet)]
    [ProducesResponseType(typeof(LeaveTypeListResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> LeaveTypeList([FromBody] LeaveTypeListRequest request)
    {
        var list = await _mediator.Send(LeaveTypeListQuery.Get(request));
        return Ok(list);
    }
}
```

### Step 10: Add EF Migration

```bash
dotnet ef migrations add AddLeaveType -p Empo.EmployeeService.Infrastructure -s Empo.EmloyeeService.Api
```

The migration will auto-apply on next startup via `InitializeDataBase()`.

---

## 7. Authentication & Authorization

### Architecture: Shared JWT (Option A)

EmployeeService is the **token issuer**. All services **validate the same JWT** using a shared symmetric key.

```
┌──────────────────┐    login    ┌──────────────────┐
│   Client / SPA   │───────────▸│  EmployeeService  │
│                  │◂───────────│  (issues JWT)     │
│                  │            └──────────────────┘
│                  │   Bearer token
│                  │───────────▸┌──────────────────┐
│                  │            │  MediaService     │
│                  │            │  (validates JWT)  │
└──────────────────┘            └──────────────────┘
```

### Shared JWT Setup

In `BuildingBlocks.Infrastructure`, `JwtExtensions.cs` provides:

```csharp
// Called in every service's Startup.ConfigureServices():
services.AddSharedJwtAuth(_config);         // validates JWT with Jwt:Key, Jwt:Issuer, Jwt:Audience
services.AddPermissionAuthorization();      // registers dynamic policy provider + handler
```

### Configuration (`appsettings.Development.json`)

Both services must have the **same** values:

```json
{
  "Jwt": {
    "Key": "<same-256-bit-key>",
    "Issuer": "EmpoAPI",
    "Audience": "EmpoClient"
  }
}
```

> **Never commit `appsettings.Development.json`** — it is in `.gitignore`.

### Permission-Based Authorization

The JWT token contains `permission` claims like `"Branch.Create"`, `"Employee.Get"`. The authorization pipeline:

1. **`[HasPermission("Branch.Create")]`** attribute on controller action
2. → `PermissionPolicyProvider` creates a policy named `"Permission:Branch.Create"`
3. → `PermissionAuthorizationHandler` checks if the user's JWT has a matching `permission` claim

**Permission string format**: `"{Entity}.{Action}"` — e.g., `"Branch.Create"`, `"Post.Like"`, `"Document.Upload"`

### Key Auth Files

| File | Location | Purpose |
|------|----------|---------|
| `HasPermissionAttribute.cs` | BuildingBlocks.Infrastructure | `[HasPermission]` attribute |
| `PermissionPolicyProvider.cs` | BuildingBlocks.Infrastructure | Maps `"Permission:X"` to policies |
| `PermissionAuthorizationHandler.cs` | BuildingBlocks.Infrastructure | Checks JWT `permission` claims |
| `Permissions.cs` | EmployeeService.Api/Authorization | All permission constants for EmployeeService |
| `MediaPermissions.cs` | MediaService.Application/Authorization | Permission constants for MediaService |

### Controller Auth Pattern

```csharp
[Route("api/branch")]
[ApiController]
[Authorize]                                  // class-level: requires valid JWT
public class BranchController : ControllerBase
{
    [HttpPost]
    [HasPermission(Permissions.BranchCreate)]  // method-level: requires specific permission
    public async Task<IActionResult> CreateBranch(...)
```

---

## 8. Entity Base & Audit Trail

All entities inherit from `EntityBase` in `BuildingBlocks.Infrastructure`:

```csharp
public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModifieed { get; set; }   // note: typo is established, don't change
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }

    public void SetDataRecorderMetadata(Guid? userId, bool IsNew)
    {
        if (IsNew)
        {
            DateCreated = DateTime.UtcNow;
            CreatedBy = userId ?? Guid.Empty;
            DateModifieed = DateTime.UtcNow;
            ModifiedBy = userId ?? Guid.Empty;
        }
        else
        {
            DateModifieed = DateTime.UtcNow;
            ModifiedBy = userId ?? Guid.Empty;
        }
    }
}
```

**Audit fields are set automatically** — the `DbContext.SaveChangesAsync()` override calls `SetChangesInternal()` which iterates all changed `EntityBase` entries and stamps the current user ID and UTC timestamp. You do **NOT** set these fields in your repository code.

> **Important**: On `EntityState.Modified`, `DateCreated` and `CreatedBy` are explicitly marked `IsModified = false` to prevent accidental overwrite.

---

## 9. DbContext Patterns

### SaveChangesAsync Override

Every `DbContext` must override `SaveChangesAsync` and `SaveChanges` to call `SetChangesInternal()`:

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
{
    SetChangesInternal();
    return await base.SaveChangesAsync(cancellationToken);
}
```

### Schema Names

Each service uses a dedicated PostgreSQL schema:

```csharp
// In OnModelCreating:
modelBuilder.HasDefaultSchema(SchemaNames.Application);

// SchemaNames.cs:
internal sealed class SchemaNames
{
    internal const string Application = "employee";  // or "media"
}
```

### User Context Injection

The `DbContext` takes `IUserContextService` to get the current user ID for audit fields:

```csharp
public EmployeeContext(DbContextOptions<EmployeeContext> options, IUserContextService userInfoProvider)
    : base(options)
{
    _userInfoProvider = userInfoProvider;
}
```

### Database Initialization

Migrations are auto-applied on startup:

```csharp
// In ServiceExtensions.cs:
public static void InitializeDataBase(this IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<EmployeeContext>();
    context.Database.Migrate();
}

// Called in Startup.Configure():
app.InitializeDataBase();
```

---

## 10. AutoMapper Profiles

Each service has **one mapper Profile** in `Infrastructure/Data/Mappers/`:

```csharp
public class EmployeeMapper : Profile
{
    public EmployeeMapper()
    {
        #region Employee
        CreateMap<EmployeeModel, EmployeeEntity>().ReverseMap();
        CreateMap<CreateEmployeeRequestModel, EmployeeEntity>().ReverseMap();
        #endregion

        #region Branch
        CreateMap<BranchEntity, BranchModel>().ReverseMap();
        CreateMap<CreateBranchRequest, BranchEntity>().ReverseMap();
        #endregion

        // ... grouped by #region
    }
}
```

**Rules**:
- Always use `.ReverseMap()` for bidirectional mapping
- Group mappings by entity using `#region` / `#endregion`
- Register in `Startup.ConfigureServices()`:

```csharp
services.AddAutoMapper(config => { }, typeof(EmployeeMapper));
```

---

## 11. Dependency Injection

Each service has a `DependencyInjection.cs` in the **Api project** with one extension method:

```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // 1. Permission authorization from BuildingBlocks
        services.AddPermissionAuthorization();

        // 2. Infrastructure services
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserContextService, UserContextService>();

        // 3. Repositories (Interface → Implementation)
        services.AddScoped<IBranchService, BranchRepository>();
        services.AddScoped<IDepartmentService, DepartmentRepository>();
        // ... one line per repository

        return services;
    }
}
```

**Called in `Startup.ConfigureServices()`**:
```csharp
services.AddInfrastructureServices(_config);
```

**Lifetime rules**:
- Repositories and services: `AddScoped`
- Policy providers: `AddSingleton` (done by `AddPermissionAuthorization()`)
- Authorization handlers: `AddScoped` (done by `AddPermissionAuthorization()`)

---

## 12. Exception Handling

### Exception Hierarchy (BuildingBlocks)

```
Exception
 └── AppException (abstract)
      ├── AlreadyExistsException    → 409 Conflict
      ├── AuthenticationException   → 401 Unauthorized
      ├── BadRequestException       → 400 Bad Request
      ├── ConflictException         → 409 Conflict
      ├── ForbiddenException        → 403 Forbidden
      ├── InvalidOperationAppException → 400 Bad Request
      ├── MediaException            → varies
      └── NotFoundException         → 404 Not Found
```

Each subclass sets the `StatusCode` and `ErrorCode` in its constructor. Example usage in a repository:

```csharp
var entity = await _context.Branch.FindAsync(id)
    ?? throw new NotFoundException("Branch not found");
```

### GlobalExceptionMiddleware

Each API project has its own `GlobalExceptionMiddleware.cs` (cannot be in BuildingBlocks because it needs ASP.NET types). It catches all exceptions and returns a consistent `ErrorResponse`:

```json
{
  "errorCode": "NOT_FOUND",
  "message": "Branch not found",
  "traceId": "0HN...",
  "errors": null
}
```

**Mapping logic**:
- `AppException` → uses its own `StatusCode` and `ErrorCode`
- `ValidationException` (FluentValidation) → 400 with field-level `errors` dictionary
- `UnauthorizedAccessException` → 401
- `KeyNotFoundException` → 404
- `ArgumentException` → 400
- Everything else → 500 (message hidden in production)

**Registration in `Startup.Configure()`**:
```csharp
app.UseGlobalExceptionHandling();  // must be first middleware
```

---

## 13. Startup & Program.cs Pattern

### Program.cs

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
}
```

> Uses the `IWebHostBuilder` + `Startup` class pattern (not minimal APIs).

### Startup.cs Template

```csharp
public class Startup
{
    private readonly IConfiguration _config;

    public Startup(IWebHostEnvironment env)
    {
        // 1. Configure Serilog
        // 2. Build configuration from appsettings + environment
        IConfigurationBuilder builder = new ConfigurationBuilder();
        if (env.EnvironmentName.ToLower() == "development")
        {
            builder = builder
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json");
        }
        _config = builder.AddEnvironmentVariables().Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
        services.AddAutoMapper(config => { }, typeof(ServiceMapper));
        services.AddControllers();
        services.AddMemoryCache();
        services.AddCors(...);
        services.AddSwaggerGen(...);         // with Bearer security definition
        services.AddSharedJwtAuth(_config);  // shared JWT validation
        services.AddDbContext<ServiceContext>(options =>
            options.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(assemblyName)));
        services.AddInfrastructureServices(_config);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseGlobalExceptionHandling();    // first!
        app.UseHttpsRedirection();
        app.InitializeDataBase();            // auto-migrate
        app.UseRouting();
        app.UseCors("MyCustomPolicy");
        app.UseAuthentication();             // must be before UseAuthorization
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        app.UseSwagger();
        app.UseSwaggerUI(...);
    }
}
```

### Middleware Order (Critical)

```
UseGlobalExceptionHandling   ← catches all downstream exceptions
UseHttpsRedirection
InitializeDataBase
UseRouting
UseCors
UseAuthentication            ← must be before UseAuthorization
UseAuthorization
UseEndpoints
UseSwagger
```

---

## 14. API Route Conventions

| Pattern | Example | HTTP Method |
|---------|---------|-------------|
| Create | `POST api/branch` | POST |
| Update | `PUT api/branch` | PUT |
| Get by ID | `GET api/branch/{branchId}` | GET |
| List (filtered) | `POST api/branch/list` | POST (body has filter/paging) |
| Sub-resource list | `POST api/branch/employee-list` | POST |
| Action | `POST api/branch/assign-employee` | POST |
| Auth | `POST api/auth/login` | POST |

**Rules**:
- Route prefix: `api/{entity-name}` (lowercase, kebab-case for multi-word)
- Class-level: `[Route("api/branch")]` + `[ApiController]` + `[Authorize]`
- Method-level: `[Route("")]` for root, `[Route("{id}")]` for by-ID, `[Route("list")]` for lists
- List endpoints use **POST** (request body contains filter + pagination)
- All controllers return `Ok(result)` — the standard response shape
- `[ProducesResponseType]` on every action

---

## 15. Shared Models & Pagination

### ModelFilterBase

All list requests extend `ModelFilterBase`:

```csharp
public class ModelFilterBase
{
    public string SortBy { get; set; }
    public SortDirection SortDirection { get; set; }   // Asc = 1, Desc = 2
    public int PageSize { get; set; } = 15;            // default 15, min 1
    public int PageNumber { get; set; } = 1;           // default 1, min 1
}
```

### PagingModel

Returned in list responses:

```csharp
public class PagingModel
{
    public int PageSize { get; set; }
    public long TotalRecords { get; set; }
    public int CurrentPage { get; set; }
}
```

### ListModelBase (Optional)

Generic wrapper for list views:

```csharp
public abstract class ListModelBase<T, N> : ViewModelBase
    where T : ViewModelBase
    where N : ModelFilterBase
{
    public abstract IEnumerable<T> Collection { get; set; }
    public abstract N Filter { get; set; }
    public PagingModel PagingModel { get; set; }
}
```

---

## 16. Adding a New Microservice

To add a new microservice (e.g., `NotificationService`):

1. **Create 3 projects** following the naming convention:
   - `Empo.NotificationService.Api` (Web SDK)
   - `Empo.NotificationService.Application` (Class Library)
   - `Empo.NotificationService.Infrastructure` (Class Library)

2. **Add project references**:
   - Api → Application, Infrastructure, BuildingBlocks.Application, BuildingBlocks.Infrastructure
   - Infrastructure → Application, BuildingBlocks.Infrastructure, BuildingBlocks.Domain

3. **Copy from an existing service**:
   - `Program.cs` — update `Startup` reference
   - `Startup.cs` — update DbContext, mapper, assembly references
   - `GlobalExceptionMiddleware.cs` — update namespace
   - `DependencyInjection.cs` — register new services
   - `ServiceExtensions.cs` — `InitializeDataBase()` with new context

4. **Create `{Service}Context.cs`** in Infrastructure, following the EmployeeContext pattern (SaveChanges override, SchemaNames, IUserContextService)

5. **Create `SchemaNames.cs`** with a unique schema name

6. **Create `{Service}Mapper.cs`** (AutoMapper Profile) in Infrastructure

7. **Add `Jwt` section** to `appsettings.json` and `appsettings.Development.json` with the same shared key

8. **Call `AddSharedJwtAuth()` and `AddPermissionAuthorization()`** in Startup

9. **Add `AssemblyReference.cs`** marker class in the Application project for MediatR scanning

10. **Create permission constants** in `Authorization/{Service}Permissions.cs`

---

## Quick Reference Card

| I need to... | Do this |
|--------------|---------|
| Add a new entity | Create `{Name}Entity : EntityBase` in `Infrastructure/Data/Entities/{Name}/` |
| Add a create endpoint | Create `CreateX/` folder with Request, Command, CommandHandler in Application |
| Add a list endpoint | Create `ListX/` folder with Request, Response, Query, QueryHandler in Application |
| Add a repository | Implement the service interface in `Infrastructure/Data/Repositories/` |
| Register DI | Add `services.AddScoped<IXService, XRepository>()` in `DependencyInjection.cs` |
| Add a permission | Add `public const string XAction = "X.Action"` in `Permissions.cs` |
| Throw a business error | `throw new NotFoundException("X not found")` — middleware handles the rest |
| Add AutoMapper mapping | Add `CreateMap<XEntity, XModel>().ReverseMap()` in the service's Mapper Profile |
| Access current user | Inject `IUserContextService`, call `GetUserId()`, `GetEmployeeId()`, `GetEmail()` |
