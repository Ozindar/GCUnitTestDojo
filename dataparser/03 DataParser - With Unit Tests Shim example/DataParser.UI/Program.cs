using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataParser.UI
{
    class Program
    {
        static void Main(string[] args)
        {

            using (StreamReader sr = new StreamReader("Data2.txt"))
            {
                Shipment shipment = new Shipment(sr);

                Console.Out.WriteLine("Done...");
                Console.ReadKey();
            }
        }
    }
}
