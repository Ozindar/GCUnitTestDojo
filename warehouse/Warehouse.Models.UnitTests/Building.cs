﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Models.Enums;
using Warehouse.Models.Exceptions;
using Xunit;

namespace Warehouse.Models.UnitTests
{
    using Interfaces;

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

    }

    public class CurrentWeatherMock : ICurrentWeather
    {
        public double Temperature { get; set; }

        public double GetTemperatureInCelcius()
        {
            return Temperature;
        }
    }
}