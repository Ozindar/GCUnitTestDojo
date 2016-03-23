using System;
using Warehouse.Models.Enums;
using Warehouse.Models.Exceptions;

namespace Warehouse.Models
{
    using System.Collections.Generic;
    public class Building : ModelBase
    {
        public virtual IList<Aisle> Ailses { get; protected set; } = new List<Aisle>();

        public virtual Airco Airco { get; protected set; } = new Airco();

        public virtual void SetAircoStatus(AircoStatus status)
        {
            // Based on current temparature, the Airco can be turned on / off
            CurrentWeather weather = CurrentWeather.GetCurrentWeather();
            double temperatureInCelsius = weather.GetTemperatureInCelcius();

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
    }
}