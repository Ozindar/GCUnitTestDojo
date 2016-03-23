using System;

namespace Warehouse.Models.Exceptions
{
    public class AircoTemperatureTooLowException : Exception
    {
        public AircoTemperatureTooLowException(string message) : base(message) { }
    }
}