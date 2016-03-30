using System;
using System.Collections.Generic;
using System.Linq;

namespace DataParser
{
    /// <summary>
    /// A Package contains 0 or more Orders.
    /// A Discount can be calculated on the Package, based on some logic involving the current DateTime.
    /// </summary>
    public class Package
    {
        private List<Order> _orders;
        
        /// <summary>
        /// Gets or sets the package date.
        /// </summary>
        /// <value>
        /// The package date.
        /// </value>
        public DateTime PackageDate { get; set; }

        /// <summary>
        /// Gets or sets the PackageNumber.
        /// </summary>
        /// <value>
        /// The PackageNumber.
        /// </value>
        public int PackageNumber { get; set; }

        /// <summary>
        /// Gets the Discount for all the Orders, based on some logic involving the Current DateTime.
        /// </summary>
        /// <value>
        /// The calculated Discount as a double.
        /// </value>
        public double Discount { get; set; }

        /// <summary>
        /// The total price with the Discount taken in account.
        /// When day is Saturday: 10% discount
        /// When day is Sunday and odd: 20% discount
        /// When day is Sunday and even: 25% discount
        /// </summary>
        /// <value>
        /// The total price minus the Discount
        /// </value>
        public double TotalOrderPriceWithDiscount
        {
            get
            {
                var totalOrderPrice = _orders.Select(o => o.Price).Sum();
                Discount = 0;
                DateTime currentDateTime = DateTime.Now;
                if (currentDateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    Discount = 0.1; // 10% discount when processed on Saturday
                }
                if (currentDateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (currentDateTime.Day % 2 == 0)
                    {
                        Discount = 0.25; // 25% discount when processed on Sunday and an 'even' day
                    }
                    else
                    {
                        Discount = 0.2; // 20% discount when processed on Sunday and an 'odd' day
                    }
                }

                return totalOrderPrice - (totalOrderPrice * Discount);
            }
        }

        /// <summary>
        /// Gets the list of Orders.
        /// </summary>
        /// <value>
        /// The list of Orders.
        /// </value>
        public List<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Package"/> class.
        /// </summary>
        /// <param name="packageNumber">The package number.</param>
        /// <param name="packageDate">The package date.</param>
        public Package(int packageNumber, DateTime packageDate)
        {
            PackageNumber = packageNumber;
            PackageDate = packageDate;
            _orders = new List<Order>();
        }
    }
}