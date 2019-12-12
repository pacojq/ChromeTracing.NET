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
            Random randThree = new Random(randTwo.Next());
            
            
            Thread threadOne = new Thread(() => DoTheStuff(5, randOne, "Session One"));
            Thread threadTwo = new Thread(() => DoTheStuff(5, randTwo, "Session One"));
            Thread threadThree = new Thread(() => DoTheStuff(5, randThree, "Session Two"));

            threadOne.Start();
            threadTwo.Start();
            threadThree.Start();

            threadOne.Join();
            threadTwo.Join();
            threadThree.Join();
        }

        public static void DoTheStuff(int reps, Random rand, string session)
        {
            if (reps <= 0)
                return;
            
            using (ChromeTrace.Profile("Test " + reps % 2, session))
            {
                int sleep = 1000 + rand.Next(1000);
                
                Console.WriteLine("Night night!");
                
                System.Threading.Thread.Sleep(sleep);
                
                Console.WriteLine($"Woke up after {sleep} ms");
            }
            
            System.Threading.Thread.Sleep(rand.Next(100));
            DoTheStuff(reps - 1, rand, session);
        }
    }
}