using System;
using System.IO;
using Warehouse.Models.Interfaces;

namespace Warehouse.Models.WeatherService
{
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class CurrentWeather : ICurrentWeather
    {
        private static readonly GlobalWeatherSoap WeatherClient = new GlobalWeatherSoapClient("GlobalWeatherSoap");

        public IGetValueFromString GetTemparatureFromString { get; } = new GetValueFromStringWithBrackets();

        public string Location { get; set; }
        public string Time { get; set; }
        public string Wind { get; set; }
        public string Visibility { get; set; }
        public string Temperature { get; set; }
        public string DewPoint { get; set; }
        public string RelativeHumidity { get; set; }
        public string Pressure { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// Gets the temperatures in celcius.
        /// Converts a string like '41 F (5 C)' to '5' as a double
        /// </summary>
        /// <returns>
        /// The temparature in Celcius
        /// </returns>
        /// <exception cref="System.InvalidCastException">When temparature can not be parsed to Celsius</exception>
        public decimal GetTemperatureInCelcius()
        {
            return GetTemparatureFromString.GetValueFromString(Temperature);
        }

        public static CurrentWeather GetCurrentWeather()
        {
            return ParseXml(WeatherClient.GetWeather("rotterdam", "netherlands"));
        }

        private static CurrentWeather ParseXml(string s)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof (CurrentWeather));
            TextReader file = new StringReader(s);
            CurrentWeather currentWeather = (CurrentWeather) reader.Deserialize(file);
            file.Close();
            return currentWeather;
        }
    }
}
