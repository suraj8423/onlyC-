# Introduction to Concurrency

## How Can We Achieve Efficiency?

One way to achieve efficiency is by using a faster computer — one with more RAM, a better CPU, faster SSDs, etc.  

However, this approach can be expensive and has physical limitations in terms of how much RAM, CPU power, or SSD speed can be added.

Another, more scalable option is to **take advantage of the multiple cores** available in modern processors. This allows us to run tasks concurrently, improving performance without relying solely on hardware upgrades.

## What is Concurrency?

- **Concurrency** means doing multiple tasks at the same time, instead of sequentially.
- It helps reduce total execution time by overlapping task execution.
  
### Real-Life Analogy:
- A restaurant with one cook = all orders handled one-by-one → longer wait time.
- Adding another cook = tasks are split and handled **simultaneously** → reduced wait time.

### Key Concept:
- **Parallelism** is a way to implement concurrency by dividing tasks across multiple workers (e.g., cooks).
- Example: One cook handles veg, another handles non-veg — both work in parallel.


## How Can We Achieve Parallelism in Programming?

- **Parallelism** is achieved using **threads** — independent sequences of execution.
- When multiple threads run at the same time, it's called **multithreading**.
- **Multithreading** is a form of **concurrency** and is used to perform multiple tasks simultaneously.

### Asynchronous Programming

- **Concurrency** isn't just about speed — it's also about **efficient resource usage**.
- In a web server, releasing threads when not in use improves efficiency.
- **Asynchronous programming** allows threads to avoid unnecessary blocking.

### Real-Life Analogy:
- Ordering a pizza and doing other tasks while waiting = async behavior.
- Instead of freezing (blocking), the thread continues with other tasks while awaiting results.

### Summary:
- **Multithreading** = parallel execution using multiple threads.
- **Async programming** = efficient thread usage, avoids blocking, serves more concurrent tasks (e.g., HTTP requests).


# Introduction to Parallel Programming

Parallel Programming helps in dividing a task into smaller parts and executing those parts simultaneously. 

### 📷 Example Use Case:
If we have a set of images and want to apply filters to each one, we can apply parallelism to speed up processing by using multiple threads.

---

## 🚀 Benefits of Parallel Programming

- **Time Efficiency**: Saves time by utilizing computer resources efficiently.
- **Maximizes CPU Usage**: Makes use of multi-threading to execute multiple operations concurrently.
- **High-Volume Data Handling**: Ideal for systems like Facebook, where massive data (e.g., 250,000 photo uploads per minute) is processed.

---

## 🛠 Parallel Programming in C#

C# provides two main ways to implement parallelism:

1. **Task Parallel Library (TPL)**  
   - Abstracts thread management and simplifies parallel code.
   - Developers can focus on logic instead of low-level threading.
   - Automatically manages thread creation and task execution.

2. **Parallel LINQ (PLINQ)**  
   - A parallel implementation of LINQ.
   - Allows filtering and transforming data collections in parallel.
   - Utilizes multiple cores to evaluate elements concurrently.

---

## 🔄 Types of Parallelism

### 1. Data Parallelism
- **Definition**: Same operation on multiple elements of a collection.
- **Example**: Filtering an array in parallel.

### 2. Task Parallelism
- **Definition**: Multiple independent tasks are executed simultaneously.
- **Example**: Sending an email and an SMS at the same time.

---

## ⚠️ When *Not* to Use Parallelism

Just because parallelism is available doesn't mean it should be used in every case.  
Sometimes, parallel execution introduces overhead and results in **slower performance** than sequential execution.

---

## 🧠 Summary

Parallel Programming:
- Improves performance by executing multiple tasks concurrently.
- Offers abstractions through TPL and PLINQ in C#.
- Supports **data** and **task** parallelism.
- Must be used **judiciously**, depending on the scenario.

# CPU vs I/O Bound Operations

Understanding the difference between CPU-bound and I/O-bound operations is essential when deciding whether to use **Asynchronous** or **Parallel Programming**.

- The method which is marked with the async keyword must return a Task or Task<T>. The idea of a Task is that it represents an asynchronous operation and does not return anything. In the case of Task<T>, it is like a promise that in the future this method will return a value of the data type T. 

---

## 🧵 Asynchronous Programming: Best for I/O-Bound Operations

### What are I/O-Bound Operations?
These are operations that involve **communication with external systems**. The performance bottleneck is not the CPU but the **waiting time** for external resources.

### 🔄 Examples:
- Calling a Web Service
- Reading/writing from a Database
- Accessing the File System

### 🔧 Why Use Async?
When making external calls, the system **waits** for a response. During this wait, asynchronous programming helps by **freeing up the thread** to perform other tasks, improving **scalability and responsiveness**.

---

## 🖥️ Parallel Programming: Best for CPU-Bound Operations

### What are CPU-Bound Operations?
These are tasks that are **heavily dependent on the processor**. They do not involve external system interaction — everything runs **locally** within the application.

### 🔄 Examples:
- Sorting large arrays
- Performing matrix operations
- Complex mathematical computations

### 🔧 Why Use Parallelism?
When multiple independent CPU-heavy operations are needed, parallel programming helps by **dividing the workload across multiple threads/cores**, reducing overall processing time.

---

## 🧠 Quick Decision Guide

| Operation Type       | Characteristics                                           | Recommended Approach       |
|----------------------|-----------------------------------------------------------|-----------------------------|
| **I/O-Bound**        | Waits for external response (e.g., DB, API, File I/O)     | Asynchronous Programming    |
| **CPU-Bound**        | Heavy computations within your system                     | Parallel Programming        |

---

## ✅ Summary

- Use **Async Programming** for I/O-bound tasks to avoid blocking threads while waiting for external responses.
- Use **Parallel Programming** for CPU-bound tasks to split computation across multiple cores.
- Choosing the right model can drastically improve the **performance** and **scalability** of your applications.

# 🧵 Sequential Programming, Concurrency, Multithreading, Parallelism & Multitasking

Understanding these concepts is key to building efficient and responsive applications. While these terms are often used interchangeably, each represents a distinct programming model.

---

## 🟠 Sequential Programming

- **Definition**: Executes instructions **one at a time** in order.
- **Pros**: Simple and easy to understand.
- **Cons**: Can be **slow**, especially when tasks can be run concurrently.

---

## 🔄 Concurrency

- **Definition**: The concept of **doing several things at once**, but **not necessarily simultaneously**.
- **Key Idea**: Interleaving tasks. Multiple operations make progress over time, not all at once.
- **Example**: Switching between threads rapidly.

---

## 🧵 Multithreading

- **Definition**: Ability to use **multiple threads** within a single program.
- **Important Note**: 
  - Multithreading **≠ Parallelism**.
  - Even single-core systems can use multithreading via **time-slicing**.
- **Goal**: Improve responsiveness and resource utilization.

---

## ⚡ Parallelism

- **Definition**: **Truly simultaneous execution** of threads on **multiple CPU cores**.
- **Requirement**: **Multicore processors**.
- **Example**: One thread sorts a list while another calculates a matrix inverse — both happen at the same time.

---

## 🔄 Multitasking

- **Definition**: Running **multiple programs or tasks**, where their **execution is interleaved**.
- **Handled By**: Operating System.
- **Example**: 
  - Program A → Thread 1 and 2  
  - Program B → Thread 3 and 4  
  - Executed in order: **Thread 1 → Thread 3 → Thread 2 → Thread 4**
- **Note**: Appears concurrent to the user, but may run **sequentially** behind the scenes.

---

## 📊 Visual Representation

![Thread Execution Illustration](30bbf9aa-fe6a-4816-9981-5f8839dd87d8.png)

### Breakdown:
- **Top Left**: Program A with Thread 1 and Thread 2
- **Top Right**: Program B with Thread 3 and Thread 4
- **Bottom**: Threads executed one after another in sequence — multitasking without real parallelism

---

## 💡 Summary Table

| Concept         | Description                                           | Requires Multicore | Executes Simultaneously |
|------------------|-------------------------------------------------------|---------------------|--------------------------|
| Sequential       | One task at a time                                     | ❌                  | ❌                       |
| Concurrency      | Multiple tasks make progress together                  | ❌                  | ❌                       |
| Multithreading   | Multiple threads in one program                        | ❌                  | ❌                       |
| Parallelism      | Threads run at the same time on multiple cores        | ✅                  | ✅                       |
| Multitasking     | OS-managed switching between programs/tasks           | ❌                  | ❌ (but feels like ✅)    |

---

## ✅ Takeaway

Use the right model depending on your use case:
- Use **Multithreading** for responsiveness.
- Use **Parallelism** for performance (CPU-bound tasks).
- Use **Async/Concurrency** for I/O-bound operations.


# Determinism vs Non-Determinism

## Determinism

- **Definition**: A method is deterministic if we can predict its result from its input values. 
- **Example**: Consider a method that takes two integers as input and returns their sum.
  
  - If the method takes 2 and 5 as inputs, the result will always be 7 (2 + 5 = 7).
  - The output is predictable based on the input values.

## Non-Determinism

- **Definition**: A method is non-deterministic if we cannot predict its result based on its input values.
- **Example 1: Random Class**
  
  - The `Random` class generates pseudo-random numbers, where the result is not predictable from the input values. 
  - The outcome of the random number generation cannot be determined beforehand.
  
- **Example 2: Parallelism**
  
  - With sequential programming, we can predict the order of operations, such as writing messages to a console.
  - In parallel programming, multiple operations are executed concurrently, and the order of execution is unpredictable.
  - For example, if credit card processing operations are parallelized and messages are written to the console as they process, we cannot predict the order of these messages.
  - Parallelism introduces non-determinism because the execution order of threads is not guaranteed.

## Key Takeaways

- **Deterministic Methods**: The output can be predicted from the input values.
- **Non-Deterministic Methods**: The output cannot be predicted, even with knowledge of the input values. This includes examples like the `Random` class and parallel programming.
- **Parallelism**: While parallel programming can increase performance, it introduces non-determinism, particularly in terms of the order of execution.
- **Caution with Parallelism**: If a specific order of tasks is important, parallelism may not be the best approach.


## 📌 Why CPU-bound processes should not use async/await
- async/await is best for I/O-bound operations (like API calls or file reads) where the thread can wait without doing work.

- CPU-bound tasks (like calculations, image processing, etc.) are computation-heavy and need the CPU continuously.

- Using async/await on CPU-bound tasks doesn't free up the thread — it just runs the heavy work on a background thread, adding unnecessary overhead.

- Instead, use Parallel.For, PLINQ, or Task.Run to take advantage of multiple CPU cores and truly parallelize CPU-bound work.

- ✅ Use async/await to improve scalability for I/O tasks.
- ✅ Use Parallel or Task.Run to improve performance for CPU tasks.
