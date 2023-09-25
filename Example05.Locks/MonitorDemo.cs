using System;
using System.Threading;

namespace Example05.Synchronization
{
    public class MonitorDemo
    {
        readonly object tLock = new object();
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
            Monitor.Enter(tLock);
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
                Monitor.Exit(tLock);
            }
        }
    }
}
