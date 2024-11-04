namespace AsyncProgramming
{
    public class AsyncProgramming
    {
        public static void Main(string[] args)
        {
            

            MainCode mainCode = new MainCode();
            Console.WriteLine($"Main Function is running on Thread - {Thread.CurrentThread.ManagedThreadId}");
            new TaskSchedulerClass().TaskSchedulerExample().GetAwaiter().GetResult();
            //mainCode.TaskExamples().GetAwaiter().GetResult();
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
            await Task.WhenAll(tasks).ConfigureAwait(false);

            await DoWorkAsync().ConfigureAwait(false);
        }

        public Task DoNothingAsync() // Sync function - will be running on callers thread synchronously
        {
            Console.WriteLine($"Returning a completed Task,thread - {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(4000);
            return Task.CompletedTask;
        }

        public Task<int> GetNumberAsync() // Sync function - will be running on callers thread synchronously
        {
            Console.WriteLine($"Task FromResult example, thread - {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(4000);
            return Task.FromResult(42);
        }

        public async Task DoWorkAsync()
        {
            Console.WriteLine($"Task Yield example,thread - {Thread.CurrentThread.ManagedThreadId}");
            await Task.Yield(); // Yields execution
            Thread.Sleep(4000);
            Console.WriteLine($"Task Yield example Completed,thread - {Thread.CurrentThread.ManagedThreadId}");
        }

        public async Task<int> Function()
        {
            Console.WriteLine($"F1 started TaskID- {Task.CurrentId}, ThreadId- {Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(2000).GetAwaiter().GetResult();
            Console.WriteLine($"Control returned to F1, ThreadId- {Thread.CurrentThread.ManagedThreadId}");
            int x = 2, y = 2;
            Console.WriteLine($"F1 Completed TaskID- {Task.CurrentId}, ThreadId- {Thread.CurrentThread.ManagedThreadId}");
            return x / y;
        }
    }
}