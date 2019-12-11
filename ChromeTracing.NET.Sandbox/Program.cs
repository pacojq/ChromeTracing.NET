using System;

namespace ChromeTracing.NET.Sandbox
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ChromeTrace.Init();

            DoTheStuff(7);
        }

        public static void DoTheStuff(int reps)
        {
            if (reps <= 0)
                return;
            
            Random rand = new Random();
            
            using (ChromeTrace.Profile("Test " + reps % 2))
            {
                int sleep = 1000 + rand.Next(1000);
                
                Console.WriteLine("Night night!");
                
                System.Threading.Thread.Sleep(sleep);
                
                Console.WriteLine($"Woke up after {sleep} ms");
            }
            
            System.Threading.Thread.Sleep(rand.Next(100));
            DoTheStuff(reps - 1);
        }
    }
}