using System;
using System.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            // Act
            Package package = new Package(expectedPackageNumber, expectedDateTime);

            // Assert
            Assert.AreEqual(expectedPackageNumber, package.PackageNumber);
            Assert.AreEqual(expectedDateTime, package.PackageDate);
            Assert.AreEqual(0, package.Orders.Count);
            Assert.AreEqual(0, package.TotalOrderPriceWithDiscount);
            Assert.AreEqual(0, package.Discount);
        }

        [TestMethod]
        public void TotalOrderPriceWithDiscount_NoOrders_Zero()
        {
            // Arange
            DateTime expectedDateTime = DateTime.Parse("2000-01-30");
            int expectedPackageNumber = 10;

            // Act
            Package package = new Package(expectedPackageNumber, expectedDateTime);

            // Assert
            Assert.AreEqual(0, package.TotalOrderPriceWithDiscount);
        }

        [TestMethod]
        public void TotalOrderPriceWithDiscount_With2OrdersOnNormalDay_100AndNoDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);
            Package package = new Package(10, DateTime.Now); // Tuesday
            package.Orders.AddRange(new []{order1,order2});

            // Act
            double priceWithDiscount = package.TotalOrderPriceWithDiscount;
            double discount = package.Discount;
            
            // Assert
            Assert.AreEqual(100, priceWithDiscount);
            Assert.AreEqual(0, discount);
        } 
        
        [TestMethod]
        public void TotalOrderPriceWithDiscount_OnSaturday_10PercentDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);

            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2014, 10, 11); // Saturday

                Package package = new Package(10, DateTime.Now); // Using Shim --> Saturday October 11th
                package.Orders.AddRange(new[] {order1, order2});

                // Act
                double priceWithDiscount = package.TotalOrderPriceWithDiscount;
                double discount = package.Discount;

                // Assert
                Assert.AreEqual(90, priceWithDiscount);
                Assert.AreEqual(0.1, discount);
            }
        }    
    
        [TestMethod]
        public void TotalOrderPriceWithDiscount_OnSundayOdd_20PercentDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);

            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2014, 10, 19); // Odd Sunday

                Package package = new Package(10, DateTime.Now); 
                package.Orders.AddRange(new[] {order1, order2});

                // Act
                double priceWithDiscount = package.TotalOrderPriceWithDiscount;
                double discount = package.Discount;

                // Assert
                Assert.AreEqual(80, priceWithDiscount);
                Assert.AreEqual(0.2, discount);
            }
        }    

        [TestMethod]
        public void TotalOrderPriceWithDiscount_OnSundayEven_25PercentDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);

            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => new DateTime(2014, 10, 12); // Even Sunday

                Package package = new Package(10, DateTime.Now); // Even Sunday
                package.Orders.AddRange(new[] {order1, order2});

                // Act
                double priceWithDiscount = package.TotalOrderPriceWithDiscount;
                double discount = package.Discount;

                // Assert
                Assert.AreEqual(75, priceWithDiscount);
                Assert.AreEqual(0.25, discount);
            }
        }
    }
}
