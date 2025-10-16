Quick summary (one liners)

Singleton (AddSingleton) — one instance for the whole application lifetime. Good for heavy, thread-safe, shared resources.

Scoped (AddScoped) — one instance per scope (in web apps: one per HTTP request). Good for per-request state (EF DbContext, per-request user context).

Transient (AddTransient) — a new instance every time it’s requested. Good for lightweight, stateless services or short-lived operation objects.


```cs
Application Start
   └─ Program.cs executes
       └─ Build Host + Configure Services
       └─ Middleware pipeline created
       └─ Singleton services initialized

↓ (app is running)

Incoming HTTP Request 1
   └─ Create Request Scope
       ├─ Scoped services created
       ├─ Transient services created on-demand
       └─ Singleton services reused
   └─ Execute middleware → controller → action
   └─ Send Response
   └─ Dispose scoped services

Incoming HTTP Request 2 (same process, new scope)

...

Application Stop
   └─ Dispose singletons
   └─ Release resources

```