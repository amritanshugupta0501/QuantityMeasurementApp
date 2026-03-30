using System;

namespace QuantityMeasurementApp
{
    public enum MeasurementUnit 
    {
        INCH ,
        FEET ,
        YARD ,
        CENTIMETRE 
    }

    public class UnitConverter
    {
        public static readonly Dictionary<MeasurementUnit, double> MeasurementUnits = new()
        {
            { MeasurementUnit.INCH, 1.0 },
            { MeasurementUnit.FEET, 12.0 },
            { MeasurementUnit.YARD, 36.0 },
            { MeasurementUnit.CENTIMETRE, 0.39370078740157477 }
        };
    }
}