using DataParser.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace DataParser.Helpers
{
    public class StaticLogger : ILogger
    {
        private LogWriterFactory logWriterFactory = new LogWriterFactory();
        private LogWriter _logWriter;

        private LogWriter LogWriter
        {
            get
            {
                if (_logWriter == null)
                {
                    _logWriter = logWriterFactory.Create();
                }
                return _logWriter;
            }
        }

        public void LogInfo(string message)
        {
            LogWriter.Write(message, "General");
        }        
        
        public void LogError(string message)
        {
            LogWriter.Write(message, "Error");
        }
    }
}