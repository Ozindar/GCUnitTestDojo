using System;
using System.IO;

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
