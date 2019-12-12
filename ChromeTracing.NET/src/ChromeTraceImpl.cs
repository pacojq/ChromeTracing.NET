using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChromeTracing.NET
{
    /// <summary>
    /// Actual implementation of the <see cref="ChromeTrace"/> API.
    /// </summary>
    internal class ChromeTraceImpl
    {
        private readonly List<IChromeEvent> _results;


        public ChromeTraceImpl()
        {
            _results = new List<IChromeEvent>();
        }

        ~ChromeTraceImpl()
        {
            Dispose();
        }
        
        
        public void AddEvent(IChromeEvent ev)
        {
            _results.Add(ev);
        }
        
        

        public void Dispose()
        {
            ChromeTrace.Logger.Log("ChromeTracing.NET disposing...");
            
            StringBuilder str = new StringBuilder();

            str.Append(WriteHeader());
            
            if (_results.Count > 0)
            {
                for (int i = 0; i < _results.Count - 1; i++)
                {
                    str.Append(WriteProfile(_results[i]));
                    str.Append(",\n");
                }
                str.Append(WriteProfile(_results[_results.Count-1]));
            }
            
            str.Append(WriteFooter());
            
            
            string filename = Path.Combine(Environment.CurrentDirectory, "trace.json");
            File.WriteAllText(filename, str.ToString());
            
            ChromeTrace.Logger.Log("ChromeTracing.NET trace file created: " + filename);
        }
        
        
        
        
        
        
        
        
        // Info about the format
        // https://docs.google.com/document/d/1CvAClvFfyA5R-PhYUmn5OOQtYMH4h6I0nSsKchNAySU/preview
        
        
        private static string WriteHeader()
        {
            return "{\"otherData\": {}, \"traceEvents\":[\n";
        }

        private static string WriteProfile(IChromeEvent ev)
        {
            return ev.Serialize();
        }
        
        private static string WriteFooter()
        {
            //return "\n],\n\"displayTimeUnit\": \"ms\"}";
            return "\n]}";
        }
        
    }
}