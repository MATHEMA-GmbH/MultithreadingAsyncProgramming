using System;
using System.Threading;

namespace Example06.BackgroundVsForegroundThreads
{
    internal class Program
    {
        static void Main()
        {
            //FOREGROUND THREADS CREATION
            XgroundTest shortTest = new XgroundTest(10);
            Thread foregroundThread =
                new Thread(new ThreadStart(shortTest.RunLoop));

            //BACKGROUND THREADS CREATION
            XgroundTest longTest = new XgroundTest(30);
            Thread backgroundThread =
                new Thread(new ThreadStart(longTest.RunLoop))
                {
                    IsBackground = true
                };
            
            foregroundThread.Start();
            backgroundThread.Start();
            
            #region 
            Console.WriteLine("Main thread is background? " + Thread.CurrentThread.IsBackground);
            //Because a main thread is also a foreground thread
            //Console.ReadLine();
            #endregion
        }
    }

    class XgroundTest
    {
        readonly int maxIterations;

        public XgroundTest(int maxIterations)
        {
            this.maxIterations = maxIterations;
        }

        public void RunLoop()
        {
            for (int i = 0; i < maxIterations; i++)
            {
                Console.WriteLine("{0} count: {1}",
                    Thread.CurrentThread.IsBackground ?
                       "-BACKground- Thread" : "+FOREground+ Thread", i);
                Thread.Sleep(550);
            }
            Console.WriteLine("{0} finished counting.",
                              Thread.CurrentThread.IsBackground ?
                              "-----BACKground Thread-----" : "+++++FOREground Thread+++++");
        }
    }
}
