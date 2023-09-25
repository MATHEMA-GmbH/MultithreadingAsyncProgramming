using System;
using System.Threading;
using System.Threading.Tasks;

namespace Example05.Locks
{
    public class SemaphoreSlimAsyncDemo
    {
        readonly SemaphoreSlim obj = new SemaphoreSlim(1);
        int sum = 0;

        public void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                new Task(Calculate, i).Start();
            }
        }

        public async void Calculate(object id)
        {
            Console.WriteLine(id + "-->>Wants to Get Enter");
            //Console.WriteLine("Current value before critical section " + obj.CurrentCount);
            try
            {
                await obj.WaitAsync();
                //Console.WriteLine("Current value inside critical section " + obj.CurrentCount); //free locks to aquire
                Console.WriteLine(" Success: " + id + " is in!");
                sum++;
                Console.WriteLine("sum is " + sum);
                Thread.Sleep(2000);
                Console.WriteLine(id + "<<-- is Evacuating");
            }
            finally
            {
                obj.Release();
                //Console.WriteLine("Current value after critical section" + obj.CurrentCount);
            }
        }
    }
}
