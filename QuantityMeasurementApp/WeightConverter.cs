using System.Collections.Generic;

namespace QuantityMeasurementApp
{
    public class WeightConverter : IUnitConverter<WeightUnit>
    {
        public static readonly WeightConverter Instance = new();

        private readonly Dictionary<WeightUnit, double> _toKiloGrams = new()
        {
            { WeightUnit.KILOGRAM, 1.0 },
            { WeightUnit.GRAM, 0.001 },
            { WeightUnit.POUND, 0.453592 }
        };

        public double ToBaseUnit(WeightUnit unit, double value) => value * _toKiloGrams[unit];
        public double FromBaseUnit(WeightUnit unit, double baseValue) => baseValue / _toKiloGrams[unit];
    }
}