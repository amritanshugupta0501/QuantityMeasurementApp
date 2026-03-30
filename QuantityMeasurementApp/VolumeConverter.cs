using System.Collections.Generic;

namespace QuantityMeasurementApp
{
    public class VolumeConverter : IUnitConverter<VolumeUnit>
    {
        public static readonly VolumeConverter Instance = new();

        private readonly Dictionary<VolumeUnit, double> _toLitres = new()
        {
            { VolumeUnit.LITRE, 1.0 },
            { VolumeUnit.MILLILITRE, 0.001 },
            { VolumeUnit.GALLON, 3.78541 }
        };

        public double ToBaseUnit(VolumeUnit unit, double value) => value * _toLitres[unit];
        public double FromBaseUnit(VolumeUnit unit, double baseValue) => baseValue / _toLitres[unit];
    }
}