# Introduction

## Multitasking
- Doing multiple tasks at one time is called multitasking.
- And to do the multiple tasking OS uses processes internally.
- Multithreading in C# refers to the ability of the C# programming language and the .NET Framework to create and manage multiple threads of execution within a single process. Threads are lightweight, independent sequences of instructions that can run concurrently, allowing you to perform multiple tasks simultaneously. Multithreading is a powerful concept in C# and is used to achieve various goals, such as improving application responsiveness, parallelizing tasks, and efficiently utilizing multi-core processors.

## Process
- Process is responsible for executing any program or application for the OS.
- Each process has its memory, CPU and stack for implemetation of the program.

## Threads
- Internally process uses threads to run the application.
- Threads shares the memory and CPU of the process.
- Although threads shares the memory of its parent process but they have their own execution context.
- Threads within the same process can run concurrently, allowing for parallel execution of tasks.

## Drawbacks of Single-Threaded Applications in the .NET Framework
- Limited CPU Utilization: Single-threaded applications can only execute one task or operation simultaneously. This means they cannot fully utilize modern multi-core processors, leading to the underutilization of available CPU resources. In contrast, multi-threaded or parallel applications can distribute work across multiple threads and utilize more CPU cores efficiently.