using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace DataParser.BLL.UnitTests
{
    [TestClass]
    public class ShipmentTests
    {
        [TestMethod]
        public void Shipment_WithNull_ArgumentNullException()
        {
            // Arange
            Action act = () => new Shipment(null);

            // Act
            // ..

            // Assert
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}
