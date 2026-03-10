using System;

namespace QuantityMeasurementModel
{
    // A custom exception type used when a measurement value is not valid (for example, it is negative or infinite).
    public class InvalidMeasurementException : Exception
    {
        public InvalidMeasurementException(string message) : base(message)
        {
        }
    }
}