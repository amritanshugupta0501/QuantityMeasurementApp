using System;
using System.Collections.Generic;

namespace QuantityMeasurementApp
{
    // Represents units of length and handles conversion to/from the base unit (inch).
    // Keeping conversion details inside the unit type makes it easier to add other
    // measurement categories later (weight, volume, etc.) without circular
    // dependencies or a monolithic converter class.
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
                throw new InvalidMeasurementException($"Unsupported length unit: {unit}");
            return value * factor;
        }

        public static double FromBaseUnit(this LengthUnit unit, double baseValue)
        {
            if (!ToInches.TryGetValue(unit, out double factor))
                throw new InvalidMeasurementException($"Unsupported length unit: {unit}");
            return baseValue / factor;
        }
    }
}
