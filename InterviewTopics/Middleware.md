# Middleware 

## 🚦 1. What is Middleware in ASP.NET Core?

- 👉 Middleware is a piece of code that sits in the HTTP request pipeline and processes incoming requests and outgoing responses.

- Each middleware:

- Receives the HTTP request

- Does something (e.g., logging, authentication, routing)

- Then either:

- Passes the request to the next middleware in the pipeline, or

- Short-circuits the pipeline and sends the response immediately

## 🔄 2. The Request Pipeline Concept

- Imagine the ASP.NET Core app like this pipeline 👇

```csharp
Incoming Request →
   [Middleware 1] → [Middleware 2] → [Middleware 3] → Endpoint (Controller)
                                  ↑
                             Response travels back

```

- Each middleware can:

- Modify the Request

- Modify the Response

- Decide whether to pass to next middleware or not