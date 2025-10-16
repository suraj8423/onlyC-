# ASP.NET Core: IActionResult vs ActionResult<T>

---

## ⚙️ 1. Overview
In ASP.NET Core, controller actions return responses to clients. These responses can be:
- Data (JSON, objects, etc.)
- HTTP responses (200 OK, 404 NotFound, 400 BadRequest, etc.)

To handle these, ASP.NET Core provides two main return types:
- `IActionResult`
- `ActionResult<T>`

---

## 🧩 2. What is `IActionResult`?

`IActionResult` is an **interface** representing an HTTP response result.
It allows returning **different HTTP response types**, such as:

```csharp
public IActionResult GetUser(int id)
{
    var user = _userService.GetById(id);
    if (user == null)
        return NotFound(); // 404

    return Ok(user); // 200 with JSON data
}
```

✅ **Use when:** The method may return multiple HTTP responses but does not always return a fixed data type.

---

## 🧱 3. What is `ActionResult<T>`?

`ActionResult<T>` is a **generic type** introduced in ASP.NET Core 2.1. It combines:
- The flexibility of `IActionResult` (for HTTP responses)
- The convenience of returning a **strongly typed object** (`T`)

```csharp
public ActionResult<User> GetUser(int id)
{
    var user = _userService.GetById(id);
    if (user == null)
        return NotFound(); // 404

    return user; // Automatically wrapped as Ok(user)
}
```

✅ **Use when:** You want to return data and possibly error responses while keeping strong typing.

---

## 🧠 4. Key Differences

| Feature | `IActionResult` | `ActionResult<T>` |
|----------|------------------|-------------------|
| Type | Interface | Generic class |
| Introduced | ASP.NET Core 1.0 | ASP.NET Core 2.1 |
| Can return HTTP results (`Ok()`, `NotFound()`) | ✅ Yes | ✅ Yes |
| Can return strongly typed data | ❌ No | ✅ Yes |
| Best for | Generic responses | Data + status responses |
| Swagger/OpenAPI support | ❌ Limited | ✅ Strongly typed schema |

---

## 🧰 5. When to Use Which

### ✅ Use `IActionResult` when:
- The endpoint only returns HTTP status results.
- Example: `return Ok()`, `return NotFound()`

### ✅ Use `ActionResult<T>` when:
- The endpoint returns a **data model** most of the time.
- You want **type safety** and better **Swagger documentation**.

---

## 💡 6. Example Comparison

```csharp
// Using IActionResult
[HttpGet("{id}")]
public IActionResult GetProduct(int id)
{
    var product = _repo.Get(id);
    if (product == null)
        return NotFound();

    return Ok(product);
}

// Using ActionResult<T>
[HttpGet("{id}")]
public ActionResult<Product> GetProductV2(int id)
{
    var product = _repo.Get(id);
    if (product == null)
        return NotFound();

    return product; // Automatically Ok(product)
}
```

Both produce the same result at runtime, but `ActionResult<T>` provides better clarity and tooling support.

---

## 🧠 7. Interview Tip

> "I prefer using `ActionResult<T>` in Web APIs because it provides type safety, better Swagger documentation, and flexibility to return both data and HTTP responses. If an endpoint only returns status codes, I use `IActionResult`."

---

## 🧾 8. Summary Table

| Concept | Description |
|----------|--------------|
| `IActionResult` | Interface representing any HTTP response |
| `ActionResult<T>` | Combines HTTP results with a strongly typed response |
| Use Case | Web APIs returning both data and status codes |
| Recommended | ✅ Prefer `ActionResult<T>` for typed Web APIs |

---

### ✅ In short:
> **`IActionResult`** → For flexible, generic responses.  
> **`ActionResult<T>`** → For strongly typed, data-returning APIs with clean, modern structure.

---