# ⚙️ Error Handling in ASP.NET Core

A complete guide on how error handling works in ASP.NET Core applications — including **local handling**, **global middleware**, **extension methods**, and **unhandled scenarios**.

---

## 🧩 Table of Contents
1. [Introduction](#introduction)
2. [Global Error Handling Middleware](#global-error-handling-middleware)
3. [Extension Method](#extension-method)
4. [Controller Example](#controller-example)
5. [Program.cs Configuration](#programcs-configuration)
6. [How It Works](#how-it-works)
7. [Exception Bubbling Explained](#exception-bubbling-explained)
8. [What If No One Handles the Exception?](#what-if-no-one-handles-the-exception)
9. [Flow Summary Table](#flow-summary-table)
10. [Bonus Tip for Interviews](#bonus-tip-for-interviews)

---

## 🧠 Introduction

Error handling in ASP.NET Core ensures that your application can handle unexpected exceptions gracefully.

There are **3 levels** where you can manage errors:

1. **Local (try-catch inside controller)**  
2. **Global (custom middleware)**  
3. **Framework default (developer page or 500 response)**

The best practice is to implement **centralized global exception handling** using middleware.

---

## 🧱 Global Error Handling Middleware

**File:** `Middleware/ErrorHandlingMiddleware.cs`

```csharp
using System.Net;
using System.Text.Json;

namespace MyApp.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Continue the pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ArgumentException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = exception.Message,
                TimeStamp = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
```

---

## 🧩 Extension Method

**File:** `Extensions/ExceptionMiddlewareExtensions.cs`

```csharp
using Microsoft.AspNetCore.Builder;
using MyApp.Middleware;

namespace MyApp.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseCustomErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
```

✅ This keeps `Program.cs` clean and readable.

---

## 🧪 Controller Example

**File:** `Controllers/DemoController.cs`

```csharp
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        [HttpGet("handled")]
        public IActionResult GetHandled()
        {
            try
            {
                throw new ArgumentException("Bad input from user!");
            }
            catch (ArgumentException ex)
            {
                // Handled locally — won't reach global middleware
                return BadRequest(new { Message = "Handled locally: " + ex.Message });
            }
        }

        [HttpGet("unhandled")]
        public IActionResult GetUnhandled()
        {
            // Not caught — will reach global middleware
            throw new InvalidOperationException("Something went wrong in processing!");
        }
    }
}
```

---

## 🏗️ Program.cs Configuration

**File:** `Program.cs`

```csharp
using MyApp.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// ✅ Register global exception middleware
app.UseCustomErrorHandling();

app.MapControllers();

app.Run();
```

---

## ⚙️ How It Works

### When `/api/demo/handled` is called:
- `ArgumentException` is thrown.
- Caught in the local `try-catch`.
- Global middleware is **not triggered**.
- Response:
  ```json
  {
    "Message": "Handled locally: Bad input from user!"
  }
  ```

### When `/api/demo/unhandled` is called:
- `InvalidOperationException` is thrown.
- No local catch block.
- Global middleware catches it.
- Response:
  ```json
  {
    "StatusCode": 500,
    "Message": "Something went wrong in processing!",
    "TimeStamp": "2025-10-16T17:45:00Z"
  }
  ```

---

## 🧠 Exception Bubbling Explained

Exceptions in .NET **bubble up the call stack** until:

1. They are **caught locally** in a try-catch ✅  
2. Or they reach the **global error handling middleware** ✅  
3. Or they crash the application ❌ (if not caught at all)

### Example:
```csharp
try
{
    SomeMethod();  // throws InvalidOperationException
}
catch (ArgumentException)
{
    // Won’t handle InvalidOperationException
}
```
Since `InvalidOperationException` isn’t caught, it moves upward → middleware → handled globally.

---

## 💥 What If No One Handles the Exception?

If you **don’t catch it locally** ❌  
and you **don’t have global middleware** ❌  
then ASP.NET Core handles it as follows:

| Environment | Behavior |
|--------------|-----------|
| **Development** | Shows Developer Exception Page (detailed stack trace). |
| **Production** | Returns plain 500 Internal Server Error — no details shown. |

### Example Output (Development)
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.6.1",
  "title": "An unhandled exception occurred while processing the request.",
  "status": 500,
  "traceId": "00-5d3fda1bfb8a47438571b0e76a0f7f5f-5a452c0e59cb1f9f-00"
}
```

### Example Output (Production)
```text
HTTP Error 500 - Internal Server Error
```

No logging, no formatted response — **not suitable for real applications**.

---

## 🧾 Flow Summary Table

| Case | Local Try/Catch | Global Middleware | Result |
|------|----------------|------------------|--------|
| ✅ Handled locally | ✅ Yes | Doesn’t matter | Local response |
| ⚙️ Not handled locally but globally | ❌ No | ✅ Yes | JSON handled by middleware |
| 💀 Not handled anywhere | ❌ No | ❌ No | Dev exception page / 500 plain response |

---

## 🧠 Bonus Tip for Interviews

If asked:

> “What happens when no exception handling is configured in ASP.NET Core?”

Say:

> “If no local or global handling exists, ASP.NET Core breaks the request pipeline at the point of failure. In the Development environment, it shows the Developer Exception Page. In Production, it returns a generic 500 Internal Server Error. That’s why we always implement centralized middleware for consistent error handling and logging.”

---

## ✅ Recommended Production Setup

Combine **custom middleware** with the **built-in fallback handler** for maximum safety:

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseCustomErrorHandling();
```

---

## 🏁 Summary

| Concept | Explanation |
|----------|--------------|
| Local Handling | Handled via try-catch inside controller or service |
| Global Handling | Implemented using middleware |
| Extension Method | Keeps startup clean and reusable |
| Exception Bubbling | Uncaught exceptions move upward until handled |
| Default Framework Handling | Shows developer page (Dev) or 500 (Prod) |
| Best Practice | Always use global middleware for consistency |

---

> 🔥 **Key takeaway:**  
> Every ASP.NET Core project should include a centralized error handling middleware with logging and clean JSON responses for consistent behavior across environments.

---
