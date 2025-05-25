
# Garbage Collection in .NET Framework – Quick Notes

## 🧠 What is Garbage Collection (GC)?

- **Garbage Collection** is a process in the .NET Framework that **automatically manages memory**.
- It is a **background process** (a small routine) that runs **periodically**.
- It identifies objects that are **no longer in use** and **reclaims their memory**.

---

## 🛠 How It Works

- When a .NET application runs, **many objects are created** using the `new` keyword.
- These objects are stored on the **managed heap**.
- The **Garbage Collector (GC)** checks the heap to find objects that are **no longer referenced** by the application.
- It then **deallocates the memory** used by those unused objects.

---

## ✅ Key Features

- **Automatic Memory Management**: You don’t need to explicitly deallocate memory (as in C/C++).
- **Manages Only Managed Objects**: 
  - GC works **only for managed objects** (those created in .NET with `new`).
  - It does **not clean unmanaged resources** (like file handles, DB connections, etc.).

---

## ❗ Important Note

- Always **release unmanaged resources** explicitly using:
  - `Dispose()` method (via `IDisposable`)
  - `using` blocks in C#

---

## Summary

> Garbage Collection in .NET is a **built-in feature** provided by the **Common Language Runtime (CLR)** to help automatically **clean unused managed objects** and **free up memory**.

# Managed and Unmanaged Objects in .NET Framework

## Managed Code and Objects

- Managed code is the code that runs under the control of the **Common Language Runtime (CLR)**.
- Examples: Applications developed using .NET supported languages like **C#, VB.NET, F#**, etc.
- CLR manages memory allocation and deallocation using the **Garbage Collector (GC)**.
- CLR also provides features like:
  - Language Interoperability
  - Automatic Memory Management
  - Exception Handling Mechanism
  - Code Access Security

### Managed Objects:
- Allocated on the **managed heap**.
- Controlled by the .NET **Garbage Collector**.
- Typically created using the **new** keyword in C# or VB.NET.
- Examples: Objects of classes, structures, arrays, strings, etc.

## Unmanaged Code and Objects

- Unmanaged code runs **outside the control of CLR**.
- Examples include **third-party EXEs** like Skype, PowerPoint, Excel, etc., developed in **C, C++, Java**, etc.
- These run under their respective **runtime environments** (e.g., C/C++ runtime, VB6 runtime).
- CLR does not manage their memory or provide .NET services.

### Unmanaged Objects:
- Memory is **not managed** by .NET GC.
- Memory must be **manually allocated and released**.
- Typically created using native code (e.g., `malloc`, `free`, `new`, `delete` in C/C++).
- Examples: File handles, database connections, COM objects, etc.

---
**Note:** While managed objects enjoy full support and safety from the CLR, unmanaged objects must be carefully handled to prevent memory leaks and ensure stability.

# Understanding Generation 0, 1, and 2 in .NET Garbage Collection

Let’s say you have a simple application called **App1**. As soon as the application starts, it creates 5 managed objects. Whenever any new objects (fresh objects) are created, they are moved into a bucket called **Generation 0**.

---

### Role of Garbage Collector

Our hero, **Mr. Garbage Collector**, runs continuously as a background process thread to check whether there are any unused managed objects so that it reclaims the memory by cleaning those objects.

- Suppose the application no longer needs **Object1** and **Object2**.
- The Garbage Collector will destroy **Object1** and **Object2** and reclaim the memory from the **Generation 0** bucket.
- The remaining objects (**Object3**, **Object4**, and **Object5**) are still in use, so they are **not cleaned**.

These objects are **promoted** to the next generation: **Generation 1**.

---

### Creating New Objects

Now, your application creates **Object6** and **Object7**.

- These are **new (fresh) objects**, so they are added to **Generation 0**.

---

### Second Garbage Collection Cycle

- The Garbage Collector runs again and checks **Generation 0**.
- Finds **Object6** and **Object7** are unused, so it destroys them and reclaims the memory.

Next, it checks **Generation 1**:

- **Object3** is unused → **destroyed**.
- **Object4** and **Object5** are still used → **moved to Generation 2**.

---

### Summary

- **Generation 0**: Newly created objects.
- **Generation 1**: Survived objects from Generation 0.
- **Generation 2**: Long-lived objects that survived multiple GC cycles.

This generational approach helps optimize the Garbage Collection process, minimizing performance impact by collecting short-lived objects more frequently.
"""

# Understanding Generations in Garbage Collection

## What are Generations?

Generations refer to the way the Garbage Collector (GC) in .NET manages the lifecycle of objects in memory. They determine **how long objects stay in memory** and how frequently the GC checks them for collection.

## Why Do We Need Generations?

In large applications, thousands of objects can be created. If the GC has to check every single object each time, it becomes a **heavy and inefficient** process. Generations optimize this by categorizing objects based on their lifetime:

- **Generation 0**: Newly created objects.
- **Generation 1**: Objects that survived one GC cycle.
- **Generation 2**: Long-lived objects.

### Benefits of Using Generations

- **Efficiency**: GC can focus more on short-lived objects (in Gen 0) and check long-lived objects (in Gen 2) less frequently.
- **Performance**: Reduces the overhead on the GC, improving the overall performance of memory management.
- **Optimal Memory Utilization**: Keeps memory clean and efficient without constant GC sweeps over long-lived objects.

> 💡 If an object has survived multiple garbage collections and moved to Generation 2, it means the object is expected to live longer and hence is scanned less frequently by the GC.

## Summary

- Generations improve the **performance** of the .NET Garbage Collector.
- The more short-lived objects are collected in **Gen 0**, the better the memory performance.
- Objects in **Gen 2** are scanned less often, assuming they're needed for a longer duration.

---

## Next Steps

To better understand how objects are managed across generations, we’ll use a tool:

### Tool: **.NET Memory Profiler**

We'll walk through how to:

1. **Download** and install .NET Memory Profiler.
2. **Create a C# Console Application**.
3. Use the profiler to **analyze object allocations** and **their respective generations**.

Stay tuned for a hands-on guide with .NET Memory Profiler!

# Destructor and Double Garbage Collector Loop in .NET

## Overview
In .NET, garbage collection is responsible for cleaning up **managed resources**. However, **unmanaged resources** (like file handles, database connections, COM objects, etc.) are not automatically handled by the Garbage Collector (GC).

To clean up unmanaged resources, the common approach is to define a method like `CleanUp()` and call it explicitly or from a destructor (`Finalize` method in C#).

---

## Destructor Behavior in .NET

- When a class defines a **destructor** (or Finalizer), the GC behaves differently during cleanup.
- Instead of immediately collecting the object, the GC:
  - Detects the destructor.
  - Promotes the object to the **next generation** (e.g., from Gen 0 to Gen 1).
  - Schedules the object for **finalization** by the Finalizer thread.
  - The actual memory is only reclaimed **after** the finalizer runs.

---

## The Problem: Double Garbage Collector Loop

- When using a destructor for cleanup:
  - The object survives **multiple GC cycles** because it's moved to higher generations.
  - The cleanup process takes **longer**, leading to **increased memory usage**.
  - More objects accumulate in **Generation 1 and Generation 2**, reducing GC efficiency.
  - This results in what's referred to as a "**double GC loop**".

---

## Example Scenario

- A legacy class in VB6 (e.g., `MyClass`) has unmanaged resources.
- A `CleanUp()` method is written to release these resources.
- The method is called from a destructor in .NET:
  ```csharp
  ~MyClass()
  {
      CleanUp();
  }
```
