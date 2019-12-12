using System.Threading;

namespace ChromeTracing.NET.ChromeEvents
{
    public struct ChromeEventInstant : IChromeEvent
    {
        public readonly string name; // name
        public readonly long ts; // time-stamp
        public readonly char ph; // phase: I
        public readonly string pid; // Process Id
        public readonly uint tid; // Thread Id
        public readonly char s; // scope: g (global), p (process) or t (thread)

        public ChromeEventInstant(string name, string process, long ts)
        {
            this.name = name;
            this.ph = 'I';
            this.pid = process;
            this.tid = (uint) Thread.CurrentThread.ManagedThreadId;
            this.ts = ts;
            this.s = 'p';
        }
    }
}