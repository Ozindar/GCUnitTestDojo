using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataParser.BLL.UnitTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void Order_ValidConstructorData_InstantiatedOrderClass()
        {
            // Arrange
            string expexctedName = "An Order";
            double expexctedPrice = 12.36;
            
            // Act
            Order order = new Order(expexctedName, expexctedPrice);

            // Assert
            Assert.AreEqual(expexctedName, order.Name);
            Assert.AreEqual(expexctedPrice, order.Price);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Order_NegativePrice_ArgumentException()
        {
            // Arrange
            string expexctedName = "An Order";
            double expexctedPrice = -12.36;
            
            // Act
            Order order = new Order(expexctedName, expexctedPrice);

            // Assert
            // ...
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Order_NullName_ArgumentNullException()
        {
            // Arrange
            string expexctedName = null;
            double expexctedPrice = 12.36;
            
            // Act
            Order order = new Order(expexctedName, expexctedPrice);

            // Assert
            // ...
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Order_EmptyName_ArgumentException()
        {
            // Arrange
            string expexctedName = "    ";
            double expexctedPrice = 12.36;
            
            // Act
            Order order = new Order(expexctedName, expexctedPrice);

            // Assert
            // ...
        }
        
        [TestMethod]
        public void Order_NameWithSpaces_TrimmedName()
        {
            // Arrange
            string expexctedName = "Order";
            double expexctedPrice = 12.36;
            
            // Act
            Order order = new Order("  Order    ", expexctedPrice);

            // Assert
            Assert.AreEqual(expexctedName, order.Name);
        }
        
        [TestMethod]
        public void Order_NameWithDots_OrderNameDividedWithGreatThanChars()
        {
            // Arrange
            string expexctedName = "Order > Name > Test";
            double expexctedPrice = 12.36;
            
            // Act
            Order order = new Order("Order.Name.Test", expexctedPrice);

            // Assert
            Assert.AreEqual(expexctedName, order.Name);
        }
    }
}
