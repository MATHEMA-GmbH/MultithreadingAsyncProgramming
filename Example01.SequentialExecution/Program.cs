using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Multithreading
{
    static class Program
    {
        static void Main(string[] args)
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            Console.WriteLine("Process name:\t" + currentProcess.ProcessName);
            Console.WriteLine("\nCurrent threads count: " + GetThreadsCount());
            Console.ReadLine();
        }

        public static int GetThreadsCount()
        {
            var threadsCount = Process.GetCurrentProcess().Threads.Cast<ProcessThread>().Count(x => x.ThreadState == ThreadState.Wait && x.WaitReason == ThreadWaitReason.ExecutionDelay || x.ThreadState == ThreadState.Running);
            return threadsCount;
        }

    }
}
