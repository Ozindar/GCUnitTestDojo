using System;
using System.Text;

namespace DataParser
{
    /// <summary>
    /// An Order Class with a Price and a Name
    /// </summary>
    public class Order
    {
        private double _price;
        private string _name;

        /// <summary>
        /// Gets or sets the Name of the Order. Name must be some valid string: no null, empty of '   '.
        /// Dots will be replaced with 'greater than' signs:
        /// 'Test.Name' --> 'Test > Name'
        /// </summary>
        /// <value>
        /// The name as a string.
        /// </value>
        /// <exception cref="System.ArgumentNullException">Provide a name.;Name</exception>
        /// <exception cref="System.ArgumentException">Provide a valid name.</exception>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Provide a name.", "Name");
                }
                if (value.Trim().Length == 0)
                {
                    throw new ArgumentException("Provide a valid name.");
                }
                _name = ParseName(value.Trim());
            }
        }

        /// <summary>
        /// Gets or sets the Price of an Order
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        /// <exception cref="System.ArgumentException">Price must be > 0</exception>
        public double Price
        {
            get { return _price; }
            set
            {
                if (_price < 0)
                {
                    throw new ArgumentException("Price must be > 0");
                }
                _price = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="price">The price.</param>
        public Order(string name, double price)
        {
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Parses the name, so "Order.Name" will be "Order > Name".
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The new (parsed) Order Name</returns>
        private string ParseName(string name)
        {
            string[] names = name.Split('.');
            StringBuilder returnName = new StringBuilder();

            foreach (string s in names)
            {
                if (returnName.Length > 0) { returnName.Append(" > "); }
                returnName.Append(s);
            }
            return returnName.ToString();
        }
    }
}