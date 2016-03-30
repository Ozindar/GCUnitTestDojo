using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using DataParser.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DataParser.BLL.UnitTests
{
    [TestClass]
    public class ShipmentTests
    {
        [TestMethod]
        public void METHOD_STATE_OUTCOME()
        {
            // Arange
            Mock<IStreamReader> streamReaderMock = new Mock<IStreamReader>();
            streamReaderMock.Setup(s => s.ReadLine()).Returns("ProcessDate:2014-01-05%TotalItems:5%TotalPrice:160,00%PackageNumber:1%Order:Food.Dogfood%Price:10,50");

            Mock<ILogger> loggerMock = new Mock<ILogger>();

            // Act
            Shipment shipment = new Shipment(streamReaderMock.Object, loggerMock.Object);

            // Assert

        }
    }
}
