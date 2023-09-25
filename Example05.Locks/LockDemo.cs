using System;
using System.Threading;

namespace Example05.Synchronization
{
    public class LockDemo
    {
        readonly object tLock = new object();
        int sharedValue = 17;

        public void Start()
        {
            do
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
            } while (Console.ReadKey(true).KeyChar != 's');
        }

        public void DoAddition()
        {
            lock (tLock)
            {
                int temp = sharedValue;
                temp++; // Increment the value.
                sharedValue = temp;
                Console.WriteLine("Result inside child " + Thread.CurrentThread.Name + ": " + sharedValue);
            }
        }
    }
}
