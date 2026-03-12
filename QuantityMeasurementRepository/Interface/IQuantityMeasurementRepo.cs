using QuantityMeasurementModel;

namespace QuantityMeasurementRepository
{
    public interface IQuantityMeasurementRepo
    {
        void SaveMeasurement(MeasurementDetails entity);
    }
}