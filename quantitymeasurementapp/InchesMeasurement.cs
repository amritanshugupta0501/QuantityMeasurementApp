using System;

namespace QuantityMeasurementApp
{
    // UC - 01 : Checking the equality of two numerical values measured in Inches
    public class InchesMeasurement
    {
        private readonly double _valueInInches;
        // Constructor saves the provided value.
        public InchesMeasurement(double valueInInches)
        {
            _valueInInches = valueInInches;
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
            InchesMeasurement otherInches = (InchesMeasurement)obj;
            return _valueInInches.Equals(otherInches._valueInInches);
        }
    }
}