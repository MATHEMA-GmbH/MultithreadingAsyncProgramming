using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example05.Locks
{
    public class ManualResetEventDemo
    {
        static readonly ManualResetEventSlim mre = new ManualResetEventSlim(false);

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
                mre.Wait();   // Wait until it is safe to enter.  
                
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
                mre.Set();
            }
            Console.WriteLine();
        }
    }
}
