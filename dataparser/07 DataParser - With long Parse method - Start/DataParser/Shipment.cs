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
        public double TotalPrice { get { return Packages.Sum(package => package.TotalOrderPriceWithDiscount); } }

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
        public Shipment(TextReader streamReader)
        {
            _packages = new List<Package>();
            ParseData(streamReader);
        }

        /// <summary>
        /// Parses the data.
        /// </summary>
        /// <param name="streamReader">The stream reader.</param>
        private void ParseData(TextReader streamReader)
        {
            string dataLine;
            do
            {
                // Read the line from the streamreader
                dataLine = streamReader.ReadLine();
                if (dataLine != null)
                {
                    // Split the data based on the % character
                    string[] allDataFromALine = dataLine.Split('%');

                    // Parse data: set al variables
                    DateTime processDateFromDataline = DateTime.MinValue;
                    int packageNumberFromDataline = 0;
                    string orderNameFromDataline = "";
                    double priceNameFromDataline = 0d;

                    // For each over the all the data lines
                    foreach (string data in allDataFromALine)
                    {
                        if (data.StartsWith("ProcessDate"))
                        {
                            int index = data.LastIndexOf(":");
                            index = index + 1;
                            string dataValue = data.Substring(index);
                            processDateFromDataline = DateTime.Parse(dataValue);
                        }
                        else if (data.StartsWith("TotalItems"))
                        {
                            int index = data.LastIndexOf(":");
                            index = index + 1;
                            string dataValue = data.Substring(index);
                            TotalNumberOfOrdersFromInput = int.Parse(dataValue);
                        }
                        else if (data.StartsWith("TotalPrice"))
                        {
                            int index = data.LastIndexOf(":");
                            index = index + 1;
                            string dataValue = data.Substring(index);
                            TotalPriceOfOrdersFromInput = double.Parse(dataValue);
                        }
                        else if (data.StartsWith("PackageNumber"))
                        {
                            int index = data.LastIndexOf(":");
                            index = index + 1;
                            string dataValue = data.Substring(index);
                            packageNumberFromDataline = int.Parse(dataValue);
                        }
                        else if (data.StartsWith("Order"))
                        {
                            int index = data.LastIndexOf(":");
                            index = index + 1;
                            string dataValue = data.Substring(index);
                            orderNameFromDataline = dataValue;
                        }
                        else if (data.StartsWith("Price"))
                        {
                            int index = data.LastIndexOf(":");
                            index = index + 1;
                            string dataValue = data.Substring(index);
                            priceNameFromDataline = double.Parse(dataValue);
                        }
                    }
                    
                    // Find existing Package
                    Package package = _packages.SingleOrDefault(p => p.PackageNumber == packageNumberFromDataline);
                    if (package == null)
                    {
                        // If package is null:
                        package = new Package(packageNumberFromDataline, DateTime.Now, new MyDateTime());
                        Packages.Add(package);
                    }
                    else
                    {
                        // No else case
                    }

                    // Create an order...
                    Order newOrder = new Order(orderNameFromDataline, priceNameFromDataline);
                    // ... and add it to the the package
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
