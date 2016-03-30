using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataParser.Helpers;

namespace DataParser
{
    /// <summary>
    /// Representation of a Shipment. Contains 0 or multiple Packages.
    /// This class also is used to parse data from the external party which is in a specific format,
    /// and needs to be parsed so the class structure can be derived from is.
    /// </summary>
    public class Shipment
    {
        private List<Package> _packages;

        /// <summary>
        /// Gets the packages list.
        /// </summary>
        /// <value>
        /// The packages.
        /// </value>
        public List<Package> Packages
        {
            get { return _packages; }
            set { _packages = value; }
        }

        /// <summary>
        /// Gets the total price of all the Orders in the Packages of this shipment. It Also takes in account the discount.
        /// </summary>
        /// <value>
        /// The total price with discount.
        /// </value>
        public double TotalPrice { get { return Enumerable.Sum<Package>(Packages, package => package.TotalOrderPriceWithDiscount); } }

        /// <summary>
        /// Gets or sets the process date of this shipment
        /// </summary>
        /// <value>
        /// The process date.
        /// </value>
        public DateTime ProcessDate { get; set; }

        /// <summary>
        /// Gets or sets the total number of orders in this shipment
        /// </summary>
        /// <value>
        /// The total number of orders.
        /// </value>
        public int TotalNumberOfOrders { get; set; }
        /// <summary>
        /// Gets or sets the total number of orders from input (external datafile that was parsed)
        /// </summary>
        /// <value>
        /// The total number of orders from input.
        /// </value>
        public int TotalNumberOfOrdersFromInput { get; set; }
        /// <summary>
        /// Gets or sets the total price of all the orders.
        /// </summary>
        /// <value>
        /// The total price of orders.
        /// </value>
        public double TotalPriceOfOrders { get; set; }
        /// <summary>
        /// Gets or sets the total price of all the orders from input (external datafile that was parsed).
        /// </summary>
        /// <value>
        /// The total price of orders from input.
        /// </value>
        public double TotalPriceOfOrdersFromInput { get; set; }
        /// <summary>
        /// Gets or sets the total price of orders with discount.
        /// </summary>
        /// <value>
        /// The total price of orders with discount.
        /// </value>
        public double TotalPriceOfOrdersWithDiscount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shipment"/> class.
        /// </summary>
        /// <param name="streamReader">The stream reader.</param>
        public Shipment(StreamReader streamReader)
        {
            _packages = new List<Package>();
            ParseData(streamReader);
        }

        /// <summary>
        /// Parses the data.
        /// </summary>
        /// <param name="streamReader">The stream reader.</param>
        private void ParseData(StreamReader streamReader)
        {
            if (streamReader == null) { throw new ArgumentNullException("streamReader", "a streamreader should be provided"); }

            // ProcessDate:2014-01-05%TotalItems:5%TotalPrice:160.00%PackageNumber:1%Order:Food.Dogfood%Price:10.50
            string dataLine;
            do
            {
                dataLine = streamReader.ReadLine();
                if (dataLine != null)
                {
                    string[] allDataFromALine = dataLine.Split('%');

                    // Parse data
                    DateTime processDateFromDataline = DateTime.Parse(allDataFromALine.SingleOrDefault(o => o.StartsWith("ProcessDate")).Split(':')[1]);
                    TotalNumberOfOrdersFromInput = int.Parse(allDataFromALine.SingleOrDefault(o => o.StartsWith("TotalItems")).Split(':')[1]);
                    TotalPriceOfOrdersFromInput = double.Parse(allDataFromALine.SingleOrDefault(o => o.StartsWith("TotalPrice")).Split(':')[1]);
                    int packageNumberFromDataline = int.Parse(allDataFromALine.SingleOrDefault(o => o.StartsWith("PackageNumber")).Split(':')[1]);
                    string orderNameFromDataline = allDataFromALine.SingleOrDefault(o => o.StartsWith("Order")).Split(':')[1];
                    double priceNameFromDataline = double.Parse(allDataFromALine.SingleOrDefault(o => o.StartsWith("Price")).Split(':')[1]);
                  
                    // Find existing Package
                    Package package = _packages.SingleOrDefault(p => p.PackageNumber == packageNumberFromDataline);
                    if (package == null)
                    {
                        package = new Package(packageNumberFromDataline, DateTime.Now, new MyDateTime());
                        Packages.Add(package);
                    }

                    // Add the order to the package
                    Order newOrder = new Order(orderNameFromDataline, priceNameFromDataline);
                    package.Orders.Add(newOrder);

                    // Set ProcessDate
                    ProcessDate = processDateFromDataline;

                }
            } while (dataLine != null);

            // Checksum data
            foreach (var package in Packages)
            {
                TotalNumberOfOrders += package.Orders.Count;
                TotalPriceOfOrders += package.Orders.Sum(o => o.Price);
                TotalPriceOfOrdersWithDiscount += package.TotalOrderPriceWithDiscount;
            }
            if (TotalNumberOfOrders != TotalNumberOfOrdersFromInput)
            {
                Console.Out.WriteLine("Total Orders expected: {0}, actual: {1}", TotalNumberOfOrdersFromInput, TotalNumberOfOrders);
            }
            else 
            {
                Console.Out.WriteLine("Checksum total # of orders are oke.");
            }

            if (TotalPriceOfOrders != TotalPriceOfOrdersFromInput)
            {
                Console.Out.WriteLine("Total price of shipment expected: {0:C}, actual: {1:C}", TotalPriceOfOrdersFromInput, TotalPriceOfOrders);
            }
            else
            {
                Console.Out.WriteLine("Checksum total price is oke.");
            }

            Console.Out.WriteLine("Total price (with Discount): {0:C}", TotalPriceOfOrdersWithDiscount);
            Console.Out.WriteLine("Total price (without Discount): {0:C}", TotalPriceOfOrders);
        }
    }
}
