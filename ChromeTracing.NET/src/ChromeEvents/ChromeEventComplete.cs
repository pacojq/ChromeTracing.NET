using System.Threading;

namespace ChromeTracing.NET.ChromeEvents
{
    internal struct ChromeEventComplete : IChromeEvent
    {
        public readonly string cat; //catagory
        public readonly string name; // name
        public readonly long dur; // duration
        public readonly long ts; // time-stamp
        public readonly char ph; // phase: X
        public readonly string pid; // Process Id
        public readonly uint tid; // Thread Id

        public ChromeEventComplete(string name, string process, long start, long end)
        {
            this.cat = "function";
            this.name = name;
            this.dur = end - start;
            this.ph = 'X';
            this.pid = process;
            this.tid = (uint) Thread.CurrentThread.ManagedThreadId;
            this.ts = start;
        }
    }
}