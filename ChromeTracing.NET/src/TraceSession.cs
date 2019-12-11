using System;

namespace ChromeTracing.NET
{
    /// <summary>
    /// Represents a procedure or session which will be profiled from start to finish.
    /// </summary>
    public class TraceSession : IDisposable
    {
        // TODO implement interface to allow profiling single-tick events and nested sessions


        private readonly string _name;
        private readonly long _start;
        private bool _stopped;
        
        internal TraceSession(string name)
        {
            _name = name;
            _start = ChromeTrace.ElapsedMillis;
            _stopped = false;
        }

        /// <summary>
        /// To be called when we the profiling session has ended.
        /// </summary>
        public void Dispose()
        {
            
            ChromeTrace.Logger.Log("Session disposing");
            
            if (!_stopped)
                Stop();
        }
        
        private void Stop()
        {
            if (_stopped)
                return;

            _stopped = true;
            long end =  ChromeTrace.ElapsedMillis;

            ProfileResult result = new ProfileResult()
            {
                Name = _name,
                Start = _start * 1000,
                End = end * 1000,
                ThreadId = 0
            };

            ChromeTrace.AddProfileResult(result);
        }
    }
}