﻿namespace ChromeTracing.NET
{
    /// <summary>
    /// Implemented by teh application.
    /// </summary>
    public interface IFileWriter
    {
        void Write(string content, string filename);
    }
}