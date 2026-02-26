using System;
using System.Collections.Generic;

namespace QuantityMeasurementApp
{
    public enum LengthUnit
    {
        INCH,
        FEET,
        YARD,
        CENTIMETRE
    }

    public static class LengthUnitExtensions
    {
        // factors that convert a value in the given unit to the base unit (inch)
        private static readonly Dictionary<LengthUnit, double> ToInches = new()
        {
            { LengthUnit.INCH, 1.0 },
            { LengthUnit.FEET, 12.0 },
            { LengthUnit.YARD, 36.0 },
            { LengthUnit.CENTIMETRE, 0.39370078740157477 }
        };
        public static double ToBaseUnit(this LengthUnit unit, double value)
        {
            if (!ToInches.TryGetValue(unit, out double factor))
            {
                throw new InvalidMeasurementException($"Unsupported length unit: {unit}");
            }
            return value * factor;
        }
        public static double FromBaseUnit(this LengthUnit unit, double baseValue)
        {
            if (!ToInches.TryGetValue(unit, out double factor))
            {
                throw new InvalidMeasurementException($"Unsupported length unit: {unit}");
            }
            return baseValue / factor;
        }
    }
}
