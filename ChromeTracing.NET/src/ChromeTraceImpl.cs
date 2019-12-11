using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ChromeTracing.NET
{
    /// <summary>
    /// Actual implementation of the <see cref="ChromeTrace"/> API.
    /// </summary>
    internal class ChromeTraceImpl
    {
        private readonly List<ProfileResult> _results;


        public ChromeTraceImpl()
        {
            _results = new List<ProfileResult>();
        }

        ~ChromeTraceImpl()
        {
            Dispose();
        }
        
        
        public void AddProfileResult(ProfileResult result)
        {
            _results.Add(result);
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
                    str.Append(",");
                }
                str.Append(WriteProfile(_results[_results.Count-1]));
            }
            
            str.Append(WriteFooter());
            
            
            string filename = Path.Combine(Environment.CurrentDirectory, "trace.json");
            File.WriteAllText(filename, str.ToString());
            
            ChromeTrace.Logger.Log("ChromeTracing.NET trace file created: " + filename);
        }
        
        
        private static string WriteHeader()
        {
            return "{\"otherData\": {}, \"traceEvents\":[";
        }

        private static string WriteProfile(ProfileResult result)
        {
            ChromeTraceEvent ev = new ChromeTraceEvent()
            {
                cat = "function",
                name = result.Name,
                dur = result.End - result.Start,
                ph = 'X',
                pid = 0,
                tid = result.ThreadId,
                ts = result.Start / 1000
            };
            return JsonConvert.SerializeObject(ev);
        }
        
        private static string WriteFooter()
        {
            return "]}";
        }
        
    }
}