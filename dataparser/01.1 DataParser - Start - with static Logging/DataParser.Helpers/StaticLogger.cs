using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace DataParser.Helpers
{
    public static class StaticLogger
    {
        private static LogWriterFactory logWriterFactory = new LogWriterFactory();
        private static LogWriter _logWriter;

        private static LogWriter LogWriter
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

        public static void LogInfo(string message)
        {
            LogWriter.Write(message, "General");
        }        
        
        public static void LogError(string message)
        {
            LogWriter.Write(message, "Error");
        }
    }
}