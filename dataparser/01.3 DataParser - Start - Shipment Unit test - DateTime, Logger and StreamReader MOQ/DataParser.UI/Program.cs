using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using DataParser.Helpers;
using DataParser.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;

namespace DataParser.UI
{
    class Program
    {
        private static IUnityContainer _container;

        public static ILogger Logger { get; set; }

        static void Main(string[] args)
        {
            IoCSetup();
            StartParsing();
        }

        private static void StartParsing()
        {
            Test test = new Test();
//            Test test = new Test(_container.Resolve<ILogger>());
            test.StartParsing();
            Console.Out.WriteLine("Done...");
            Console.ReadKey();
        }

        private static void IoCSetup()
        {
            _container = new UnityContainer();

            _container.RegisterType<ILogger, StaticLogger>();
            _container.RegisterType<IDateTime, MyDateTime>();
        }
    }

    public class Test
    {
        public Test() { }        

        [Dependency]
        public ILogger Logger { get; set; }

        public void StartParsing()
        {
            using (MyStreamReader sr = new MyStreamReader("Data1.txt"))
            {
                try
                {
                    Logger.LogInfo("Start parsing...");
                    Shipment shipment = new Shipment(sr, Logger);
                    Logger.LogInfo("Ended parsing...");
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                }
            }
        }
    }
}
