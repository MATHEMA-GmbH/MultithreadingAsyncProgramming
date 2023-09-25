using System;
using System.Threading.Tasks;

namespace Example05.Synchronization
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            DisplayMenu();
            String input = "";

            while (input != "3")
            {
                input = Console.ReadLine();

                switch (input)
                {
                    //lock
                    case "1":
                        LockDemo lockDemo = new LockDemo();
                        lockDemo.Start();
                        Console.ReadKey();
                        break;
                    //Semaphore
                    case "2":
                        SemaphoreDemo semaphoreDemo = new SemaphoreDemo();
                        semaphoreDemo.Start();
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
                DisplayMenu();
            }
            Environment.Exit(0);
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("---------Synchronization of Threads---------");
            Console.WriteLine("Available options:");
            Console.WriteLine("1- With Lock object");
            Console.WriteLine("2- With SemaphoreSlim");
            Console.WriteLine("3- Exit program");
            Console.WriteLine("Select the option number");
        }
    }
}
