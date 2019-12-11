namespace ChromeTracing.NET
{
    /// <summary>
    /// A struct representing a "traceEvent", which will be
    /// listed in an array inside the final JSON trace file.
    /// </summary>
    internal struct ChromeTraceEvent
    {
        public string cat; //catagory
        public string name; // name
        public long dur; // duration
        public long ts; // time-stamp
        public char ph; // phase
        public uint pid; // Process Id
        public uint tid; // Thread Id
    }
}