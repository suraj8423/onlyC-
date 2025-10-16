
# 🌐 ASP.NET Core Custom Middleware & HttpContext — Complete Notes

---

## 🧩 What is Middleware?

**Middleware** in ASP.NET Core is a software component that handles HTTP requests and responses.  
It sits in the **request processing pipeline** and can:
- Inspect, modify, or terminate requests and responses.
- Pass control to the next middleware in the sequence using `_next(context)`.

Middleware are executed **in the order they are registered**.

---

## ⚙️ Middleware Pipeline Flow

### How Requests Flow

```
Request → [Middleware 1] → [Middleware 2] → [Middleware 3] → Controller
                                     ↓
                             Response travels back
```

Each middleware:
- Executes logic **before** calling `_next(context)` (on the way in)
- Executes logic **after** `_next(context)` (on the way out)

---

## 🧠 Creating a Custom Middleware — Best Practice

### ✅ Step 1: Create Middleware Class

```csharp
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace YourApp.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation("Incoming Request: {method} {url} at {time}",
                    context.Request.Method,
                    context.Request.Path,
                    DateTime.UtcNow.ToString("O"));

                await _next(context); // Pass control to next middleware

                stopwatch.Stop();

                _logger.LogInformation("Completed Request: {method} {url} responded {statusCode} in {elapsed} ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Unhandled exception for {method} {url} after {elapsed} ms",
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An unexpected error occurred.");
            }
        }
    }
}
```

---

### ✅ Step 2: Create Extension Method

```csharp
using Microsoft.AspNetCore.Builder;

namespace YourApp.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
```

---

### ✅ Step 3: Register Middleware in `Program.cs`

```csharp
using YourApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

app.UseRequestLogging(); // Custom middleware registration

app.UseRouting();
app.UseAuthorization();

app.MapGet("/", () => "Hello, Middleware!");
app.Run();
```

---

## 🔁 What Happens If You Don’t Call `_next(context)`?

If you skip `await _next(context);`:
- The pipeline **stops immediately** at your middleware.
- Later middlewares and endpoints **won’t execute**.
- The response might be **empty or invalid**, unless you manually set it.

✅ You should **only skip `_next(context)` intentionally**, e.g.:
- For authentication failures.
- For rate limiting or maintenance mode responses.

Example:

```csharp
if (!context.Request.Headers.ContainsKey("X-API-KEY"))
{
    context.Response.StatusCode = 401;
    await context.Response.WriteAsync("Unauthorized");
    return; // Stop pipeline intentionally
}
```

---

## 🧩 Understanding `HttpContext`

### What Is `HttpContext`?

`HttpContext` is the **core object** that represents everything about an HTTP request and response.

ASP.NET Core creates one `HttpContext` per request and passes it through the entire pipeline.

---

### Key Properties of `HttpContext`

| Property | Description | Example |
|-----------|--------------|----------|
| `context.Request` | Represents the incoming HTTP request | `context.Request.Method`, `context.Request.Path` |
| `context.Response` | Represents the outgoing HTTP response | `context.Response.StatusCode = 200` |
| `context.User` | Represents the authenticated user | `context.User.Identity.Name` |
| `context.Items` | Key-value storage shared between middlewares | `context.Items["StartTime"] = DateTime.Now` |
| `context.Connection` | Connection info (IP, port, etc.) | `context.Connection.RemoteIpAddress` |

---

### How It Works in Middleware

When a request arrives:
1. ASP.NET Core creates an `HttpContext` object.
2. Passes it to the first middleware’s `InvokeAsync(HttpContext context)` method.
3. Each middleware:
   - Reads/modifies data from `HttpContext`.
   - Calls `_next(context)` to pass it on.
4. The same `HttpContext` flows all the way to the controller and back with the response.

---

## 🔁 Complete Request-Response Flow Diagram

```
         ┌────────────────────────────────────────────────────┐
         │                    HTTP Request                    │
         │        (Browser, Postman, Mobile App, etc.)         │
         └────────────────────────────────────────────────────┘
                               │
                               ▼
               ┌────────────────────────────────┐
               │      ASP.NET Core Server       │
               │ (Kestrel/IIS Integration Layer)│
               └────────────────────────────────┘
                               │
                               ▼
┌────────────────────────────────────────────────────────────────────────┐
│                        MIDDLEWARE PIPELINE                             │
├────────────────────────────────────────────────────────────────────────┤
│                                                                        │
│ 1️⃣  RequestLoggingMiddleware                                           │
│     ─ Logs request info before and after `_next(context)`              │
│                                                                        │
│ 2️⃣  Authentication Middleware                                          │
│     ─ Validates token/cookie and populates context.User                │
│                                                                        │
│ 3️⃣  Authorization Middleware                                           │
│     ─ Checks access permissions                                        │
│                                                                        │
│ 4️⃣  Routing Middleware                                                 │
│     ─ Routes to appropriate endpoint                                   │
│                                                                        │
│ 5️⃣  Controller / Endpoint Execution                                   │
│     ─ Executes business logic                                          │
│     ─ Writes response to context.Response                              │
│                                                                        │
│ ⬆ Response travels back up the same middlewares                        │
│   (logging, metrics, cleanup, etc.)                                    │
└────────────────────────────────────────────────────────────────────────┘
                               │
                               ▼
               ┌────────────────────────────────┐
               │       HTTP Response Sent        │
               │     (through same HttpContext)  │
               └────────────────────────────────┘
```

---

## 🧠 Key Takeaways

✅ Each HTTP request gets its **own `HttpContext`** instance.  
✅ The same object flows through all middlewares and back.  
✅ `_next(context)` passes control to the next middleware.  
✅ Middleware can:
- Inspect or modify requests/responses.
- Stop the pipeline intentionally.
✅ Best practice: Log using `ILogger`, not `Console.WriteLine`.  
✅ Use an **extension method** for clean and reusable registration.  
✅ Always handle exceptions gracefully within middleware.

---

## 💡 Common Interview Question

> 🧠 "What is the difference between `HttpContext` and `RequestDelegate` in middleware?"

**Answer:**
- `HttpContext` → Represents the current request and response.
- `RequestDelegate` → Represents the function that invokes the **next middleware** in the pipeline.

---

## 🧾 Example One-Line Definition (for Interviews)

> "A middleware in ASP.NET Core is a component that handles requests and responses in a pipeline using `HttpContext`, optionally performing actions before and after calling the next middleware using `RequestDelegate`."

---

**✅ You now fully understand how `HttpContext`, `_next(context)`, and custom middleware work together in ASP.NET Core!**
