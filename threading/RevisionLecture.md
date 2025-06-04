# Multithreading in C#

Multithreading in C# refers to the capability to create and manage multiple threads within a single process. A thread is the smallest unit of execution within a process, and multiple threads can run concurrently, sharing the same resources of the parent process but executing different code paths.

---

## 🧠 Basics

- Every C# application starts with a **single thread**, known as the **main thread**.
- The .NET framework provides classes and methods to create and manage **additional threads**.

---

## 🧰 Core Classes & Namespaces

- **Namespace:** `System.Threading`
- **Key Classes:**
  - `Thread`: Represents a single thread. Provides methods/properties to control and query its state.
  - `ThreadPool`: Manages a pool of worker threads for tasks, async I/O, etc.

---

## ✅ Advantages

- **Improved Responsiveness**: 
  - Useful in GUI apps to offload long-running tasks and keep UI responsive.
- **Better Resource Utilization**: 
  - Efficient CPU use, especially on multi-core systems.

---

## ⚠️ Challenges

- **Race Conditions**: 
  - When multiple threads access and modify shared data simultaneously.
- **Deadlocks**: 
  - Occur when threads wait indefinitely for each other to release resources.
- **Resource Starvation**: 
  - A thread is repeatedly denied access to needed resources.

### 🛠 To handle these:
- Use **synchronization primitives** like:
  - `lock`
  - `Mutex`
  - `Monitor`
  - `Semaphore`

---

## 📝 Considerations

- Avoid creating too many threads — it can **degrade performance** due to context-switching.
- Threads consume system resources.
- Synchronization introduces overhead — balance is essential.

---

> Multithreading is powerful but must be used with caution to avoid performance and stability issues.
