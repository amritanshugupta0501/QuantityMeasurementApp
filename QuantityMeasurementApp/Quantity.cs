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
        // A Centralized helper method for all arithmetic operations.
        private Quantity<TUnit> PerformArithmetic(Quantity<TUnit> other, TUnit targetUnit, ArithmeticOperation operation)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            double val1Base = _converter.ToBaseUnit(_unit, _value);
            double val2Base = _converter.ToBaseUnit(other._unit, other._value);
            double resultBase = operation switch
            {
                ArithmeticOperation.Add => val1Base + val2Base,
                ArithmeticOperation.Subtract => val1Base - val2Base,
                ArithmeticOperation.Divide => val1Base / val2Base,
                _ => throw new InvalidOperationException("Unknown arithmetic operation")
            };
            double finalValue = _converter.FromBaseUnit(targetUnit, resultBase);
            return new Quantity<TUnit>(finalValue, targetUnit, _converter);
        }
        // Adds two quantities together by converting both to a base unit, summing them, and returning a new Quantity in the target unit.
        public Quantity<TUnit> Add(Quantity<TUnit> other, TUnit targetUnit)
        {
            return PerformArithmetic(other, targetUnit, ArithmeticOperation.Add);
        }
        // Subtracts two quantities together by converting both to a base unit and returning a new Quantity in the target unit.
        public Quantity<TUnit> Subtract(Quantity<TUnit> other, TUnit targetUnit)
        {
            return PerformArithmetic(other, targetUnit, ArithmeticOperation.Subtract);
        }
        // Divides two quantities together by converting both to a base unit and returning a new Quantity in the target unit.
        public Quantity<TUnit> Division(Quantity<TUnit> other, TUnit targetUnit)
        {
            return PerformArithmetic(other, targetUnit, ArithmeticOperation.Divide);
        }
        // Compares two quantities for equality by converting both to their base units and checking if the difference is within a small epsilon.
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is not Quantity<TUnit> other)
            {
                return false;
            }
            double firstBase = _converter.ToBaseUnit(_unit, _value);
            double secondBase = _converter.ToBaseUnit(other._unit, other._value);

            const double epsilon = 1e-6;
            return Math.Abs(firstBase - secondBase) < epsilon;
        }
    }
}