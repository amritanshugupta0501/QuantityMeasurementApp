using QuantityMeasurementModel;

namespace QuantityMeasurementRepository
{
    public interface IQuantityMeasurementRepo
    {
        QuantityMeasurementEntity[] GetAllMeasurements();    
        void SaveMeasurement(QuantityMeasurementEntity entity);
        QuantityMeasurementEntity GetMeasurementById(int id);
        bool DeleteMeasurement(int id);
    }
}