using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChromeTracing.NET
{
    /// <summary>
    /// <para>
    /// API that outputs a .json file to be displayed by
    /// Google Chrome's chrome://tracing tool.
    /// </para>
    ///
    /// <para>
    /// More info at https://www.chromium.org/developers/how-tos/trace-event-profiling-tool
    /// </para>
    /// </summary>
    public static class ChromeTrace
    {
        private static ChromeTraceImpl _impl;
        internal static Logger Logger { get; private set; }


        private static Stopwatch _stopwatch;

        internal static long ElapsedMillis => _stopwatch.ElapsedMilliseconds;
        
        
        
        public static void Init()
        {
            Logger = new Logger();
            
            _impl = new ChromeTraceImpl();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            
            Logger.Log("ChromeTracing.NET successfully initialized!");
        }

        /// <summary>
        /// <para>
        /// To be called when we want ChromeTrace.NET to shut down and
        /// output a profiling JSON file.
        /// </para>
        /// 
        /// <para>
        /// Can be called at the end of the application execution, even
        /// though it's not always necessary: <see cref="ChromeTrace"/> will
        /// automatically dispose with the last pass of the Garbage Collector.
        /// </para>
        /// </summary>
        public static void Dispose()
        {
            _stopwatch.Stop();
            // TODO check for running TraceSessions
            _impl.Dispose();
        }
        
        public static TraceSession Profile(string name)
        {
            return new TraceSession(name, name);
        }
        
        public static TraceSession Profile(string name, TraceSession parent)
        {
            return new TraceSession(name, parent.ProcessId);
        }

        internal static void AddProfileResult(ProfileResult result)
        {
            _impl.AddProfileResult(result);
        }
    }
}