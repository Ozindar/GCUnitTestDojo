using Warehouse.Models.Interfaces;

namespace Warehouse.Models.UnitTests.Mocks
{
    public class CurrentWeatherMock : ICurrentWeather
    {
        public decimal Temperature { get; set; }

        public decimal GetTemperatureInCelcius()
        {
            return Temperature;
        }
    }
}