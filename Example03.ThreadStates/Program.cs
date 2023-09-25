using System;
using System.Threading;

namespace Example03.ThreadStates
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating instance for RunnableObject class
            RunnableObject runnableObject = new RunnableObject();

            object mainThread = Thread.CurrentThread;

            // Creating and initializing threads (Unstarted state)
            Thread childThread = new Thread(new ParameterizedThreadStart(runnableObject.Run));

            Console.WriteLine("Main thread " + Thread.CurrentThread.ThreadState);
            Console.WriteLine("\nChild thread " + childThread.ThreadState);

            // Child thread running state
            childThread.Start(mainThread);

            Console.WriteLine("\nMain thread state " + Thread.CurrentThread.ThreadState);
            Console.WriteLine("\nChild thread state " + childThread.ThreadState + " from main thread");

            // Simulate some work in the main thread to cause the child thread
            //to have already stopped
            Thread.Sleep(3000); //Stopped state
            Console.WriteLine("\nChild thread state " + childThread.ThreadState + " from main thread");
            Console.WriteLine("\nMain thread state " + Thread.CurrentThread.ThreadState);
            Console.ReadLine();
        }

        public class RunnableObject
        {
            public void Run(object mainThreadState)
            {
                Console.WriteLine("\nMain thread state is " + GetThreadState((Thread)mainThreadState) + " from child thread");
                Console.WriteLine("\nInside child thread state " + Thread.CurrentThread.ThreadState + "\n");
                Console.WriteLine("doing something in child thread...");
            }

            string GetThreadState(Thread thread)
            {
                return thread.ThreadState.ToString();
            }
        }
    }
}