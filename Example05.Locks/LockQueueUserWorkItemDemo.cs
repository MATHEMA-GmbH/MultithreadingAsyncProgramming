using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example05.Locks
{
    public class LockQueueUserWorkItemDemo
    {
        readonly object tLock = new object();

        public void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(Calculate, string.Format("Working ThreadPool Thread: {0}", i));
            }
        }

        public void Calculate(object objeto)
        {
            lock (tLock)
            {
                Console.Write("{0} {1} is Executing", objeto, Thread.CurrentThread.Name);
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(new Random().Next(5));
                    Console.Write(" {0},", i);
                }
                Console.WriteLine();
            }
        }
    }
}
