# Parallel Programming in C# - Notes

## What is Parallel Programming?
- Parallel Programming allows dividing a task into smaller parts and executing them simultaneously.
- Improves performance by utilizing multi-threading and processor cores.

## Benefits
- **Time saving** by maximizing computer resource usage.
- Utilizes **multi-core processors** efficiently.
- Ideal for processing **large amounts of data** (e.g., filtering images, processing credit cards).

## When to Use
- Suitable for **CPU-bound operations** (e.g., arithmetic calculations, image processing).
- Not ideal for **I/O-bound operations** or **ASP.NET / ASP.NET Core**, as these are already parallelized per request.

## Considerations
- Using parallelism has overhead; it may sometimes **reduce performance**.
- Always **measure performance** to ensure benefits outweigh the costs.
- Not all tasks are worth parallelizing—small tasks may not benefit.
- Ensure the **system supports parallelism** (i.e., has multiple cores).

## Best Practices
- Do not use multiple threads for a single HTTP request.
- Use **background services** for long-running tasks in web applications.
- Modern processors often support parallelism (check Task Manager > Performance > CPU).

## Real-World Example
- Facebook handles ~250,000 photo uploads per minute using parallel processing techniques.

## Summary
- Use Parallel Programming for CPU-intensive tasks.
- Avoid it in already-parallelized environments like web request handling.
- Measure and validate performance improvements.

# 🧾 Notes: Cores vs Logical Processors

## 🔹 What are CPU Cores?
- **Cores** are the physical units inside a CPU that execute instructions.
- Each core can independently handle one task (thread) at a time.
- Modern CPUs have **multiple cores** to allow **true multitasking**.

### In Your Case:
- CPU: **Intel Core i7-13700H**
- Total **Cores**: **14**
  - **6 Performance Cores (P-cores)**: Handle demanding tasks
  - **8 Efficiency Cores (E-cores)**: Handle background/lightweight tasks

---

## 🔹 What are Logical Processors?
- **Logical Processors** (also called **threads**) are virtual cores created using **Hyper-Threading**.
- Hyper-Threading allows a single core to process **2 threads simultaneously**.
- Logical processors improve performance in multithreaded applications.

### In Your Case:
- **P-cores** (6): Support Hyper-Threading → 6 × 2 = 12 threads
- **E-cores** (8): Do **not** support Hyper-Threading → 8 × 1 = 8 threads
- **Total Logical Processors** = 12 + 8 = **20**

---

## 🔹 Comparison Table

| Term                  | Count | Description                                           |
|-----------------------|-------|-------------------------------------------------------|
| **Cores**             | 14    | Physical CPU cores (6 P-cores + 8 E-cores)            |
| **Logical Processors**| 20    | Total executable threads (12 from P-cores + 8 E-cores)|

---

## 🔹 Why It Matters
- More **cores** → Better true multitasking (e.g., running many apps at once).
- More **logical processors** → Better performance for **multithreaded** tasks like:
  - Programming/Compiling
  - Gaming
  - Video editing
  - Data processing



# Parallel Programming in C#

Parallel Programming in C# is a programming paradigm where multiple calculations or the execution of processes occur simultaneously. It is particularly useful for leveraging multi-core processors and improving application performance.

---

## Points to Remember

- **Tasks Must Be Independent**  
  Each parallel task should operate on its own data or perform its own logic without depending on the results of another parallel task.

- **Order of Execution Does Not Matter**  
  Since tasks run concurrently, you cannot assume a particular order of execution. Ensure that the logical correctness of your code does not rely on sequential execution.

- **Not Always Faster**  
  Parallelism introduces overhead (thread creation, context switching, synchronization). In some scenarios—especially with small workloads or I/O-bound operations—parallel code can be slower than a sequential approach.

---

## Types of Parallelism in C#

C# provides two main categories of parallelism:
1. **Data Parallelism**  
2. **Task Parallelism**

---

### 1. Data Parallelism

- **Definition:**  
  Apply the same operation to each element in a data collection (array, list, matrix, etc.) concurrently.  
- **Use Cases:**  
  - Filtering elements of a large array.  
  - Performing the same mathematical transformation (e.g., calculating the inverse of each matrix in a collection).  

#### Key Characteristics

- Each data element is processed independently.
- There is no shared-state mutation between iterations (or it’s minimized through proper partitioning).
- Workload is evenly distributed across threads/cores.

#### Common APIs

- **`Parallel.For`**  
  Executes a `for` loop in which iterations may run in parallel. Ideal for numerical loops or array processing.

  ```csharp
  int[] numbers = Enumerable.Range(1, 1_000_000).ToArray();
  long sum = 0;

  // Each iteration adds its index to the shared sum.
  // Note: Access to shared resources may need synchronization.
  Parallel.For(0, numbers.Length, i =>
  {
      Interlocked.Add(ref sum, numbers[i]);
  });
  ```

- **`Parallel.ForEach`**  
  Executes an action on each element of a collection in parallel. Especially useful for collections like arrays, lists, or custom partitions.

  ```csharp
  List<string> urls = new List<string> { "https://site1.com", "https://site2.com", /* ... */ };

  // Process each URL in parallel (e.g., download content).
  Parallel.ForEach(urls, url =>
  {
      string content = DownloadPageContent(url);
      ProcessContent(content);
  });
  ```

---

### 2. Task Parallelism

- **Definition:**  
  Execute a set of independent tasks concurrently. Each task can perform a different operation or logic branch.  
- **Use Cases:**  
  - Sending an email and SMS to a user at the same time (if these two operations do not depend on each other).  
  - Running multiple independent web service calls simultaneously.  

#### Key Characteristics

- Each task may execute different code.
- Tasks are typically created and managed explicitly (e.g., via `Task` objects or `Parallel.Invoke`).
- Ideal for “fire-and-forget” or independent work units that don’t share data.

#### Common API

- **`Parallel.Invoke`**  
  Executes multiple `Action` delegates in parallel. Perfect for a small, fixed set of independent tasks.

  ```csharp
  Parallel.Invoke(
      () => SendEmail(userEmail),
      () => SendSms(userPhoneNumber),
      () => LogNotification("Notification sent to user.")
  );
  ```

  In this example:
  - Emails, SMS, and logging run simultaneously.
  - Each action is independent (no shared-state concerns).

---

## When *Not* to Use Parallelism

- **Fine-Grained Operations:**  
  If each operation is very small or the iteration count is low (e.g., summing five numbers), the overhead of parallelism (thread creation, synchronization) outweighs the benefits.

- **I/O-Bound Tasks:**  
  If your tasks spend most of their time waiting for I/O (disk reads/writes, network calls), thread-based parallelism may not help. Instead, consider asynchronous programming (`async`/`await`).

- **Shared-State Contention:**  
  If tasks frequently access or modify shared data, you might incur locks or other synchronization mechanisms that degrade performance.

- **Order-Dependent Logic:**  
  If your logic relies on a strict sequence of operations (e.g., processing a queue in order), parallel execution could break correctness.

---

## Example Scenarios

1. **Computing Matrix Inverses (Data Parallelism)**  
   ```csharp
   Matrix[] matrices = LoadMatrices();
   Matrix[] inverses = new Matrix[matrices.Length];

   Parallel.For(0, matrices.Length, i =>
   {
       inverses[i] = InvertMatrix(matrices[i]);
   });
   ```

2. **Filtering a Large List (Data Parallelism)**  
   ```csharp
   List<int> numbers = Enumerable.Range(1, 1_000_000).ToList();
   ConcurrentBag<int> evenNumbers = new ConcurrentBag<int>();

   Parallel.ForEach(numbers, n =>
   {
       if (n % 2 == 0)
       {
           evenNumbers.Add(n);
       }
   });

   // 'evenNumbers' now contains all even integers up to 1,000,000.
   ```

3. **Independent Tasks: Email & SMS (Task Parallelism)**  
   ```csharp
   string userEmail = "user@example.com";
   string userPhone = "+1-555-1234";

   Parallel.Invoke(
       () => SendWelcomeEmail(userEmail),
       () => SendWelcomeSms(userPhone)
   );
   ```

---

## Summary

- **Parallel Programming in C#** enables simultaneous execution of code to utilize multiple CPU cores.
- **Key Requirements:**  
  1. Tasks must be independent.  
  2. Execution order is non-deterministic.  
- **Types of Parallelism:**  
  - **Data Parallelism**: `Parallel.For`, `Parallel.ForEach`  
  - **Task Parallelism**: `Parallel.Invoke`, or explicit `Task.Run(...)`  
- **When to Avoid:**  
  - Small or I/O-bound workloads  
  - Heavy shared-state contention  
  - Order-dependent logic  

Use parallelism judiciously—while it can significantly speed up CPU-bound operations, it can also introduce overhead and complexity if misused.  
