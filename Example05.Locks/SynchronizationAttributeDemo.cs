using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace Example05.Synchronization
{
    [Synchronization]
    public class SynchronizationAttributeDemo : ContextBoundObject
    {
        int sharedValue = 17;

        public void Start()
        {

            Console.Clear();
            Thread[] tr = new Thread[2];
            for (int i = 0; i < 2; i++)
            {
                tr[i] = new Thread(new ThreadStart(DoAddition))
                {
                    Name = string.Format("Working Thread: {0}", i)
                };
            }
            //Start each thread  
            foreach (Thread x in tr)
            {
                x.Start();
                Thread.Sleep(100);
            }
            
            Console.WriteLine("Result printing in main thread -*-" + sharedValue + "-*- in Thread " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
            Start();

        }

        public void DoAddition()
        {
            int temp = sharedValue;
            temp++; // Increment the value.
            sharedValue = temp;
            Console.WriteLine("Result inside the thread -*-" + sharedValue + "-*- in Thread " + Thread.CurrentThread.ManagedThreadId);
            
        }
    }
}
