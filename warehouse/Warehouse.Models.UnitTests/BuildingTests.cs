using System;
using Warehouse.Models.Enums;
using Warehouse.Models.Exceptions;
using Warehouse.Models.UnitTests.Mocks;
using Xunit;

namespace Warehouse.Models.UnitTests
{
    public class BuildingTests
    {
        [Fact]
        public void Constructor_NewBuilding_HaveAndAircoThatIsOff()
        {
            // Arrange - Act
            CurrentWeatherMock weather = new CurrentWeatherMock { Temperature = 2 };
            Building sut = new Building(weather);

            // Assert
            Assert.Equal(AircoStatus.Off, sut.Airco.AircoStatus);
        }
        
        [Fact]
        public void SetAircoStatus_TurnOnWhenItsWarm_TurnOnAirco()
        {
            // Arrange
            CurrentWeatherMock weather = new CurrentWeatherMock {Temperature = 32};
            Building sut = new Building(weather);

            // Act
            sut.SetAircoStatus(AircoStatus.On);

            // Assert
            Assert.Equal(AircoStatus.On, sut.Airco.AircoStatus);
        }

        [Fact]
        public void SetAircoStatus_TurnOnWhenItsCold_ThrowsAircoTemperatureTooLowException()
        {
            // Arrange
            CurrentWeatherMock weather = new CurrentWeatherMock {Temperature = 2};
            Building sut = new Building(weather);

            // Act
            Action act = () => sut.SetAircoStatus(AircoStatus.On);

            // Assert
            Assert.ThrowsAny<AircoTemperatureTooLowException>(() => act());
        }

        [Fact]
        public void SetAircoStatus_TurnOffWhenItsCold_TurnOffAirco()
        {
            // Arrange
            CurrentWeatherMock weather = new CurrentWeatherMock {Temperature = 2};
            Building sut = new Building(weather);

            // Act
            sut.SetAircoStatus(AircoStatus.Off);

            // Assert
            Assert.Equal(AircoStatus.Off, sut.Airco.AircoStatus);
        }

        [Fact]
        public void SetAircoStatus_TurnOffWhenItsWarm_ThrowsAircoTemperatureWarmowException()
        {
            // Arrange
            CurrentWeatherMock weather = new CurrentWeatherMock {Temperature = 32};
            Building sut = new Building(weather);

            // Act
            Action act = () => sut.SetAircoStatus(AircoStatus.Off);

            // Assert
            Assert.ThrowsAny<AircoTemperatureTooHighException>(() => act());
        }

        [Fact]
        public void SetAircoStatus_InvalidState_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            CurrentWeatherMock weather = new CurrentWeatherMock {Temperature = 32};
            Building sut = new Building(weather);

            // Act
            Action act = () => sut.SetAircoStatus((AircoStatus)Enum.Parse(typeof(AircoStatus),"3"));

            // Assert
            Assert.ThrowsAny<ArgumentOutOfRangeException>(() => act());
        }

        [Fact]
        public void ToString_DefaultBuilding_ValidToString()
        {
            // Arrange
            CurrentWeatherMock weather = new CurrentWeatherMock { Temperature = 32 };
            Building sut = new Building(weather) {Name = "Test building"};

            // Act
            string result = sut.ToString();

            // Assert
            Assert.Equal("Test building (Airco: Off)", result);
        }

    }
}
