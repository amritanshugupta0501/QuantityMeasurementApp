using System;

namespace QuantityMeasurementApp
{
    // represents a measurement of length; conversion logic lives on the unit type itself
    public class QuantityLength
    {
        private readonly double _measurementValue;
        private readonly LengthUnit _measurementUnit;

        public QuantityLength(double measurementValue, LengthUnit measurementUnit)
        {
            _measurementValue = measurementValue;
            _measurementUnit = measurementUnit;
        }

        public static double Convert(double value, LengthUnit sourceUnit, LengthUnit targetUnit)
        {
            // value validation
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new InvalidMeasurementException($"Cannot convert invalid quantity: {value}");
            }

            // unit validation
            if (!Enum.IsDefined(typeof(LengthUnit), sourceUnit) ||
                !Enum.IsDefined(typeof(LengthUnit), targetUnit))
            {
                throw new InvalidMeasurementException($"One or more units are not supported: {sourceUnit}, {targetUnit}");
            }

            // perform conversion via base unit (handled by the unit type)
            double valueInBase = sourceUnit.ToBaseUnit(value);
            double result = targetUnit.FromBaseUnit(valueInBase);
            return result;
        }

        public double ConvertTo(LengthUnit targetUnit) =>
            Convert(_measurementValue, _measurementUnit, targetUnit);

        public QuantityLength Add(QuantityLength other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            double otherInThis = Convert(other._measurementValue, other._measurementUnit, _measurementUnit);
            double sum = _measurementValue + otherInThis;
            return new QuantityLength(sum, _measurementUnit);
        }

        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            var baseSum = Add(other);
            double converted = baseSum.ConvertTo(targetUnit);
            return new QuantityLength(converted, targetUnit);
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
            QuantityLength otherMeasurement = (QuantityLength)obj;
            double firstMeasurement = _measurementValue * _measurementUnit.ToBaseUnit(1.0);
            double secondMeasurement = otherMeasurement._measurementValue * otherMeasurement._measurementUnit.ToBaseUnit(1.0);
            const double epsilon = 1e-6;
            return Math.Abs(firstMeasurement - secondMeasurement) < epsilon;
        }
    }
}

