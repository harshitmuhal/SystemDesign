# Async Programming in C#

### Threads
A thread is the smallest unit of execution within a process. Each thread has its own stack and local variables.

<b>When a C# program starts, it’s a single threaded process by default. This “main” thread is responsible for executing your code line by line, creating what is known as a single threaded application.</b>

However, you can create additional threads to run tasks in parallel.

### Processes
A process is an executing program. An operating system uses processes to separate the applications that are being executed. Each thread has a scheduling priority and saves the thread context when the thread's execution is paused. The thread context includes all the information the thread needs to resume execution, including the thread's set of CPU registers and stack. Multiple threads can run in the context of a process. All threads of a process share its virtual address space.

<b>Link to Async Programming uses Threads, thread synchronization, and thread safety:
<a href="https://learn.microsoft.com/en-us/dotnet/standard/threading/threads-and-threading#processes-and-threads">Processes and Threads</a>
</b>

### Tasks
A task is a unit of work that can be executed independently of other tasks. Multiple tasks can be executed simultaneously without blocking the main thread.

#### Task vs. Thread: Key Differences

<li>Threading Model: Threads are managed by the operating system, while tasks are managed by the runtime environment.
</li>
<li>Resource Management: Threads require explicit resource management, while tasks are managed by the runtime.
</li>

### Async/Await vs Task-based programming

Task and Task`<T>` are used both with Async/Await as well as Task-based programming. Task and Task<T> represent work that’s done asynchronously and can be started, awaited etc but these don't handle thread assignment.

Tasks created and run using Task.Run or Task.Factory.StartNew are assigned work to a separate thread from thread pool.


Task.Delay(1000): Creates a task that completes after a specified time interval and runs it.

```csharp
Task task = Task.Delay(1000);
```

#### Async Keyword
Async keyword marks a function to be able to use use asynchronous programming. Just using async keyword doesn't mean that it will assign a separate thread for the function.

Example -

```csharp
public class AsyncProgramming
{
    public static void Main(string[] args)
    {
        MainCode mainCode = new MainCode();
        Console.WriteLine($"Main Function is running on Thread - {Thread.CurrentThread.ManagedThreadId}");
        mainCode.function();
        Console.WriteLine($"Main Function is completed on Thread - {Thread.CurrentThread.ManagedThreadId}");

    }
}
class MainCode
{
    public async Task function()
    {
        Console.WriteLine($"F1 started, ThreadId- {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine($"F1 Completed, ThreadId- {Thread.CurrentThread.ManagedThreadId}");
    }
}
```

```txt
Output -
Main Function is running on Thread - 1
F1 started, ThreadId- 1
F1 Completed, ThreadId- 1
Main Function is completed on Thread - 1
```

Everything ran on single thread synchronously.

#### Await Keyword
Await is the one that does the magic. For those asynchronous operations for which we do not want to block the execution thread i.e. the current thread, we can use the await operator. 

So, when we use await operator, what we are doing is, we are freeing the current thread from having to wait for the execution of the task. The control returns to the calling thread/function till the awaited task is completed on a different thread asynchronously. In this way, we are avoiding blocking the current thread that we’re using and then that thread can be used in another task.

```csharp
public class AsyncProgramming
{
    public static void Main(string[] args)
    {
        MainCode mainCode = new MainCode();
        Console.WriteLine($"Main Function is running on Thread - {Thread.CurrentThread.ManagedThreadId}");
        Task<int> task = mainCode.Function();
        Console.WriteLine($"Processing other flow, Thread - {Thread.CurrentThread.ManagedThreadId}");
        int result = task.GetAwaiter().GetResult();
        Console.WriteLine($"Main Function is completed on Thread - {Thread.CurrentThread.ManagedThreadId} with Result - {result}");
    }
}

class MainCode
{
    public async Task<int> Function()
    {
        Console.WriteLine($"F1 started TaskID- {Task.CurrentId}, ThreadId- {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(2000);
        Console.WriteLine($"Control returned to F1, ThreadId- {Thread.CurrentThread.ManagedThreadId}");
        int x = 2, y = 2;
        Console.WriteLine($"F1 Completed TaskID- {Task.CurrentId}, ThreadId- {Thread.CurrentThread.ManagedThreadId}");
        return x/y;
    }
}
```

```txt
Main Function is running on Thread - 1
F1 started TaskID- , ThreadId- 1
Processing other flow, Thread - 1
Control returned to F1, ThreadId- 9
F1 Completed TaskID- , ThreadId- 9
Main Function is completed on Thread - 1 with Result - 1
``` 

#### Blocking Wait Methods with Task
| Aspect                   | `.Wait()`                                       | `.Result`                                         | `.GetAwaiter().GetResult()`                    |
|--------------------------|-------------------------------------------------|---------------------------------------------------|------------------------------------------------|
| **Functionality**        | Blocks until the `Task` completes but does not retrieve the return value. | Blocks until the `Task` completes and retrieves the return value. | Blocks until the `Task` completes and retrieves the return value. |
| **Return Type**          | `void` (only waits; no value retrieved).        | `T` (returns the result of type `T`).             | `T` (returns the result of type `T`).           |
| **Error Handling**       | Throws an `AggregateException` if the `Task` fails. | Throws an `AggregateException` if the `Task` fails. | Throws the original exception (not wrapped in `AggregateException`). |
| **Blocking Behavior**    | Blocks synchronously on the calling thread.     | Blocks synchronously on the calling thread.       | Blocks synchronously on the calling thread.     |
| **Use Case**             | When only waiting for completion is needed without a result. | When waiting and retrieving the result is needed. | Used to retrieve the result without `AggregateException` for clearer error handling. |
| **Recommended Usage**    | Typically avoided in asynchronous contexts as it may cause deadlocks in UI/thread contexts. | Avoided in asynchronous contexts due to deadlocks and `AggregateException`. | Sometimes preferred for synchronous calls needing clearer exception handling. |
| **Deadlock Risk**        | High in single-threaded synchronization contexts (like UI threads). | High in single-threaded synchronization contexts. | High in single-threaded synchronization contexts. |


### Task-based programming

#### Different ways of creating an aysnc task using Task


```csharp
public class AsyncProgramming
{
    public static void Main(string[] args)
    {
        MainCode mainCode = new MainCode();
        Console.WriteLine($"Main Function is running on Thread - {Thread.CurrentThread.ManagedThreadId}");
        mainCode.TaskExamples().GetAwaiter().GetResult();
        Console.WriteLine($"Main Function is completed on Thread - {Thread.CurrentThread.ManagedThreadId}");
    }
}

class MainCode
{
    public async Task TaskExamples()
    {
        Console.WriteLine($"Calling function is running on Thread - {Thread.CurrentThread.ManagedThreadId}");

        Task task1 = Task.Run(() =>
        {
            Console.WriteLine($"Task.Run Example, thread - {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(4000);
        });
        
        Task task2 = Task.Factory.StartNew(() =>
        {
            Console.WriteLine($"Task.Factory.StartNew Example, thread - {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(4000);
        });
        
        Task task3 = new Task(() =>
        {
            Console.WriteLine($"Task with lambda expression Example, thread - {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(4000);
        });
        task3.Start();

        Task task4 = DoNothingAsync();

        Task task5 = GetNumberAsync();

        Task task6 = DoWorkAsync();

        Task[] tasks =
        {
            task1, task2, task3, task4, task5, task6
        };
        // Creates a task that will complete when all of the supplied tasks have completed.
        await Task.WhenAll(tasks);
    }

    public Task DoNothingAsync() // Sync function - will be running on callers thread synchronously
    {
        //Represents a task that is already completed.
        // Useful for synchronous methods that return `Task` but don’t perform asynchronous work.
        Console.WriteLine($"Returning a completed Task,thread - {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(4000);
        return Task.CompletedTask;
    }

    public Task<int> GetNumberAsync() // Sync function - will be running on callers thread synchronously
    {
        // Useful for returning a precomputed value as a task, often in test code or synchronous cases.
        Console.WriteLine($"Task FromResult example, thread - {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(4000);
        return Task.FromResult(42);
    }

    public async Task DoWorkAsync()
    {
        // Forces the async method to yield execution, allowing other tasks to run.
        // Useful in asynchronous methods to yield control back to the calling context, 
        // often to prevent blocking.
        Console.WriteLine($"Task Yield example,thread - {Thread.CurrentThread.ManagedThreadId}");
        await Task.Yield(); // Yields execution
        Thread.Sleep(4000);
        Console.WriteLine($"Task Yield example Completed,thread - {Thread.CurrentThread.ManagedThreadId}");
    }
}
```

```txt
Main Function is running on Thread - 1
Calling function is running on Thread - 1
Task.Run Example, thread - 9
Task.Factory.StartNew Example, thread - 6
Task with lambda expression Example, thread - 11
Returning a completed Task,thread - 1
Task FromResult example, thread - 1
Task Yield example,thread - 1
Task Yield example Completed,thread - 13
Main Function is completed on Thread - 1
```

**Important - Async tasks creating using Task run on a separate thread from the calling thread unlike in async function which starts running on the same thread and run the awaited operation on a different thread.**

#### ContinueWith
Creates a continuation that executes asynchronously when the target Task completes.

```csharp
Task<string> task1 = Task.Run(() =>
{
    return 12;
}).ContinueWith((antecedent) =>
{
    return $"The Square of {antecedent.Result} is: {antecedent.Result * antecedent.Result}";
});
Console.WriteLine(task1.Result);
```
```txt
Output -
The Square of 12 is : 144
```
```csharp
Task<int> task = Task.Run(() =>
{
    return 10;
});
task.ContinueWith((i) =>
{
    Console.WriteLine("TasK Canceled");oka
}, TaskContinuationOptions.OnlyOnCanceled);
task.ContinueWith((i) =>
{
    Console.WriteLine("Task Faulted");
}, TaskContinuationOptions.OnlyOnFaulted);
var completedTask = task.ContinueWith((i) =>
{
    Console.WriteLine("Task Completed");
}, TaskContinuationOptions.OnlyOnRanToCompletion);
completedTask.Wait();
```
#### Thread Pool
A thread pool is a collection of pre-initialized threads that are managed and maintained by the operating system or a threading library, which can be reused to perform multiple tasks without the overhead of creating and destroying threads for each task.

**Efficiency:** Since threads are reused, the overhead of thread creation and destruction is minimized which improves the overall performance.

**Resource Management:** Thread pools manage the number of concurrent threads to ensure that system resources are not overwhelmed. They can limit the number of threads that run concurrently based on the system's capabilities.

**Task Scheduling:** Tasks submitted to a thread pool are queued, and the pool's threads pick up tasks as they become available. This allows for efficient scheduling and execution of tasks.
   - **Behind the scenes, tasks are queued to the ThreadPool, which has been enhanced with algorithms that determine and adjust to the number of threads. These algorithms provide load balancing to maximize throughput.**
   - **Handling low-level work of queuing tasks onto threads is done by TaskSchedular.**
   - **TaskSchedular calls and uses ThreadPool.QueueUserWorkItem method in C#. ThreadPool is available is C# to override default behaviour of ThreadPool, set different min and max number of threads, check number of available threads, etc.** Link - <a href="https://medium.com/@nirajranasinghe/understanding-concurrency-in-c-with-threads-tasks-and-threadpool-4c80f6e03df9#:~:text=In%20C%23%2C%20the%20creation%20and,creation%2C%20execution%2C%20and%20completion.&text=Thread%20synchronization%20is%20a%20crucial%20element%20of%20managing%20these%20parallel%20operations%20smoothly.">ThreadPool Examples</a>
   - Default TaskScheduler can be overridden by creating a custom TaskScheduler and passing it to TaskFactory. Link - <a href="https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskscheduler?view=net-8.0">TaskScheduler</a>, Detailed - <a href="https://www.codeproject.com/Articles/5274136/Customizing-the-TaskScheduler-Queue-Your-Task-Work">Custom TaskScheduler</a>
   - Custom TaskSchedulers are most useful when you need to control concurrency, such as for rate-limiting APIs, ensuring specific task ordering, or dedicating tasks to a restricted thread pool.

**Task based programming and Async/Await uses thread pool to run tasks while when we create Threads using System.Threading it doesn't use ThreadPool. It will create threads and resource management and it's lifecycle has to be managed.**

#### ThreadPool Starvation
ThreadPool starvation occurs when all threads in the ThreadPool are occupied, and new tasks are unable to acquire a thread for execution.

#### Resolving ThreadPool Starvation Using async/await:

- Utilizing asynchronous programming with async/await can help mitigate ThreadPool starvation.
- By using async/await, threads are not blocked during asynchronous operations, allowing them to be released back to the ThreadPool.
- Use Task.Run when necessary as it always run on separate thread.

<a href="https://dotnettutorials.net/lesson/thread-pooling/">ThreadPool Class in C# and performance difference with and without threadpool</a>

#### Custom Task Scheduler for Strict Ordering
#### ConcurrentExclusiveSchedulerPair
When using the ExclusiveScheduler from a ConcurrentExclusiveSchedulerPair, all tasks will run one at a time in strict order but not necessarily on the same thread. The scheduler enforces exclusivity, meaning only one task executes at a time, but it doesn’t guarantee that the same thread will handle each task.

Here’s what happens in practice:

- Sequential Execution: Tasks are executed sequentially, as the ExclusiveScheduler only allows one task to run at any given time.
- Thread Reuse: The .NET ThreadPool typically provides the threads for these tasks. So, while only one task runs at a time, it may do so on a different ThreadPool thread for each task.

You can enforce single thread use by -
```csharp
var schedulerPair = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default, 1)
```
Example -
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    public class TaskSchedulerClass
    {
        TaskScheduler taskScheduler = new ConcurrentExclusiveSchedulerPair().ExclusiveScheduler;
        public async Task TaskSchedulerExample()
        {
            Console.WriteLine($"Starting ordered tasks. Thread - {Thread.CurrentThread.ManagedThreadId}");

            // Queue tasks in strict order
            var task1 = Task.Factory.StartNew(() => PrintTask("Task 1"), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            var task2 = Task.Factory.StartNew(() => PrintTask("Task 2"), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            var task3 = Task.Factory.StartNew(() => PrintTask("Task 3"), CancellationToken.None, TaskCreationOptions.None, taskScheduler);

            await Task.WhenAll(task1, task2, task3);

            Console.WriteLine($"All tasks completed in strict order. Thread - {Thread.CurrentThread.ManagedThreadId}");
        }

        public async Task PrintTask(string message)
        {
            // Use Simple sync code inside without using using await otherwise the behaviour is wierd (run with await to see)
            Console.WriteLine($"{message} executing on Thread {Thread.CurrentThread.ManagedThreadId}");
            DependentCall(message).Wait();
            Console.WriteLine($"{message} completed on Thread {Thread.CurrentThread.ManagedThreadId}");
        }

        public async Task DependentCall(string message)
        {
            Console.WriteLine($"{message} starting dependent tasks. Thread - {Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(2000).Wait();
            Console.WriteLine($"{message} completed dependent tasks. Thread - {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
```

```txt
Output - 
Main Function is running on Thread - 1
Starting ordered tasks. Thread - 1
Task 1 executing on Thread 9
Task 1 starting dependent tasks. Thread - 9
Task 1 completed dependent tasks. Thread - 9
Task 1 completed on Thread 9
Task 2 executing on Thread 9
Task 2 starting dependent tasks. Thread - 9
Task 2 completed dependent tasks. Thread - 9
Task 2 completed on Thread 9
Task 3 executing on Thread 9
Task 3 starting dependent tasks. Thread - 9
Task 3 completed dependent tasks. Thread - 9
Task 3 completed on Thread 9
All tasks completed in strict order. Thread - 4
```

#### SynchronizationContextTaskScheduler

- Used in UI applications (e.g., WinForms, WPF) to marshal task execution back to the UI thread. Tasks scheduled with this scheduler are run on the same thread as the UI, allowing safe interaction with UI elements.
- In a UI application, the TaskScheduler.FromCurrentSynchronizationContext() method can be used to schedule tasks to run on the UI thread

### ConfigureAwait(false)

https://devblogs.microsoft.com/dotnet/configureawait-faq/

```csharp
Task task1 = DoNothingAsync();
Task task2 = GetNumberAsync();
Task task2 = DoWorkAsync();

Task[] tasks = { task1, task2, task3 };
await Task.WhenAll(tasks).ConfigureAwait(false);
await DoWorkAsync().ConfigureAwait(false);
```