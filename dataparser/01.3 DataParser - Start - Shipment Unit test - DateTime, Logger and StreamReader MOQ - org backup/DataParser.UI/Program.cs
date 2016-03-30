using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using DataParser.Helpers;
using DataParser.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace DataParser.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new StaticLogger();
            using (MyStreamReader sr = new MyStreamReader("Data1.txt"))
            {
                try
                {
                    logger.LogInfo("Start parsing...");
                    Shipment shipment = new Shipment(sr, logger);
                    logger.LogInfo("Ended parsing...");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
                Console.Out.WriteLine("Done...");
                Console.ReadKey();
            }
        }
    }
}
