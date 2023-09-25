using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Example02.ConcurrentExecution
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Main thread started with id " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("\nCURRENT THREADS COUNT " + GetThreadsCount());
            Thread thread1 = new Thread(SomethingToDo);
            thread1.Start();
            Thread thread2 = new Thread(SomethingToDo);
            thread2.Start();
            Thread thread3 = new Thread(SomethingToDo);
            thread3.Start();

            Console.WriteLine("\nCURRENT THREADS COUNT " + GetThreadsCount());
            Console.WriteLine("\nMain thread with id " + Thread.CurrentThread.ManagedThreadId  + " continues while...");
            thread1.Join();
            thread2.Join();
            thread3.Join();
            Console.WriteLine("\nWaiting all child threads to finish from main thread");
            Console.WriteLine("\nCURRENT THREADS COUNT " + GetThreadsCount());
            Console.WriteLine("\nMain thread " + Thread.CurrentThread.ManagedThreadId + " is finishing");
            Console.ReadKey();
        }

        public static void SomethingToDo()
        {
            Console.WriteLine("\nSomething started in child thread with id " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(3000);
            Console.WriteLine("\nSomething finished in child thread with id " + Thread.CurrentThread.ManagedThreadId);
        }

        public static int GetThreadsCount()
        {
            var threadsCount = Process.GetCurrentProcess().Threads.Cast<ProcessThread>().Count(x => x.ThreadState == System.Diagnostics.ThreadState.Wait && x.WaitReason == ThreadWaitReason.ExecutionDelay || x.ThreadState == System.Diagnostics.ThreadState.Running);
            return threadsCount;
        }
    }
}
