using System;
using System.Threading;

namespace Example05.Locks
{
    public class AutoResetEventDemo
    {
        static readonly AutoResetEvent autoResetEvent = new AutoResetEvent(true);
        
        public void Start()
        {
            Thread[] tr = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                tr[i] = new Thread(new ThreadStart(Calculate))
                {
                    Name = string.Format("Working Thread: {0}", i)
                };
            }
            //Start each thread  
            foreach (Thread x in tr)
            {
                x.Start();
            }
        }

        public void Calculate()
        {
            try
            {
                autoResetEvent.WaitOne();   // Wait until it is safe to enter.  
                Console.Write(" {0} is Executing", Thread.CurrentThread.Name);
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(new Random().Next(5));
                    Console.Write(" {0},", i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                autoResetEvent.Set();
            }
            Console.WriteLine();
        }
    }
}