using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using DataParser.Helpers;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace DataParser.UI
{
    class Program
    {
        static void Main(string[] args)
        {

            using (StreamReader sr = new StreamReader("Data1.txt"))
            {
                try
                {
                    StaticLogger.LogInfo("Start parsing...");
                    Shipment shipment = new Shipment(sr);
                    StaticLogger.LogInfo("Ended parsing...");
                }
                catch (Exception ex)
                {
                    StaticLogger.LogError(ex.Message);
                }
                Console.Out.WriteLine("Done...");
                Console.ReadKey();
            }
        }
    }
}
