using System;

namespace Warehouse.Models.Exceptions
{
    public class AircoTemperatureTooHighException : Exception
    {
        public AircoTemperatureTooHighException(string message) : base(message) { }
    }
}