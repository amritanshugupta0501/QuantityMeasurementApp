using System;
using System.Collections.Generic;

namespace QuantityMeasurementApp
{
    public enum WeightUnit
    {
        KILOGRAM,
        GRAM,
        POUND
    }

    public static class WeightUnitExtensions
    {
        private static readonly Dictionary<WeightUnit, double> _toKiloGrams = new()
        {
            { WeightUnit.KILOGRAM, 1.0 },
            { WeightUnit.GRAM, 0.001 },
            { WeightUnit.POUND, 0.453592 }
        };
        public static double ToBaseUnit(this WeightUnit unit,double value)
        {
            if(!_toKiloGrams.TryGetValue(unit, out double factor))
            {
                throw new InvalidMeasurementException($"Unsupported weight unit: {unit}");
            }
            return value * factor;
        }
        public static double FromBaseUnit(this WeightUnit unit, double baseValue)
        {
            if(!_toKiloGrams.TryGetValue(unit, out double factor))
            {
                throw new InvalidMeasurementException($"Unsupported weight unit: {unit}");
            }
            return baseValue / factor;
        }
    }
}