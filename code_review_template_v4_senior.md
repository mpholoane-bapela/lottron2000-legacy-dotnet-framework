# AI-Assisted Code Review Template - Senior Developer Edition

**Version:** 3.1-Senior  
**Target Audience:** Senior Developers (5+ years experience)  
**Target Framework:** .NET 10+ / C# 14.0+  
**For Use With:** GitHub Copilot, Visual Studio  
**Last Updated:** 2025-01-15

This template provides **rigorous, enterprise-grade, and production-critical** code review criteria for senior developers. Excellence is the baseline, and architectural decisions are scrutinized.

---

<!-- 
============================================================================
SOLUTION ORGANIZATION REVIEW SETTINGS (Optional Section - Not Scored)
============================================================================
Use for Review: Yes
RootPath: C:\WORK\00 KNOW\0 TEMPLATES\CodeBestPractice\Version-02
Templates: OPINIONATED_SOLUTION_ARCHITECTURE_TEMPLATE_v2.md

Note: At least one template should match the solution structure.
This section is for commentary only and does not contribute to the final score.
============================================================================
-->

---

## How to Use This Template

### Senior-Level Review:
```
"Perform a comprehensive code review using code_review_template_v3_senior.md. 
Apply enterprise-grade standards, evaluate architectural decisions critically, 
identify hidden technical debt, assess long-term maintainability, and provide 
strategic recommendations for scalability and resilience."
```

---

## Review Philosophy for Senior Developers

### Non-Negotiable Standards:
- **Production excellence** is the minimum bar
- **Architectural integrity** maintained across all changes
- **Zero tolerance** for security vulnerabilities
- **Performance** considered in every decision
- **Long-term maintainability** over short-term convenience
- **Domain-driven design** principles applied rigorously
- **Observability** built-in from day one

### Senior-Level Accountability:
You are responsible for:
1. **Technical leadership** - Code sets the standard for the team
2. **System-wide impact** - Understanding ripple effects
3. **Production readiness** - Code must be deploy-ready
4. **Knowledge transfer** - Code is documentation for junior/intermediate devs
5. **Technical debt management** - Balancing pragmatism with excellence

---

## IMPORTANT: Category Weight Adjustment

**Not all code reviews will include all categories.** Some projects may not have:
- App Driver (Front End/Jobs) sections
- Security requirements
- Monitoring and Reporting features
- Database/Data Management components

**When a category is not applicable to the review:**
1. Mark it as "N/A" in the review
2. **Proportionally re-adjust the weights** of remaining categories
3. The total weight must always equal 100%

**Example:** If Security (10%) is N/A, distribute its weight proportionally across other categories based on their original weights.

---

## Review Categories & Weights (Senior Level)

| # | Category | Weight | Excellence Threshold |
|---|----------|--------|---------------------|
| 1 | **Solution Architecture** | 20% | Clean architecture, DDD principles, scalable patterns, low coupling |
| 2 | **Programming** | 20% | Clean code, SOLID principles, zero code smells, proper patterns |
| 3 | **Data Management** | 10% | Optimized data access, proper EF Core usage, query optimization |
| 4 | **Testing and QA** | 15% | >85% coverage, mutation testing, comprehensive test suite |
| 5 | **App Driver (Front End/Jobs)** | 10% | Clean UI/job architecture, proper separation of concerns |
| 6 | **Security** | 10% | Zero critical vulnerabilities, OWASP compliance, defense in depth |
| 7 | **Monitoring and Reporting** | 5% | Structured logging, metrics, distributed tracing, observability |
| 8 | **Performance and Optimization** | 5% | Profiled, optimized, resilient under load, efficient algorithms |
| 9 | **Misc (Documentation + Others)** | 5% | ADRs, runbooks, comprehensive inline docs, knowledge transfer |

**Total:** 100%

### Senior Performance Expectations

| Score | Status | Interpretation |
|-------|--------|----------------|
| **97-100** | Exceptional | Reference implementation, teachable |
| **93-96** | Excellent | Production-ready, minimal feedback |
| **88-92** | Good | Solid work, minor improvements |
| **80-87** | Adequate | Meets baseline, needs refinement |
| **< 80** | Below Standard | Not meeting senior-level expectations |

**Note:** Score < 85 requires technical lead review and may delay merge.

---

# 0. Solution Organization (Not Scored - Commentary Only)

**This section is OPTIONAL and does not contribute to the final score.**

### Purpose
Evaluate whether the solution structure follows established organizational patterns for:
- Project layout and naming conventions
- Folder hierarchy and separation of concerns
- Dependencies and references between projects
- Adherence to team/organization standards

### Review Against Pattern Templates
Check if the solution matches one or more of the configured pattern templates:
- Clean Architecture Pattern
- Domain-Driven Design (DDD) Pattern
- Onion Architecture Pattern
- Layered Architecture Pattern
- Or custom organizational pattern

### Evaluation Criteria (Commentary)
- [ ] Projects are organized by architectural layers or features
- [ ] Naming conventions are consistent and meaningful
- [ ] Dependencies flow in the correct direction (inner layers don't depend on outer)
- [ ] Test projects mirror source project structure
- [ ] Shared/common projects are properly utilized
- [ ] Infrastructure concerns are separated from business logic

### Findings
*Document alignment or deviations from the expected pattern(s). Include recommendations for organizational improvements.*

**Example:**
```
? Solution follows Clean Architecture pattern closely
? Clear separation between Domain, Application, Infrastructure, and UI layers
? Recommendation: Consider moving shared DTOs from Infrastructure to a dedicated Contracts project
? Test project naming could be more consistent (Unit vs Tests.Unit)
```

---

# 1. Solution Architecture (Weight: 20%)

### Senior Architecture Expectations
You are the **architectural guardian**. Every decision impacts system scalability, maintainability, and long-term success.

### Clean Architecture Principles

#### Domain-Driven Design (DDD)
```csharp
// Excellent - Rich domain model with behavior
public class Order : AggregateRoot
{
    private readonly List<OrderLine> _lines = new();
    public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();
    
    public Money Total { get; private set; }
    public OrderStatus Status { get; private set; }
    
    public void AddLine(Product product, Quantity quantity)
    {
        if (Status != OrderStatus.Draft)
            throw new DomainException("Cannot modify confirmed order");
            
        var line = new OrderLine(product, quantity);
        _lines.Add(line);
        RecalculateTotal();
        
        RaiseDomainEvent(new OrderLineAddedEvent(Id, line.Id));
    }
    
    private void RecalculateTotal()
    {
        Total = _lines.Sum(l => l.Subtotal);
    }
}

// Poor - Anemic domain model (just data)
public class Order
{
    public List<OrderLine> Lines { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
}
```

#### Dependency Inversion
```csharp
// Excellent - Dependencies point inward
namespace Company.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(OrderId id);
        Task SaveAsync(Order order);
    }
}

// Infrastructure implements domain interfaces
namespace Company.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        
        public async Task<Order> GetByIdAsync(OrderId id)
        {
            var entity = await _context.Orders
                .Include(o => o.Lines)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
                
            return entity?.ToDomainModel();
        }
    }
}
```

### SOLID Principles

#### Single Responsibility Principle
```csharp
// Excellent - Single responsibility
public class OrderPricingCalculator
{
    public Money CalculateTotal(Order order)
    {
        var subtotal = order.Lines.Sum(l => l.Price * l.Quantity);
        var discount = _discountStrategy.Calculate(order);
        var tax = _taxCalculator.Calculate(subtotal - discount);
        
        return subtotal - discount + tax;
    }
}

// Poor - Multiple responsibilities
public class OrderService
{
    public void ProcessOrder(Order order)
    {
        // Pricing (responsibility 1)
        order.Total = order.Lines.Sum(l => l.Price * l.Quantity);
        
        // Persistence (responsibility 2)
        _db.Orders.Add(order);
        _db.SaveChanges();
        
        // Email notification (responsibility 3)
        _emailService.Send(order.CustomerEmail, "Order confirmed");
        
        // Logging (responsibility 4)
        _logger.LogInformation($"Order {order.Id} processed");
    }
}
```

### Architectural Patterns

#### CQRS (Command Query Responsibility Segregation)
```csharp
// Commands (write operations)
public record CreateOrderCommand(CustomerId CustomerId, List<OrderLineDto> Lines);

public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, OrderId>
{
    public async Task<OrderId> HandleAsync(CreateOrderCommand command)
    {
        var order = Order.Create(command.CustomerId);
        foreach (var line in command.Lines)
        {
            var product = await _productRepository.GetByIdAsync(line.ProductId);
            order.AddLine(product, line.Quantity);
        }
        
        await _orderRepository.SaveAsync(order);
        await _eventPublisher.PublishAsync(new OrderCreatedEvent(order.Id));
        
        return order.Id;
    }
}

// Queries (read operations)
public record GetOrderDetailsQuery(OrderId OrderId);

public class GetOrderDetailsHandler : IQueryHandler<GetOrderDetailsQuery, OrderDetailsDto>
{
    public async Task<OrderDetailsDto> HandleAsync(GetOrderDetailsQuery query)
    {
        // Optimized read model - no domain logic
        return await _dbContext.Orders
            .Where(o => o.Id == query.OrderId.Value)
            .Select(o => new OrderDetailsDto
            {
                OrderId = o.Id,
                CustomerName = o.Customer.Name,
                Total = o.Total,
                Lines = o.Lines.Select(l => new OrderLineDto
                {
                    ProductName = l.Product.Name,
                    Quantity = l.Quantity,
                    Price = l.Price
                }).ToList()
            })
            .SingleAsync();
    }
}
```

### Architecture Scoring (Senior Level)

| Score | Criteria |
|-------|----------|
| **97-100** | Exemplary architecture, reference implementation, follows all best practices |
| **93-96** | Excellent layering, strong SOLID adherence, proper patterns applied |
| **88-92** | Good architecture with minor coupling issues or pattern inconsistencies |
| **80-87** | Adequate structure, some architectural smells, needs improvement |
| **< 80** | Poor layering, tight coupling, architectural anti-patterns present |

---

# 2. Programming (Weight: 20%)

### Code Quality Expectations
Every line of code should be **clean, intentional, and maintainable**. Code is read 10x more than it's written.

### Clean Code Principles

#### Meaningful Names
```csharp
// Excellent - Intent-revealing names
public class OrderFulfillmentCoordinator
{
    private readonly IInventoryAvailabilityChecker _inventoryChecker;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly IShippingLabelGenerator _shippingLabelGenerator;
    
    public async Task<FulfillmentResult> FulfillOrderAsync(
        Order order,
        ShippingPreferences shippingPreferences)
    {
        // Clear, self-documenting code
    }
}

// Poor - Cryptic, abbreviated names
public class OrdProc
{
    private readonly IInvChk _ic;
    private readonly IPayProc _pp;
    
    public async Task<bool> Proc(Order o, Dictionary<string, object> prefs)
    {
        // What does this do?
    }
}
```

#### Functions/Methods
```csharp
// Excellent - Small, focused methods
public class OrderValidator
{
    public ValidationResult Validate(Order order)
    {
        var errors = new List<string>();
        
        ValidateOrderLines(order, errors);
        ValidateCustomerInformation(order, errors);
        ValidateShippingAddress(order, errors);
        ValidatePaymentMethod(order, errors);
        
        return new ValidationResult(errors);
    }
    
    private void ValidateOrderLines(Order order, List<string> errors)
    {
        if (!order.Lines.Any())
            errors.Add("Order must contain at least one line");
            
        if (order.Lines.Any(l => l.Quantity <= 0))
            errors.Add("All line quantities must be positive");
    }
    
    // ... other focused validation methods
}

// Poor - Long, complex methods
public bool ValidateOrder(Order order)
{
    if (order == null) return false;
    if (order.Lines == null || order.Lines.Count == 0) return false;
    foreach (var line in order.Lines)
    {
        if (line.Quantity <= 0) return false;
        if (line.Product == null) return false;
        if (string.IsNullOrWhiteSpace(line.Product.Name)) return false;
        // ... 50 more lines of validation logic
    }
    if (order.Customer == null) return false;
    if (string.IsNullOrWhiteSpace(order.Customer.Email)) return false;
    // ... continues for 200 lines
}
```

#### Error Handling
```csharp
// Excellent - Specific exception handling with context
public async Task<Order> GetOrderByIdAsync(OrderId orderId)
{
    try
    {
        var order = await _repository.GetByIdAsync(orderId);
        
        if (order == null)
        {
            _logger.LogWarning(
                "Order {OrderId} not found in repository", 
                orderId.Value);
            throw new OrderNotFoundException(orderId);
        }
        
        return order;
    }
    catch (DbException ex)
    {
        _logger.LogError(
            ex,
            "Database error while retrieving order {OrderId}", 
            orderId.Value);
        throw new DataAccessException(
            $"Failed to retrieve order {orderId.Value}", 
            ex);
    }
}

// Poor - Swallowed exceptions, empty catches
public Order GetOrder(string id)
{
    try
    {
        return _repository.GetById(id);
    }
    catch
    {
        // Silent failure - debugging nightmare
        return null;
    }
}
```

### Design Patterns

#### Strategy Pattern
```csharp
public interface IDiscountStrategy
{
    Money CalculateDiscount(Order order);
}

public class VolumeDiscountStrategy : IDiscountStrategy
{
    public Money CalculateDiscount(Order order)
    {
        var itemCount = order.Lines.Sum(l => l.Quantity);
        return itemCount > 100 
            ? order.Subtotal * 0.15m 
            : Money.Zero;
    }
}

public class SeasonalDiscountStrategy : IDiscountStrategy
{
    public Money CalculateDiscount(Order order)
    {
        return _clock.Now.Month == 12 
            ? order.Subtotal * 0.20m 
            : Money.Zero;
    }
}
```

#### Repository Pattern
```csharp
public interface IRepository<T, TId> where T : AggregateRoot
{
    Task<T> GetByIdAsync(TId id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(TId id);
}

public class OrderRepository : IRepository<Order, OrderId>
{
    private readonly AppDbContext _context;
    
    public async Task<Order> GetByIdAsync(OrderId id)
    {
        var entity = await _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product)
            .SingleOrDefaultAsync(o => o.Id == id.Value);
            
        return entity?.ToDomainModel();
    }
}
```

### Programming Scoring (Senior Level)

| Score | Criteria |
|-------|----------|
| **97-100** | Exemplary code quality, reference implementation, zero smells |
| **93-96** | Excellent coding practices, strong SOLID adherence, proper patterns |
| **88-92** | Good code quality with minor issues in naming or structure |
| **80-87** | Adequate but has code smells, needs refactoring |
| **< 80** | Poor code quality, multiple violations, technical debt present |

---

# 3. Data Management (Weight: 10%)

### Data Access Expectations
Efficient, safe, and maintainable data access patterns are critical for performance and reliability.

### Entity Framework Core Best Practices

#### Proper DbContext Usage
```csharp
// Excellent - DbContext properly scoped
public class OrderService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;
    
    public async Task<Order> ProcessOrderAsync(OrderId orderId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        
        var order = await context.Orders
            .Include(o => o.Lines)
            .SingleAsync(o => o.Id == orderId.Value);
            
        order.Process();
        
        await context.SaveChangesAsync();
        return order;
    }
}

// Poor - DbContext injected directly (wrong lifetime)
public class OrderService
{
    private readonly AppDbContext _context; // Singleton service with scoped context!
    
    public OrderService(AppDbContext context)
    {
        _context = context; // Memory leak risk
    }
}
```

#### Query Optimization
```csharp
// Excellent - Optimized query with projection
public async Task<IReadOnlyList<OrderSummaryDto>> GetOrderSummariesAsync(
    CustomerId customerId,
    int pageSize,
    int pageNumber)
{
    return await _context.Orders
        .Where(o => o.CustomerId == customerId.Value)
        .OrderByDescending(o => o.CreatedAt)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .Select(o => new OrderSummaryDto
        {
            OrderId = o.Id,
            Total = o.Total,
            ItemCount = o.Lines.Count,
            Status = o.Status
        })
        .AsNoTracking()
        .ToListAsync();
}

// Poor - N+1 query problem
public List<OrderSummaryDto> GetOrderSummaries(string customerId)
{
    var orders = _context.Orders
        .Where(o => o.CustomerId == customerId)
        .ToList(); // Loads all orders
        
    return orders.Select(o => new OrderSummaryDto
    {
        OrderId = o.Id,
        Total = o.Total,
        ItemCount = o.Lines.Count, // N+1: Loads lines for each order!
        CustomerName = o.Customer.Name // N+1: Loads customer for each order!
    }).ToList();
}
```

#### Proper Entity Configuration
```csharp
// Excellent - Fluent configuration
public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Orders", "Sales");
        
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .HasMaxLength(50)
            .IsRequired();
            
        builder.Property(o => o.Total)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
            
        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
            
        builder.HasMany(o => o.Lines)
            .WithOne(l => l.Order)
            .HasForeignKey(l => l.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(o => o.CustomerId);
        builder.HasIndex(o => o.CreatedAt);
    }
}

// Poor - Relying on conventions, no optimization
public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    
    // No configurations, no indexes, no constraints
}
```

### Read/Write Optimization (CQRS)

```csharp
// Write model - Full entity for business logic
public class OrderWriteModel : AggregateRoot
{
    private readonly List<OrderLine> _lines = new();
    
    public void AddLine(Product product, Quantity quantity)
    {
        // Business logic, validation, events
    }
    
    public void ApproveOrder(UserId approvedBy)
    {
        // State changes, domain events
    }
}

// Read model - Denormalized for efficient querying
public record OrderReadModel
{
    public string OrderId { get; init; }
    public string CustomerName { get; init; }
    public string CustomerEmail { get; init; }
    public decimal Total { get; init; }
    public int ItemCount { get; init; }
    public string Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public List<string> ProductNames { get; init; }
}
```

### Data Management Scoring (Senior Level)

| Score | Criteria |
|-------|----------|
| **97-100** | Exemplary data access, CQRS implemented, fully optimized queries |
| **93-96** | Excellent EF Core usage, proper configuration, query optimization |
| **88-92** | Good data access with minor inefficiencies |
| **80-87** | Adequate but has N+1 queries or missing indexes |
| **< 80** | Poor data access, performance issues, no optimization |

---

# 4. Testing and QA (Weight: 15%)

### Testing Expectations
Comprehensive testing is non-negotiable. Tests are **living documentation** and your **safety net** for refactoring.

### Test Coverage Requirements

#### Unit Tests
```csharp
// Excellent - AAA pattern, descriptive, isolated
[TestClass]
public class OrderTests
{
    [TestMethod]
    public void AddLine_WhenOrderIsDraft_ShouldAddLineAndRecalculateTotal()
    {
        // Arrange
        var order = Order.Create(new CustomerId("CUST-001"));
        var product = new Product("PROD-001", "Widget", Money.FromDecimal(10.00m));
        var quantity = new Quantity(5);
        
        // Act
        order.AddLine(product, quantity);
        
        // Assert
        order.Lines.Should().HaveCount(1);
        order.Lines.First().Product.Should().Be(product);
        order.Lines.First().Quantity.Should().Be(quantity);
        order.Total.Should().Be(Money.FromDecimal(50.00m));
    }
    
    [TestMethod]
    public void AddLine_WhenOrderIsConfirmed_ShouldThrowDomainException()
    {
        // Arrange
        var order = Order.Create(new CustomerId("CUST-001"));
        order.Confirm();
        var product = new Product("PROD-001", "Widget", Money.FromDecimal(10.00m));
        
        // Act
        var act = () => order.AddLine(product, new Quantity(1));
        
        // Assert
        act.Should().Throw<DomainException>()
            .WithMessage("Cannot modify confirmed order");
    }
}

// Poor - No clear arrange/act/assert, unclear intent
[Test]
public void Test1()
{
    var o = new Order();
    o.Lines = new List<OrderLine>();
    o.AddLine(new OrderLine { ProductId = "123", Qty = 5 });
    Assert.AreEqual(1, o.Lines.Count);
}
```

#### Integration Tests
```csharp
// Excellent - Real database, comprehensive scenario
[TestClass]
public class OrderRepositoryIntegrationTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly OrderRepository _repository;
    
    public OrderRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}")
            .Options;
            
        _context = new AppDbContext(options);
        _repository = new OrderRepository(_context);
    }
    
    [TestMethod]
    public async Task GetByIdAsync_WhenOrderExists_ShouldReturnOrderWithLines()
    {
        // Arrange
        var orderId = new OrderId("ORD-001");
        var order = Order.Create(new CustomerId("CUST-001"));
        order.AddLine(
            new Product("PROD-001", "Widget", Money.FromDecimal(10.00m)),
            new Quantity(2));
            
        await _repository.AddAsync(order);
        
        // Act
        var retrieved = await _repository.GetByIdAsync(orderId);
        
        // Assert
        retrieved.Should().NotBeNull();
        retrieved.Id.Should().Be(orderId);
        retrieved.Lines.Should().HaveCount(1);
    }
    
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
```

#### Architectural Tests (ArchUnitNET)
```csharp
[TestClass]
public class ArchitectureTests
{
    private static readonly Architecture Architecture = 
        new ArchLoader().LoadAssemblies(
            typeof(Order).Assembly,
            typeof(OrderRepository).Assembly,
            typeof(OrderService).Assembly)
        .Build();
    
    [TestMethod]
    public void DomainLayer_ShouldNotDependOnInfrastructure()
    {
        var rule = ArchRuleDefinition.Types()
            .That().ResideInNamespace("Company.Domain")
            .Should().NotDependOnAny("Company.Infrastructure");
            
        rule.Check(Architecture);
    }
    
    [TestMethod]
    public void Entities_ShouldNotHavePublicSetters()
    {
        var entities = ArchRuleDefinition.Classes()
            .That().ResideInNamespace("Company.Domain.Entities")
            .As("Domain Entities");
            
        foreach (var entity in entities.GetObjects(Architecture))
        {
            var publicSetters = entity.GetProperties()
                .Where(p => p.SetMethod?.IsPublic == true);
            
            publicSetters.Should().BeEmpty(
                $"{entity.Name} should not have public setters");
        }
    }
}
```

### Test Quality Checklist

- [ ] 85%+ line coverage, 80%+ branch coverage
- [ ] Mutation testing score >75% (Stryker.NET)
- [ ] All critical paths have tests
- [ ] Edge cases and boundary values tested
- [ ] Error paths tested comprehensively
- [ ] No flaky tests (deterministic, idempotent)
- [ ] Tests are fast (unit: <50ms, integration: <500ms)
- [ ] Minimal mocking (only external dependencies)

### Testing Scoring (Senior Level)

| Score | Criteria |
|-------|----------|
| **97-100** | Exemplary testing, >90% coverage, mutation testing, architectural tests |
| **93-96** | Excellent coverage (>85%), comprehensive test suite |
| **88-92** | Good coverage (>80%), minor gaps acceptable |
| **80-87** | Adequate (>75%), missing some edge cases |
| **< 80** | Insufficient testing, critical gaps present |

---

# 5. App Driver (Front End/Jobs) (Weight: 10%)

**Note:** This section applies to projects with UI (web, desktop, mobile) or background jobs. Mark as N/A if not applicable.

### UI/Presentation Layer Expectations

#### Razor Pages / MVC Best Practices
```csharp
// Excellent - Thin controller, delegating to services
public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly ILogger<OrdersController> _logger;
    
    public OrdersController(
        IOrderService orderService,
        IMapper mapper,
        ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOrderViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
            
        try
        {
            var command = _mapper.Map<CreateOrderCommand>(model);
            var orderId = await _orderService.CreateOrderAsync(command);
            
            _logger.LogInformation(
                "Order {OrderId} created successfully", 
                orderId);
                
            return RedirectToAction(nameof(Details), new { id = orderId });
        }
        catch (BusinessRuleException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }
}

// Poor - Fat controller with business logic
public class OrdersController : Controller
{
    private readonly AppDbContext _db;
    
    [HttpPost]
    public IActionResult Create(Order order)
    {
        // Business logic in controller!
        order.Total = order.Lines.Sum(l => l.Price * l.Quantity);
        
        if (order.Total > 1000)
            order.Discount = order.Total * 0.10m;
            
        _db.Orders.Add(order);
        _db.SaveChanges();
        
        // Email logic in controller!
        var smtp = new SmtpClient("smtp.example.com");
        smtp.Send("from@example.com", order.Email, "Order confirmed", "Thank you");
        
        return View();
    }
}
```

#### ViewModels
```csharp
// Excellent - Proper view model with validation
public class CreateOrderViewModel
{
    [Required(ErrorMessage = "Customer is required")]
    public string CustomerId { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "At least one order line is required")]
    public List<OrderLineViewModel> Lines { get; set; }
    
    [Display(Name = "Shipping Address")]
    [Required]
    public AddressViewModel ShippingAddress { get; set; }
    
    public decimal EstimatedTotal => Lines?.Sum(l => l.Subtotal) ?? 0;
}

public class OrderLineViewModel
{
    [Required]
    public string ProductId { get; set; }
    
    [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Subtotal => Price * Quantity;
}

// Poor - Using domain entities directly in views
@model Order

<form asp-action="Create">
    @* Direct binding to domain entity - bad practice *@
    <input asp-for="CustomerId" />
    <input asp-for="Total" /> @* Exposing calculated fields for manipulation *@
</form>
```

#### Background Jobs / Hosted Services
```csharp
// Excellent - Proper hosted service with error handling
public class OrderProcessingBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderProcessingBackgroundService> _logger;
    
    public OrderProcessingBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<OrderProcessingBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Order Processing Service started");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await using var scope = _serviceProvider.CreateAsyncScope();
                var processor = scope.ServiceProvider.GetRequiredService<IOrderProcessor>();
                
                await processor.ProcessPendingOrdersAsync(stoppingToken);
                
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Order Processing Service is stopping");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing orders");
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
        
        _logger.LogInformation("Order Processing Service stopped");
    }
}

// Poor - No error handling, improper scoping
public class OrderProcessingService : BackgroundService
{
    private readonly IOrderProcessor _processor; // Wrong lifetime!
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            await _processor.ProcessOrders(); // No error handling
            Thread.Sleep(60000); // Blocking sleep
        }
    }
}
```

### App Driver Scoring (Senior Level)

| Score | Criteria |
|-------|----------|
| **97-100** | Exemplary UI/job architecture, proper separation, comprehensive validation |
| **93-96** | Excellent controller/service design, proper ViewModels |
| **88-92** | Good structure with minor issues in separation |
| **80-87** | Adequate but has some logic in controllers/views |
| **< 80** | Poor separation, business logic in presentation layer |

---

# 6. Security (Weight: 10%)

**Note:** This section applies to projects with security requirements. Mark as N/A if not applicable.

### Security Expectations
You are the **last line of defense** against vulnerabilities. Security must be **proactive, layered, and comprehensive**.

### Defense in Depth

#### Multi-Layered Authorization
```csharp
// Excellent - Multi-layered security
[Authorize]
[RequireHttps]
[ServiceFilter(typeof(RateLimitingFilter))]
public class OrdersController : ControllerBase
{
    [HttpDelete("{id}")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public async Task<IActionResult> DeleteOrder(
        string id,
        [FromServices] IAuthorizationService authService)
    {
        var order = await _orderService.GetByIdAsync(new OrderId(id));
        
        // Resource-based authorization
        var authResult = await authService.AuthorizeAsync(
            User,
            order,
            "CanDeleteOrder");
            
        if (!authResult.Succeeded)
            return Forbid();
            
        await _orderService.DeleteAsync(order.Id);
        
        _logger.LogWarning(
            "Order {OrderId} deleted by user {UserId}",
            id,
            User.FindFirstValue(ClaimTypes.NameIdentifier));
            
        return NoContent();
    }
}

// Poor - Single authorization layer, no resource check
[Authorize]
public class OrdersController : ControllerBase
{
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _db.Orders.Where(o => o.Id == id).ExecuteDeleteAsync();
        return Ok();
    }
}
```

#### Input Validation & Sanitization
```csharp
// Excellent - Comprehensive validation
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .MaximumLength(50)
            .Matches(@"^CUST-\d{6}$")
            .WithMessage("Invalid customer ID format");
            
        RuleFor(x => x.Lines)
            .NotEmpty()
            .Must(lines => lines.Count <= 100)
            .WithMessage("Cannot have more than 100 lines per order");
            
        RuleForEach(x => x.Lines)
            .SetValidator(new OrderLineValidator());
            
        RuleFor(x => x.ShippingAddress)
            .NotNull()
            .SetValidator(new AddressValidator());
    }
}

// Poor - No validation, SQL injection risk
public async Task<Order> CreateOrder(string customerId, string productIds)
{
    // SQL injection vulnerability!
    var sql = $"INSERT INTO Orders (CustomerId, ProductIds) VALUES ('{customerId}', '{productIds}')";
    await _db.Database.ExecuteSqlRawAsync(sql);
}
```

#### Secrets Management
```csharp
// Excellent - Using Azure Key Vault / User Secrets
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var keyVaultUrl = Configuration["KeyVault:Url"];
        var credential = new DefaultAzureCredential();
        
        Configuration = new ConfigurationBuilder()
            .AddConfiguration(Configuration)
            .AddAzureKeyVault(new Uri(keyVaultUrl), credential)
            .Build();
            
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                Configuration["ConnectionStrings:AppDb"])); // From Key Vault
    }
}

// Poor - Hardcoded secrets
public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            "Server=prod-db.example.com;Database=Orders;User=sa;Password=P@ssw0rd123"); // Hardcoded!
    }
}
```

### Security Scoring (Senior Level)

| Score | Criteria |
|-------|----------|
| **97-100** | Exemplary security, defense in depth, zero vulnerabilities |
| **93-96** | Excellent security practices, OWASP compliance |
| **88-92** | Good security with minor gaps |
| **80-87** | Adequate but missing some security layers |
| **< 80** | Critical vulnerabilities, poor security practices |

---

# 7. Monitoring and Reporting (Weight: 5%)

### Observability Expectations
Production systems must be **observable, debuggable, and monitorable** without requiring code changes.

### Structured Logging
```csharp
// Excellent - Structured logging with context
public class OrderService
{
    private readonly ILogger<OrderService> _logger;
    
    public async Task<Order> CreateOrderAsync(CreateOrderCommand command)
    {
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CustomerId"] = command.CustomerId,
            ["CorrelationId"] = Activity.Current?.Id ?? Guid.NewGuid().ToString()
        }))
        {
            _logger.LogInformation(
                "Creating order for customer {CustomerId} with {LineCount} lines",
                command.CustomerId,
                command.Lines.Count);
                
            try
            {
                var order = await _orderRepository.CreateAsync(command);
                
                _logger.LogInformation(
                    "Order {OrderId} created successfully with total {Total:C}",
                    order.Id,
                    order.Total);
                    
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to create order for customer {CustomerId}",
                    command.CustomerId);
                throw;
            }
        }
    }
}

// Poor - String concatenation, no context
public void CreateOrder(Order order)
{
    Console.WriteLine("Creating order: " + order.Id);
    
    try
    {
        _db.Add(order);
        _db.SaveChanges();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message); // Lost context!
    }
}
```

### Distributed Tracing
```csharp
// Excellent - OpenTelemetry integration
public class OrderService
{
    private static readonly ActivitySource ActivitySource = 
        new("Company.OrderService");
        
    public async Task<Order> ProcessOrderAsync(OrderId orderId)
    {
        using var activity = ActivitySource.StartActivity("ProcessOrder");
        activity?.SetTag("order.id", orderId.Value);
        activity?.SetTag("customer.id", order.CustomerId.Value);
        
        try
        {
            var order = await _repository.GetByIdAsync(orderId);
            activity?.SetTag("order.total", order.Total.Amount);
            
            await ProcessPaymentAsync(order);
            await AllocateInventoryAsync(order);
            await GenerateShippingLabelAsync(order);
            
            activity?.SetStatus(ActivityStatusCode.Ok);
            return order;
        }
        catch (Exception ex)
{
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            activity?.RecordException(ex);
            throw;
        }
    }
}
```

### Metrics & Health Checks
```csharp
// Excellent - Custom metrics
public class OrderMetrics
{
    private readonly Counter<long> _ordersCreated;
    private readonly Histogram<double> _orderValue;
    
    public OrderMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("Company.Orders");
        
        _ordersCreated = meter.CreateCounter<long>(
            "orders.created",
            description: "Number of orders created");
            
        _orderValue = meter.CreateHistogram<double>(
            "orders.value",
            unit: "USD",
            description: "Order value distribution");
    }
    
    public void RecordOrderCreated(Order order)
    {
        _ordersCreated.Add(1, new KeyValuePair<string, object>("customer", order.CustomerId));
        _orderValue.Record(order.Total.Amount);
    }
}

// Health checks
public class DatabaseHealthCheck : IHealthCheck
{
    private readonly AppDbContext _context;
    
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Database.ExecuteSqlRawAsync(
                "SELECT 1",
                cancellationToken);
                
            return HealthCheckResult.Healthy("Database is reachable");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                "Database is unreachable",
                ex);
        }
    }
}
```

### Monitoring Scoring (Senior Level)

| Score | Criteria |
|-------|----------|
| **97-100** | Exemplary observability, distributed tracing, comprehensive metrics |
| **93-96** | Excellent structured logging, health checks, metrics |
| **88-92** | Good logging with minor gaps in metrics or tracing |
| **80-87** | Adequate logging but missing metrics or health checks |
| **< 80** | Poor observability, debugging will be difficult |

---

# 8. Performance and Optimization (Weight: 5%)

### Performance Expectations
Performance is a **feature**, not an afterthought. Code must be **measured, optimized, and scalable**.

### Query Optimization
```csharp
// Excellent - Optimized query with pagination
public async Task<PagedResult<OrderSummaryDto>> GetOrdersAsync(
    CustomerId customerId,
    int pageNumber,
    int pageSize)
{
    var totalCount = await _context.Orders
        .CountAsync(o => o.CustomerId == customerId.Value);
        
    var orders = await _context.Orders
        .Where(o => o.CustomerId == customerId.Value)
        .OrderByDescending(o => o.CreatedAt)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .Select(o => new OrderSummaryDto
        {
            OrderId = o.Id,
            Total = o.Total,
            ItemCount = o.Lines.Count,
            Status = o.Status
        })
        .AsNoTracking()
        .ToListAsync();
        
    return new PagedResult<OrderSummaryDto>(
        orders,
        totalCount,
        pageNumber,
        pageSize);
}

// Poor - Loading everything into memory
public List<Order> GetOrders(string customerId)
{
    return _context.Orders
        .Include(o => o.Lines)
        .ThenInclude(l => l.Product)
        .Include(o => o.Customer)
        .ToList() // Loads entire table!
        .Where(o => o.CustomerId == customerId)
        .ToList();
}
```

### Caching
```csharp
// Excellent - Distributed caching
public class CachedProductRepository : IProductRepository
{
    private readonly IProductRepository _innerRepository;
    private readonly IDistributedCache _cache;
    private readonly ILogger<CachedProductRepository> _logger;
    
    public async Task<Product> GetByIdAsync(ProductId id)
    {
        var cacheKey = $"product:{id.Value}";
        
        var cached = await _cache.GetStringAsync(cacheKey);
        if (cached != null)
        {
            _logger.LogDebug("Cache hit for product {ProductId}", id.Value);
            return JsonSerializer.Deserialize<Product>(cached);
        }
        
        _logger.LogDebug("Cache miss for product {ProductId}", id.Value);
        var product = await _innerRepository.GetByIdAsync(id);
        
        if (product != null)
        {
            var serialized = JsonSerializer.Serialize(product);
            await _cache.SetStringAsync(
                cacheKey,
                serialized,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                });
        }
        
        return product;
    }
}

// Poor - No caching, repeated DB hits
public async Task<Product> GetProduct(string id)
{
    return await _db.Products.FindAsync(id); // Every call hits DB
}
```

### Async/Await Best Practices
```csharp
// Excellent - Proper async usage
public async Task<OrderResult> ProcessOrderAsync(Order order)
{
    var paymentTask = _paymentService.ProcessPaymentAsync(order);
    var inventoryTask = _inventoryService.AllocateInventoryAsync(order);
    var shippingTask = _shippingService.CreateShipmentAsync(order);
    
    await Task.WhenAll(paymentTask, inventoryTask, shippingTask);
    
    var payment = await paymentTask;
    var inventory = await inventoryTask;
    var shipping = await shippingTask;
    
    return new OrderResult(payment, inventory, shipping);
}

// Poor - Blocking on async calls
public OrderResult ProcessOrder(Order order)
{
    var payment = _paymentService.ProcessPaymentAsync(order).Result; // Deadlock risk!
    var inventory = _inventoryService.AllocateInventoryAsync(order).Result;
    var shipping = _shippingService.CreateShipmentAsync(order).Result;
    
    return new OrderResult(payment, inventory, shipping);
}
```

### Performance Scoring (Senior Level)

| Score | Criteria |
|-------|----------|
| **97-100** | Exemplary performance, profiled, caching, optimized algorithms |
| **93-96** | Excellent query optimization, proper async usage |
| **88-92** | Good performance with minor inefficiencies |
| **80-87** | Adequate but has some performance issues |
| **< 80** | Poor performance, N+1 queries, blocking calls |

---

# 9. Misc (Documentation + Others) (Weight: 5%)

### Documentation Expectations
Code should be **self-documenting**, but critical decisions need **explicit documentation**.

### Architecture Decision Records (ADRs)
```markdown
# ADR-001: Use CQRS for Order Management

## Status
Accepted (2025-11-30)

## Context
Order queries are read-heavy (10:1 read/write ratio). Complex reporting 
requirements with multiple join operations causing performance issues. 
Need to optimize read operations without compromising write consistency.

## Decision
Implement CQRS (Command Query Responsibility Segregation) with:
- Separate write models (aggregates) for business logic
- Denormalized read models for efficient querying
- Event sourcing for audit trail (future consideration)

## Consequences

### Positive
- Query performance improved by 80% (measured)
- Read models optimized independently of write models
- Clear separation of concerns
- Better scalability

### Negative
- Increased complexity (two models to maintain)
- Eventual consistency between write and read models
- Learning curve for team members unfamiliar with CQRS

## Alternatives Considered
1. **Database views** - Rejected due to limited flexibility and performance
2. **Caching layer** - Rejected due to cache invalidation complexity
3. **Materialized views** - Rejected due to database-specific implementation

## Implementation Notes
- Use MediatR for command/query handling
- Redis for eventual consistency notifications
- Background service to update read models from domain events
```

### XML Documentation
```csharp
/// <summary>
/// Processes an order through the complete fulfillment workflow.
/// </summary>
/// <param name="orderId">The unique identifier of the order to process.</param>
/// <param name="cancellationToken">Cancellation token for async operation.</param>
/// <returns>
/// A <see cref="FulfillmentResult"/> indicating the outcome of the fulfillment process.
/// </returns>
/// <exception cref="OrderNotFoundException">
/// Thrown when the specified order does not exist.
/// </exception>
/// <exception cref="InsufficientInventoryException">
/// Thrown when inventory cannot be allocated for the order.
/// </exception>
/// <remarks>
/// This method orchestrates the following steps:
/// 1. Validates order eligibility for fulfillment
/// 2. Processes payment authorization
/// 3. Allocates inventory from warehouse
/// 4. Generates shipping labels
/// 5. Updates order status to 'Processing'
/// 
/// The operation is transactional and will rollback on any failure.
/// </remarks>
public async Task<FulfillmentResult> FulfillOrderAsync(
    OrderId orderId,
    CancellationToken cancellationToken = default)
{
    // Implementation
}
```

### README & Runbooks
```markdown
# Order Management Service

## Overview
Microservice responsible for order lifecycle management including creation, 
payment processing, fulfillment, and cancellation.

## Architecture
- **Pattern**: Clean Architecture with CQRS
- **Framework**: .NET 10 / C# 14
- **Database**: SQL Server with Entity Framework Core
- **Messaging**: Azure Service Bus
- **Caching**: Redis

## Local Development Setup

### Prerequisites
- .NET 10 SDK
- Docker Desktop
- SQL Server 2022 (or Docker container)
- Redis (or Docker container)

### Quick Start
```bash
# Clone repository
git clone https://github.com/company/order-service.git
cd order-service

# Start dependencies
docker-compose up -d

# Apply migrations
dotnet ef database update

# Run application
dotnet run --project src/OrderService.API

# Run tests
dotnet test
```

## Configuration
See `appsettings.Development.json` for local configuration.
Production settings use Azure App Configuration.

## Deployment
Deployed via Azure DevOps CI/CD pipeline. See `/docs/deployment.md`.

## Monitoring
- **Metrics**: Application Insights
- **Logs**: Azure Log Analytics
- **Alerts**: Configured in Azure Monitor

## Support
- **Team**: Orders Team
- **Slack**: #orders-dev
- **Runbook**: `/docs/runbook.md`
```

### Misc Scoring (Senior Level)

| Score | Criteria |
|-------|----------|
| **97-100** | Exemplary documentation, comprehensive ADRs, runbooks |
| **93-96** | Excellent documentation, clear XML docs, README |
| **88-92** | Good documentation with minor gaps |
| **80-87** | Adequate but missing some critical documentation |
| **< 80** | Poor documentation, difficult to understand or maintain |

---

# Final Score Calculation (Senior Level)

```
Final Score = (Solution Architecture × 0.20) +
              (Programming × 0.20) +
              (Data Management × 0.10) +
              (Testing and QA × 0.15) +
              (App Driver × 0.10) +
              (Security × 0.10) +
              (Monitoring and Reporting × 0.05) +
              (Performance and Optimization × 0.05) +
              (Misc × 0.05)
```

### Dynamic Weight Adjustment Formula

When category is marked N/A:
```
New Weight (Category X) = Original Weight (Category X) × (100 / Sum of Applicable Weights)
```

**Example:**
If Security (10%) is N/A:
- Applicable weight sum = 90%
- Adjustment factor = 100 / 90 = 1.111
- New Solution Architecture weight = 20% × 1.111 = 22.22%
- New Programming weight = 20% × 1.111 = 22.22%
- And so on for all applicable categories...

### Production Readiness Gate

| Score | Status | Action |
|-------|--------|--------|
| **97-100** | Exceptional | Reference implementation, consider for tech talks |
| **93-96** | Excellent | Approved for production, minimal feedback |
| **88-92** | Good | Approved with minor improvements recommended |
| **80-87** | Adequate | Refactor recommended before merge |
| **< 80** | Below Standard | Architecture review required, blocks merge |

**Critical:** Score < 85 blocks merge and requires technical lead sign-off.

---

## Review Output Template

```markdown
# Code Review Report

**Date:** YYYY-MM-DD  
**Reviewer:** [Name]  
**Project:** [Project Name]  
**Commit/PR:** [Commit Hash or PR Number]

## Executive Summary
[High-level assessment, key findings, final score]

## Category Scores

| Category | Score | Weight | Weighted Score | Status |
|----------|-------|--------|----------------|--------|
| Solution Architecture | XX/100 | 20% | X.XX | ?/?/? |
| Programming | XX/100 | 20% | X.XX | ?/?/? |
| Data Management | XX/100 | 10% | X.XX | ?/?/? |
| Testing and QA | XX/100 | 15% | X.XX | ?/?/? |
| App Driver | XX/100 | 10% | X.XX | ?/?/? |
| Security | XX/100 | 10% | X.XX | ?/?/? |
| Monitoring | XX/100 | 5% | X.XX | ?/?/? |
| Performance | XX/100 | 5% | X.XX | ?/?/? |
| Misc (Docs) | XX/100 | 5% | X.XX | ?/?/? |
| **TOTAL** | **XX/100** | **100%** | **XX.XX** | **Status** |

## Solution Organization (Commentary)
[Assessment of solution structure against pattern templates - not scored]

## Detailed Findings

### 1. Solution Architecture (XX/100)
#### Strengths
- [Positive finding 1]
- [Positive finding 2]

#### Issues
- **[P0]** [Critical issue] - [Recommendation]
- **[P1]** [Important issue] - [Recommendation]

#### Recommendations
- [Architectural improvement 1]
- [Architectural improvement 2]

[Repeat for each category...]

## Action Items

### Priority 0 (Critical - Must Fix Before Merge)
- [ ] [Action item 1]
- [ ] [Action item 2]

### Priority 1 (Important - Fix in Next Iteration)
- [ ] [Action item 1]
- [ ] [Action item 2]

### Priority 2 (Nice to Have - Backlog)
- [ ] [Action item 1]
- [ ] [Action item 2]

## Final Verdict
**Score:** XX/100  
**Status:** [Exceptional/Excellent/Good/Adequate/Below Standard]  
**Decision:** [APPROVED / APPROVED WITH CONDITIONS / REQUIRES CHANGES]

[Final commentary and strategic recommendations]

---
**Reviewed by:** [Senior Developer Name]  
**Next Review:** [If applicable]
```

---

**Version History:**
- **v3.1-Senior (2025-01-15)** - Reorganized categories, added Solution Organization, weight adjustment guidance
- **v3.0-Senior (2025-11-30)** - Initial senior-focused template

**Maintained By:** Technical Leadership Team
