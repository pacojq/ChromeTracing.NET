using System;
using System.Threading;

namespace ChromeTracing.NET.Sandbox
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ChromeTrace.Init();

            Random randOne = new Random();
            Random randTwo = new Random(randOne.Next());
            
            Thread threadOne = new Thread(() => DoTheStuff(7, randOne));
            Thread threadTwo = new Thread(() => DoTheStuff(7, randTwo));
            
            threadOne.Start();
            threadTwo.Start();

            threadOne.Join();
            threadTwo.Join();
        }

        public static void DoTheStuff(int reps, Random rand)
        {
            if (reps <= 0)
                return;
            
            using (ChromeTrace.Profile("Test " + reps % 2))
            {
                int sleep = 1000 + rand.Next(1000);
                
                Console.WriteLine("Night night!");
                
                System.Threading.Thread.Sleep(sleep);
                
                Console.WriteLine($"Woke up after {sleep} ms");
            }
            
            System.Threading.Thread.Sleep(rand.Next(100));
            DoTheStuff(reps - 1, rand);
        }
    }
}