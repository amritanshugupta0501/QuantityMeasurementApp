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
        public static double Convert(double value, MeasurementUnit sourceUnit, MeasurementUnit targetUnit)
        {
            // value validation
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new InvalidMeasurementException($"Cannot convert invalid quantity: {value}");
            }

            // unit validation
            if (!Enum.IsDefined(typeof(MeasurementUnit), sourceUnit) ||
                !Enum.IsDefined(typeof(MeasurementUnit), targetUnit))
            {
                throw new InvalidMeasurementException($"One or more units are not supported: {sourceUnit}, {targetUnit}");
            }

            // perform conversion via the common base (inches)
            double valueInBase = value * UnitConverter.MeasurementUnits[sourceUnit];
            double result = valueInBase / UnitConverter.MeasurementUnits[targetUnit];
            return result;
        }
        public double ConvertTo(MeasurementUnit targetUnit) =>
            Convert(_measurementValue, _measurementUnit, targetUnit);

        public QuantityMeasurement Add(QuantityMeasurement other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            // convert the other value into this unit
            double otherInThis = Convert(other._measurementValue, other._measurementUnit, _measurementUnit);
            double sum = _measurementValue + otherInThis;
            return new QuantityMeasurement(sum, _measurementUnit);
        }
        public QuantityMeasurement Add(QuantityMeasurement other, MeasurementUnit targetUnit)
        {
            var baseSum = Add(other); // result in this unit
            double converted = baseSum.ConvertTo(targetUnit);
            return new QuantityMeasurement(converted, targetUnit);
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
            double firstMeasurement = _measurementValue * UnitConverter.MeasurementUnits[_measurementUnit];
            double secondMeasurement = otherMeasurement._measurementValue * UnitConverter.MeasurementUnits[otherMeasurement._measurementUnit];
            // use a small tolerance to allow for floating-point imprecision
            const double epsilon = 1e-6;
            return Math.Abs(firstMeasurement - secondMeasurement) < epsilon;
        }
    }
}

