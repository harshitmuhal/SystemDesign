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
