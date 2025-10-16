
# 💡 Dependency Injection in ASP.NET Core — Interface vs Abstract Class

Author: Suraj Tripathi  
Date: October 2025  

---

## 🧩 1. What is Dependency Injection (DI)?

**Dependency Injection (DI)** is a design pattern used to achieve **loose coupling** between classes.  
Instead of creating dependencies manually using `new`, the DI container **provides the dependency** automatically.

### Example
```csharp
public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public void RegisterUser(string name)
    {
        _repository.AddUser(name);
    }
}
```

---

## ⚙️ 2. Interface-Based Dependency Injection (✅ Recommended)

### Interface
```csharp
public interface IUserRepository
{
    Task AddUserAsync(string name);
    Task<IEnumerable<string>> GetAllUsersAsync();
}
```

### Implementation
```csharp
public class SqlUserRepository : IUserRepository
{
    private readonly List<string> _users = new();

    public Task AddUserAsync(string name)
    {
        _users.Add(name);
        Console.WriteLine($"[SQL] Added user: {name}");
        return Task.CompletedTask;
    }

    public Task<IEnumerable<string>> GetAllUsersAsync()
    {
        return Task.FromResult(_users.AsEnumerable());
    }
}
```

### Service Layer
```csharp
public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task RegisterUserAsync(string name)
    {
        await _repository.AddUserAsync(name);
    }
}
```

### Program.cs
```csharp
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<UserService>();
```

✅ Flexible  
✅ Testable  
✅ Supports multiple implementations  

---

## ⚠️ 3. Abstract Class-Based DI (Possible but Not Ideal)

### Abstract Class
```csharp
public abstract class UserRepositoryBase
{
    public abstract Task AddUserAsync(string name);
    public abstract Task<IEnumerable<string>> GetAllUsersAsync();
}
```

### Implementation
```csharp
public class SqlUserRepository : UserRepositoryBase
{
    private readonly List<string> _users = new();

    public override Task AddUserAsync(string name)
    {
        _users.Add(name);
        Console.WriteLine($"[SQL] Added user: {name}");
        return Task.CompletedTask;
    }

    public override Task<IEnumerable<string>> GetAllUsersAsync()
    {
        return Task.FromResult(_users.AsEnumerable());
    }
}
```

### Service Layer
```csharp
public class UserService
{
    private readonly UserRepositoryBase _repository;

    public UserService(UserRepositoryBase repository)
    {
        _repository = repository;
    }

    public async Task RegisterUserAsync(string name)
    {
        await _repository.AddUserAsync(name);
    }
}
```

### Program.cs
```csharp
builder.Services.AddScoped<UserRepositoryBase, SqlUserRepository>();
builder.Services.AddScoped<UserService>();
```

✅ Works  
⚠️ But less flexible and harder to extend  

---

## 🧠 4. Why Interface is Better for DI

| Feature | **Interface-based DI** | **Abstract class-based DI** |
|----------|------------------------|-----------------------------|
| Multiple inheritance | ✅ Yes | ❌ No |
| Mocking for testing | ✅ Easy | ⚠️ Harder |
| Represents | Pure contract | Partial base implementation |
| Supports multiple abstractions | ✅ Yes | ❌ No |
| Best suited for | Loose coupling, DI, mocking | Code sharing |
| Example use | `IUserRepository`, `ILogger`, `IEmailService` | `BaseRepository`, `BaseController` |

---

## 🧪 5. Testing Example (Mocking an Interface)

```csharp
public class FakeUserRepository : IUserRepository
{
    public Task AddUserAsync(string name) => Task.CompletedTask;
    public Task<IEnumerable<string>> GetAllUsersAsync() => Task.FromResult(Enumerable.Empty<string>());
}

[Test]
public async Task TestRegisterUser()
{
    var fakeRepo = new FakeUserRepository();
    var service = new UserService(fakeRepo);

    await service.RegisterUserAsync("Suraj");

    Assert.Pass("Mock test successful");
}
```

✅ Very easy to mock using an interface  
⚠️ Abstract class mocking requires extra effort with frameworks like **Moq**.

---

## 💬 6. Interview-Perfect Summary Answer

> “Technically, we can inject abstract classes just like interfaces, since both define abstract members.  
> But in practice, interfaces are preferred for dependency injection because they represent **pure contracts** without state or implementation.  
> They enable **loose coupling**, **multiple inheritance**, and **easy testing** — while abstract classes are meant for sharing base logic among related types.”

---

## ✅ 7. Key Takeaways

| Concept | Interface | Abstract Class |
|----------|------------|----------------|
| Purpose | Contract definition | Base implementation |
| DI Support | Perfect fit | Possible but limited |
| State/Fields | No | Yes |
| Mocking | Easy | Complex |
| Multiple inheritance | Yes | No |
| Best Use | Loose coupling, DI | Shared logic |
| Real Example | `IRepository`, `ILogger`, `IService` | `BaseController`, `BaseEntity` |

---

### 🧩 Visual Representation

```
Interface-based DI
UserService → IUserRepository → SqlUserRepository / MongoUserRepository / FakeUserRepository

Abstract class-based DI
UserService → UserRepositoryBase → SqlUserRepository (single inheritance only)
```

---

## 🧱 8. Final Note

> Use **interfaces** for defining contracts (especially when using DI).  
> Use **abstract classes** when you want to share common code or base logic.

---

Happy Coding! 🚀  
*Created by Suraj Tripathi for .NET Core Interview & Revision Notes*
