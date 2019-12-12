using System.Threading;

namespace ChromeTracing.NET.ChromeEvents
{
    internal struct ChromeEventDuration : IChromeEvent
    {
        public readonly string cat; //catagory
        public readonly string name; // name
        public readonly long ts; // time-stamp
        public readonly char ph; // phase: B (begin) or E (end)
        public readonly string pid; // Process Id
        public readonly uint tid; // Thread Id

        public ChromeEventDuration(string name, string process, long ts, char ph)
        {
            this.cat = "function";
            this.name = name;
            this.ph = ph;
            this.pid = process;
            this.tid = (uint) Thread.CurrentThread.ManagedThreadId;
            this.ts = ts;
        }
    }
}