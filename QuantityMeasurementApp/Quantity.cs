using System;

namespace QuantityMeasurementApp
{
    public class Quantity<TUnit> where TUnit : Enum
    {
        private readonly double _value;
        private readonly TUnit _unit;
        private readonly IUnitConverter<TUnit> _converter;
        // Initializes a new measurement with its numeric value, specific unit, and the required converter.
        public Quantity(double value, TUnit unit, IUnitConverter<TUnit> converter)
        {
            _value = value;
            _unit = unit;
            _converter = converter;
        }
        // Initializes a new measurement with its numeric value, specific unit, and the required converter.
        public double ConvertTo(TUnit targetUnit)
        {
            double baseValue = _converter.ToBaseUnit(_unit, _value);
            return _converter.FromBaseUnit(targetUnit, baseValue);
        }
        // Adds two quantities together by converting both to a base unit, summing them, and returning a new Quantity in the target unit.
        public Quantity<TUnit> Add(Quantity<TUnit> other, TUnit targetUnit)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            double val1Base = _converter.ToBaseUnit(_unit, _value);
            double val2Base = _converter.ToBaseUnit(other._unit, other._value);
            
            double sumBase = val1Base + val2Base;
            double finalValue = _converter.FromBaseUnit(targetUnit, sumBase);

            return new Quantity<TUnit>(finalValue, targetUnit, _converter);
        }
        // Compares two quantities for equality by converting both to their base units and checking if the difference is within a small epsilon.
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not Quantity<TUnit> other) return false;

            double firstBase = _converter.ToBaseUnit(_unit, _value);
            double secondBase = _converter.ToBaseUnit(other._unit, other._value);

            const double epsilon = 1e-6;
            return Math.Abs(firstBase - secondBase) < epsilon;
        }
    }
}