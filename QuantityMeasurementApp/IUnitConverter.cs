namespace QuantityMeasurementApp
{
    public interface IUnitConverter<TUnit> where TUnit : Enum
    {
        double ToBaseUnit(TUnit unit, double value);
        double FromBaseUnit(TUnit unit, double baseValue);
    }
}