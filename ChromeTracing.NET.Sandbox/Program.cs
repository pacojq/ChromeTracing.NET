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
            
            TraceSession sessionOne = ChromeTrace.Profile("Session One");
            TraceSession sessionTwo = ChromeTrace.Profile("Session Two");
            
            Thread threadOne = new Thread(() => DoTheStuff(7, randOne, sessionOne));
            Thread threadTwo = new Thread(() => DoTheStuff(7, randTwo, sessionTwo));

            threadOne.Start();
            threadTwo.Start();

            threadOne.Join();
            sessionOne.Dispose();
            
            threadTwo.Join();
            sessionTwo.Dispose();
            
        }

        public static void DoTheStuff(int reps, Random rand, TraceSession session)
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