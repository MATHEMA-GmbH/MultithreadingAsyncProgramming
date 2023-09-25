﻿using System;
using System.Threading;

namespace Example05.Synchronization
{
    public class SemaphoreDemo
    {
        readonly SemaphoreSlim semaphore = new SemaphoreSlim(2);
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
            semaphore.Wait();
            try
            {
                int temp = sharedValue;
                //Thread.Sleep(2);
                temp++; // Increment the value.
                sharedValue = temp;
                Console.WriteLine("Result inside child " + Thread.CurrentThread.Name + ": " + sharedValue);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
























    //class TheClub      // No door lists!
    //{
    //    static SemaphoreSlim _sem = new SemaphoreSlim(3);    // Capacity of 3

    //    static void Main()
    //    {
    //        for (int i = 1; i <= 5; i++) new Thread(Enter).Start(i);
    //    }

    //    static void Enter(object id)
    //    {
    //        Console.WriteLine(id + " wants to enter");
    //        _sem.Wait();
    //        Console.WriteLine(id + " is in!");           // Only three threads
    //        Thread.Sleep(1000 * (int)id);               // can be here at
    //        Console.WriteLine(id + " is leaving");       // a time.
    //        _sem.Release();
    //    }
    //}
}
