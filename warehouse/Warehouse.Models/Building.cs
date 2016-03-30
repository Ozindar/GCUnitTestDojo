using System;
using Warehouse.Models.Enums;
using Warehouse.Models.Exceptions;

namespace Warehouse.Models
{
    using System.Collections.Generic;
    using Interfaces;

    public class Building : ModelBase
    {
        public Building(ICurrentWeather currentWeather)
        {
            CurrentWeather = currentWeather;
        }

        public Building()
        {
            CurrentWeather = WeatherService.CurrentWeather.GetCurrentWeather();
        }

        public virtual IList<Aisle> Ailses { get; protected set; } = new List<Aisle>();

        public ICurrentWeather CurrentWeather;

        public virtual Airco Airco { get; protected set; } = new Airco();

        /// <summary>
        /// Sets the airco status to On of Off
        /// </summary>
        /// <param name="status">The status.</param>
        /// <exception cref="AircoTemperatureTooHighException">Not allowed / needed to turn Airco off: temparature is too high.</exception>
        /// <exception cref="AircoTemperatureTooLowException">Not allowed / needed to turn Airco on: temperature is too low.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">null</exception>
        public virtual void SetAircoStatus(AircoStatus status)
        {
            // Based on current temparature, the Airco can be turned on / off
            decimal temperatureInCelsius = CurrentWeather.GetTemperatureInCelcius();

            switch (status)
            {
                case AircoStatus.Off:
                {
                    if (temperatureInCelsius > 10)
                    {
                        // Not allowed / needed to turn Airco off
                        throw new AircoTemperatureTooHighException("Not allowed / needed to turn Airco off: temparature is too high.");
                    }
                    break;
                }
                case AircoStatus.On:
                {
                    if (temperatureInCelsius < 5)
                    {
                        // Not allowed / needed to turn Airco on
                        throw new AircoTemperatureTooLowException("Not allowed / needed to turn Airco on: temperature is too low.");
                    }
                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
                }
            }
            this.Airco.SetAircoStatus(status);
        }

        public override string ToString()
        {
            return $"{Name} (Airco: {Airco.AircoStatus})";
        }
    }
}