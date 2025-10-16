# Rate Limiter

## 🧩 What is Rate Limiting?

- Rate limiting is a technique used to control how many requests a client can make to your server or API in a specific time window.

- It helps protect your application from:

- Abuse or denial-of-service (DoS) attacks 🧱

- Resource exhaustion 🧮

- Accidental overloads (e.g., due to retry loops)

- Fair usage enforcement among multiple clients ⚖️

- Starting from .NET 7, Microsoft introduced built-in Rate Limiting Middleware in Microsoft.AspNetCore.RateLimiting.

- You can configure it in your Program.cs file to automatically throttle requests.