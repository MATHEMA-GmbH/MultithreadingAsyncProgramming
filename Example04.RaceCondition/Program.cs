using System;
using System.Threading;

namespace Example04.RaceCondition
{
    public class Program
    {
        static int sharedValue = 17;

        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Thread threadA = new Thread(DoAddition) { Name = "Thread A" };
                Thread threadB = new Thread(DoAddition) { Name = "Thread B" };

                threadA.Start();
                threadB.Start();

                //with Join the main thread waits to finish the execution of threadA
                //and threadB respectively to continue its own execution
                threadA.Join();
                threadB.Join();
                Console.WriteLine("\nResult printing in main Thread: " + sharedValue);
                sharedValue = 17;
                Thread.Sleep(4000);
            }
        }

        static void DoAddition()
        {
            Thread.Sleep(100);
            int temp = sharedValue;
            temp++; // Increment the value.
            sharedValue = temp;

            Console.WriteLine("Result inside child " + Thread.CurrentThread.Name + ": " + sharedValue);
        }
    }
}