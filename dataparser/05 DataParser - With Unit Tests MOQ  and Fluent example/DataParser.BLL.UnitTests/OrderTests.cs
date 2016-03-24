using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

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
            order.Name.Should().Be(expexctedName);
            order.Price.Should().Be(expexctedPrice);
        }

        [TestMethod]
        public void Order_NegativePrice_ArgumentException()
        {
            // Arrange
            string expexctedName = "An Order";
            double expexctedPrice = -12.36;
            
            // Act
            Action act = () => new Order(expexctedName, expexctedPrice);

            // Assert
            act.ShouldThrow<ArgumentException>().WithMessage("Price must be > 0");
        }

        [TestMethod]
        public void Order_NullName_ArgumentNullException()
        {
            // Arrange
            string expexctedName = null;
            double expexctedPrice = 12.36;
            
            // Act
            Action act = () => new Order(expexctedName, expexctedPrice);

            // Assert
            act.ShouldThrow<ArgumentNullException>().WithMessage("*Provide a name.*").And.ParamName.Should().Be("value");;
        }

        [TestMethod]
        public void Order_EmptyName_ArgumentException()
        {
            // Arrange
            string expexctedName = "    ";
            double expexctedPrice = 12.36;
            
            // Act
            Action act = () => new Order(expexctedName, expexctedPrice);

            // Assert
            act.ShouldThrow<ArgumentException>().WithMessage("*Provide a valid name.*").And.ParamName.Should().Be("value");;
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
            order.Name.Should().Be(expexctedName);
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
            order.Name.Should().Be(expexctedName);
        }
    }
}
