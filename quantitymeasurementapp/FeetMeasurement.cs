using System;

namespace QuantityMeasurementApp
{
    // UC - 01 : Checking the equality of two numerical values measured in feet
    class FeetMeasurement
    {
        private readonly double _valueInFeet;
        // Constructor saves the provided value.
        public FeetMeasurement(double valueInFeet)
        {
            _valueInFeet = valueInFeet;
        }
        // Compare the current object with another one. Two instances are equal if their stored numbers are the same.
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is null || GetType() != obj.GetType())
            {
                return false; 
            }
            FeetMeasurement otherFeet = (FeetMeasurement)obj;
            return _valueInFeet.Equals(otherFeet._valueInFeet);
        }
    }
}