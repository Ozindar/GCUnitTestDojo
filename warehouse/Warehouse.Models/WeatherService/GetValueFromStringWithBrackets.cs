using System;

namespace Warehouse.Models.WeatherService
{
    public class GetValueFromStringWithBrackets : IGetValueFromString
    {
        /// <summary>
        /// Gets the temperatures in celcius.
        /// Converts a string like '41 F (5 C)' to '5' as a double
        /// </summary>
        public decimal GetValueFromString(string value)
        {
            // TODO more logic to make this safe...
            decimal temparature;
            int firstBracket = value.IndexOf("(", StringComparison.Ordinal) + 1;
            int secondBracket = value.IndexOf(")", StringComparison.Ordinal) - 2;
            string temperatureString = value.Substring(firstBracket, secondBracket - firstBracket);
            if (!decimal.TryParse(temperatureString, out temparature))
            {
                throw new InvalidCastException($"Error in parsing Celcius temperature: {value}");
            }

            return temparature;
        }
    }
}
