﻿using System;
using DataParser.Helpers;
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
            Package package = new Package(expectedPackageNumber, expectedDateTime, new DateTimeMock(DateTime.Now));

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
            Package package = new Package(expectedPackageNumber, expectedDateTime, new DateTimeMock(DateTime.Now));

            // Assert
            Assert.AreEqual(0, package.TotalOrderPriceWithDiscount);
        }

        [TestMethod]
        public void TotalOrderPriceWithDiscount_With2OrdersOnNormalDay_100AndNoDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);
            Package package = new Package(10, DateTime.Now, new DateTimeMock(DateTime.Parse("2014-10-07"))); // Tuesday
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
            Package package = new Package(10, DateTime.Now, new DateTimeMock(DateTime.Parse("2014-10-11"))); // Saturday
            package.Orders.AddRange(new []{order1,order2});

            // Act
            double priceWithDiscount = package.TotalOrderPriceWithDiscount;
            double discount = package.Discount;
            
            // Assert
            Assert.AreEqual(90, priceWithDiscount);
            Assert.AreEqual(0.1, discount);
        }    
    
        [TestMethod]
        public void TotalOrderPriceWithDiscount_OnSundayOdd_20PercentDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);
            Package package = new Package(10, DateTime.Now, new DateTimeMock(DateTime.Parse("2014-10-05"))); // Odd Sunday
            package.Orders.AddRange(new []{order1,order2});

            // Act
            double priceWithDiscount = package.TotalOrderPriceWithDiscount;
            double discount = package.Discount;
            
            // Assert
            Assert.AreEqual(80, priceWithDiscount);
            Assert.AreEqual(0.2, discount);
        }    

        [TestMethod]
        public void TotalOrderPriceWithDiscount_OnSundayEven_25PercentDiscount()
        {
            // Arange
            Order order1 = new Order("Order 1", 40.00);
            Order order2 = new Order("Order 2", 60.00);
            Package package = new Package(10, DateTime.Now, new DateTimeMock(DateTime.Parse("2014-08-10"))); // Even Sunday
            package.Orders.AddRange(new []{order1,order2});

            // Act
            double priceWithDiscount = package.TotalOrderPriceWithDiscount;
            double discount = package.Discount;
            
            // Assert
            Assert.AreEqual(75, priceWithDiscount);
            Assert.AreEqual(0.25, discount);
        }

        private class DateTimeMock : IDateTime {
            public DateTime GetDateTime { get; private set; }

            public DateTimeMock(DateTime dateTime)
            {
                GetDateTime = dateTime;
            }
        }
    }
}
