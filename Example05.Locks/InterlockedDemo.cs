using System;
using System.Threading;

namespace Example05.Locks
{
    public class InterlockedDemo
    {
        int sum = 0;
        public void Start()
        {
            Thread[] tr = new Thread[5];
            for (int i = 0; i < 5; i++)
            {
                tr[i] = new Thread(new ThreadStart(CalculateSum))
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

        public void CalculateSum()
        {
            try
            {
                Interlocked.Increment(ref sum);
                Console.WriteLine("Result of sum is " + sum);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
        }
    }
}
