using System;

namespace QuantityMeasurementApp
{
    public class QuantityWeight
    {
        private readonly double _measurementValue;
        private readonly WeightUnit _measurementUnit;

        public QuantityWeight(double measurementValue, WeightUnit measurementUnit)
        {
            _measurementValue = measurementValue;
            _measurementUnit = measurementUnit;
        }
        public static double Convert(double value, WeightUnit sourceUnit, WeightUnit targetUnit)
        {
            // value validation
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                throw new InvalidMeasurementException($"Cannot convert invalid quantity: {value}");
            }
            // unit validation
            if (!Enum.IsDefined(typeof(WeightUnit), sourceUnit) ||
                !Enum.IsDefined(typeof(WeightUnit), targetUnit))
            {
                throw new InvalidMeasurementException($"One or more units are not supported: {sourceUnit}, {targetUnit}");
            }

            // perform conversion via base unit (handled by the unit type)
            double valueInBase = sourceUnit.ToBaseUnit(value);
            double result = targetUnit.FromBaseUnit(valueInBase);
            return result;
        }

        public double ConvertTo(WeightUnit targetUnit) =>
            Convert(_measurementValue, _measurementUnit, targetUnit);

        public QuantityWeight Add(QuantityWeight other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            double otherInThis = Convert(other._measurementValue, other._measurementUnit, _measurementUnit);
            double sum = _measurementValue + otherInThis;
            return new QuantityWeight(sum, _measurementUnit);
        }

        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            var baseSum = Add(other);
            double converted = baseSum.ConvertTo(targetUnit);
            return new QuantityWeight(converted, targetUnit);
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
            QuantityWeight otherMeasurement = (QuantityWeight)obj;
            double firstMeasurement = _measurementValue * _measurementUnit.ToBaseUnit(1.0);
            double secondMeasurement = otherMeasurement._measurementValue * otherMeasurement._measurementUnit.ToBaseUnit(1.0);
            const double epsilon = 1e-6;
            return Math.Abs(firstMeasurement - secondMeasurement) < epsilon;
        }
    }
}

