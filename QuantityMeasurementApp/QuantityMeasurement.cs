using System;

namespace QuantityMeasurementApp
{
    public class QuantityMeasurement
    {
        private readonly double _measurementValue;
        private readonly MeasurementUnit _measurementUnit;
        public QuantityMeasurement(double measurementValue, MeasurementUnit measurementUnit)
        {
            _measurementValue = measurementValue;
            _measurementUnit = measurementUnit;
        }
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            QuantityMeasurement otherMeasurement = (QuantityMeasurement)obj;
            double firstMeasurement = _measurementValue * (double)_measurementUnit;
            double secondMeasurement = otherMeasurement._measurementValue * (double)otherMeasurement._measurementUnit;
            return firstMeasurement.Equals(secondMeasurement);
        }
    }
}

