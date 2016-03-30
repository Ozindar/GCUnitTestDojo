using System;
using DataParser.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;

namespace DataParser.BLL.UnitTests
{
    [TestClass]
    public class PackageTests
    {
        [TestMethod]
        public void Package_ValidContructorData_ValidPackage()
        {
            // Arange
            DateTime expectedDateTime = DateTime.Parse("2000-01-30");
            int expectedPackageNumber = 10;
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(d => d.GetDateTime).Returns(DateTime.Now);

            // Act
            Package package = new Package(expectedPackageNumber, expectedDateTime, dateTimeMock.Object);

            // Assert
            package.PackageNumber.Should().Be(expectedPackageNumber);
            package.PackageDate.Should().Be(expectedDateTime);
            package.Orders.Should().NotBeNull().And.HaveCount(0);
            package.TotalOrderPriceWithDiscount.Should().Be(0);
            package.Discount.Should().Be(0);
        }

        [TestMethod]
        public void TotalOrderPriceWithDiscount_NoOrders_Zero()
        {
            // Arange
            DateTime expectedDateTime = DateTime.Parse("2000-01-30");
            int expectedPackageNumber = 10;
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(d => d.GetDateTime).Returns(new DateTime(2014, 10, 07));

            // Act
            Package package = new Package(expectedPackageNumber, expectedDateTime, dateTimeMock.Object);

            // Assert
            package.TotalOrderPriceWithDiscount.Should().Be(0);
        }

        [TestMethod]
        public void TotalOrderPriceWithDiscount_With2OrdersOnNormalDay_100AndNoDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(d => d.GetDateTime).Returns(new DateTime(2014, 10, 07));

            // Act
            Package package = new Package(10, DateTime.Now, dateTimeMock.Object); // Tuesday
            package.Orders.AddRange(new[] { order1, order2 });
            
            // Assert
            dateTimeMock.Object.GetDateTime.DayOfWeek.Should().Be(DayOfWeek.Tuesday);
            package.TotalOrderPriceWithDiscount.Should().Be(100);
            package.Discount.Should().Be(0);
        } 
        
        [TestMethod]
        public void TotalOrderPriceWithDiscount_OnSaturday_10PercentDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(d => d.GetDateTime).Returns(new DateTime(2014, 10, 11));

            // Act
            Package package = new Package(10, DateTime.Now, dateTimeMock.Object); // Saturday
            package.Orders.AddRange(new[] { order1, order2 });
            
            // Assert
            dateTimeMock.Object.GetDateTime.DayOfWeek.Should().Be(DayOfWeek.Saturday);
            package.TotalOrderPriceWithDiscount.Should().Be(90);
            package.Discount.Should().Be(0.1);
        }    
    
        [TestMethod]
        public void TotalOrderPriceWithDiscount_OnSundayOdd_20PercentDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(d => d.GetDateTime).Returns(new DateTime(2014, 10, 05));

            // Act
            Package package = new Package(10, DateTime.Now, dateTimeMock.Object); // Odd Sunday
            package.Orders.AddRange(new []{order1,order2});
            
            // Assert
            dateTimeMock.Object.GetDateTime.DayOfWeek.Should().Be(DayOfWeek.Sunday);
            package.TotalOrderPriceWithDiscount.Should().Be(80);
            package.Discount.Should().Be(0.2);
        }    

        [TestMethod]
        public void TotalOrderPriceWithDiscount_OnSundayEven_25PercentDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);
            Mock<IDateTime> dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(d => d.GetDateTime).Returns(new DateTime(2014, 08, 10));

            // Act
            Package package = new Package(10, DateTime.Now, dateTimeMock.Object); // Even Sunday
            package.Orders.AddRange(new []{order1,order2});
            
            // Assert
            dateTimeMock.Object.GetDateTime.DayOfWeek.Should().Be(DayOfWeek.Sunday);
            package.TotalOrderPriceWithDiscount.Should().Be(75);
            package.Discount.Should().Be(0.25);
        }
    }
}
