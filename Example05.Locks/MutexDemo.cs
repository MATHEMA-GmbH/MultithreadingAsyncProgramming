using System;
using System.Threading;

namespace Example05.Synchronization
{
    public class MutexDemo
    {
        static readonly Mutex mutex = new Mutex();
        int sharedValue = 17;

        public void Start()
        {
            do
            {
                Console.Clear();
                Thread threadA = new Thread(DoAddition);
                Thread threadB = new Thread(DoAddition);

                threadA.Start();
                threadB.Start();

                //with Join the main thread waits to finish the execution of threadA
                //and threadB respectively to continue its own execution
                threadA.Join();
                threadB.Join();
                Console.WriteLine("Result printing in main thread -*-" + sharedValue + "-*- in Thread " + Thread.CurrentThread.ManagedThreadId);
                sharedValue = 17;
            } while (Console.ReadKey(true).KeyChar != 's');
        }

        public void DoAddition()
        {
            mutex.WaitOne(); // Wait until it is safe to enter.
            try
            {  
                int temp = sharedValue;
                temp++; // Increment the value.
                sharedValue = temp;
                Console.WriteLine("Result inside the thread -*-" + sharedValue + "-*- in Thread " + Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}