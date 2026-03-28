using System.Collections.Generic;
using QuantityMeasurementModel;

namespace QuantityMeasurementService
{
    public interface IQuantityMeasurementService
    {
        QuantityMeasurementDTO Add(QuantityInputDTO request);
        QuantityMeasurementDTO Compare(QuantityInputDTO request);
        QuantityMeasurementDTO Convert(QuantityInputDTO request);
        IEnumerable<QuantityMeasurementDTO> GetHistoryByType(string type);
        IEnumerable<QuantityMeasurementDTO> GetHistoryByOperation(string operation);
        IEnumerable<QuantityMeasurementDTO> GetErroredHistory();
        int GetOperationCount(string operation);
    }
}