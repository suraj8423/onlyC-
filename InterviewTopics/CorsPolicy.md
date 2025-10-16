# 🧠 Understanding CORS (Cross-Origin Resource Sharing) in ASP.NET Core

## 📘 What is CORS?
CORS (**Cross-Origin Resource Sharing**) is a **security feature** implemented by browsers that restricts web pages from making HTTP requests to a different domain (origin) than the one that served the web page.

---

## ⚙️ Why Do We Need CORS?
When your **frontend** (React, Angular, etc.) runs on a different domain/port than your **backend API**, the browser blocks requests by default.

Example:

- Frontend: `http://localhost:3000`
- Backend API: `http://localhost:5000`

A fetch request from the frontend like:
```js
fetch("http://localhost:5000/api/users");
```
will be blocked with this error:

```
Access to fetch at 'http://localhost:5000/api/users' from origin 'http://localhost:3000' has been blocked by CORS policy.
```

---

## 🧩 How CORS Works
When a browser sends a request to a different origin, it checks the **response headers**.

If the response includes:
```
Access-Control-Allow-Origin: http://localhost:3000
```
the browser allows the request.

Otherwise, it blocks it.

---

## 🧠 Types of Requests

### 1. Simple Request
Only happens for methods like **GET, HEAD, POST** with basic headers.

### 2. Preflight Request (OPTIONS)
For requests that include **custom headers** or methods like PUT, DELETE.

The browser first sends an `OPTIONS` request to check if the origin is allowed.

---

## ✅ Enabling CORS in ASP.NET Core

### 🔹 For .NET 5 or below — `Startup.cs`
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicy("AllowReactApp", builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });

    services.AddControllers();
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRouting();
    app.UseCors("AllowReactApp");
    app.UseEndpoints(endpoints => endpoints.MapControllers());
}
```

### 🔹 For .NET 6+ — `Program.cs`
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowFrontend");
app.MapControllers();
app.Run();
```

---

## 🧩 Advanced Configurations

### 1️⃣ Allow Multiple Origins
```csharp
.WithOrigins("http://localhost:3000", "https://myapp.com")
```

### 2️⃣ Allow Credentials (Cookies, Tokens)
```csharp
.WithOrigins("http://localhost:3000")
.AllowCredentials()
.AllowAnyHeader()
.AllowAnyMethod();
```
> ⚠️ You cannot use `AllowAnyOrigin()` with `AllowCredentials()`.

### 3️⃣ Allow All Origins (Public APIs)
```csharp
.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod();
```

---

## 🧰 Attribute-Based CORS

### Apply CORS to a Specific Controller
```csharp
using Microsoft.AspNetCore.Cors;

[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowReactApp")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(new[] { "Laptop", "Phone", "Tablet" });
    }
}
```

### Disable CORS for a Controller
```csharp
[DisableCors]
public class SecureController : ControllerBase
{
    [HttpGet("admin")]
    public IActionResult GetAdminData()
    {
        return Ok("Admin access only");
    }
}
```

---

## 🧾 Common Mistakes

| Mistake | Description |
|----------|--------------|
| ❌ Placing `app.UseCors()` after `app.UseEndpoints()` | Middleware order matters |
| ❌ Using `AllowAnyOrigin()` with `AllowCredentials()` | Not allowed by CORS spec |
| ❌ Mismatch in origin URL (missing port or https) | Must exactly match frontend origin |

---

## 🧩 Real World Scenarios

| Scenario | Frontend | Backend | Solution |
|-----------|-----------|----------|-----------|
| Both same port | localhost:5000 | localhost:5000 | No CORS needed |
| Different ports | localhost:3000 | localhost:5000 | Enable CORS |
| Public API | any | api.myapp.com | AllowAnyOrigin() |
| Cookies/Session | localhost:3000 | localhost:5000 | AllowCredentials() |
| Only one route | - | /api/products | [EnableCors] |

---

## 🧭 CORS Flow Diagram

```text
[React App] ---> [Browser Sends OPTIONS Request] ---> [ASP.NET API]
         <--- Access-Control-Allow-* Headers ---
         ---> [Browser Sends Actual Request] ---> [ASP.NET API]
```

---

## ✅ Summary

| Concept | Description |
|----------|--------------|
| **CORS** | Mechanism allowing cross-origin requests |
| **Purpose** | Let browsers safely call APIs from different origins |
| **Setup** | Add CORS policy → Apply via middleware |
| **Best Practice** | Use named policies and apply specifically |
| **Remember** | `UseCors()` must be between `UseRouting()` and `UseEndpoints()` |

---

🧾 **Pro Tip:** Always start with a *restrictive* CORS setup and open it gradually as needed.
