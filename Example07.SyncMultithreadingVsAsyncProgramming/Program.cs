using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example07.SyncMultithreadingVsAsyncProgramming
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //ASYNC USING TASKS
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            await ExecuteAsyncAwaitApproach();

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            //USING CLASICAL THREADS
            SyncMultithreading syncMultithreading = new SyncMultithreading();
            syncMultithreading.ExecuteMultithreading();

            Console.ReadLine();
        }

        public static async Task ExecuteAsyncAwaitApproach()
        {
            Console.WriteLine("TASK AND ASYNC AWAIT APPROACH");
            var firstAsync = FirstAsync();
            var secondAsync = SecondAsync();
            var thirdAsync = ThirdAsync();

            await Task.WhenAll(firstAsync, secondAsync, thirdAsync);
        }

        public static async Task FirstAsync()
        {
            Console.WriteLine("First Async Method on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Before the delay a background thread (from threadpool) is executed? " + Thread.CurrentThread.IsBackground);
            await Task.Delay(1000); //simulates an IO operation allowing to run in a thread of threadpool
            Console.WriteLine("\nAfter the delay a background thread (from threadpool) is executed? " + Thread.CurrentThread.IsBackground);
            Console.WriteLine("First Async Method Continuation on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
        }

        public static async Task SecondAsync()
        {
            Console.WriteLine("Second Async Method on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);
            Console.WriteLine("Second Async Method Continuation on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
        }

        public static async Task ThirdAsync()
        {
            Console.WriteLine("Third Async Method on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(3000);
            Console.WriteLine("Third Async Method Continuation on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
        }
    }

    public class SyncMultithreading
    {
        public void ExecuteMultithreading()
        {
            Console.WriteLine("EXPLICIT THREADING APPROACH");
            Thread t1 = new Thread(new ThreadStart(FirstMethod));
            Thread t2 = new Thread(new ThreadStart(SecondMethod));
            Thread t3 = new Thread(new ThreadStart(ThirdMethod));
            t1.Start();
            t2.Start();
            t3.Start();
        }

        public void FirstMethod()
        {
            Console.WriteLine("First Method on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Before the sleep a background thread (from threadpool) is executed? " + Thread.CurrentThread.IsBackground);
            Thread.Sleep(1000);
            Console.WriteLine("\nAfter the sleep a background thread (from threadpool) is executed? " + Thread.CurrentThread.IsBackground);
            Console.WriteLine("First Method Continuation on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
        }
        public void SecondMethod()
        {
            Console.WriteLine("Second Method on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            Console.WriteLine("Second Method Continuation on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
        }
        public void ThirdMethod()
        {
            Console.WriteLine("Third Method on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            Console.WriteLine("Third Method Continuation on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}




