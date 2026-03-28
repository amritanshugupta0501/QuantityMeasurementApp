using System;
using QuantityMeasurementModel;
using QuantityMeasurementRepository;
namespace QuantityMeasurementService
{
    public class QuantityMeasurementServices : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepo _repository;
        public QuantityMeasurementServices(IQuantityMeasurementRepo repository)
        {
            _repository = repository;
        }
        public QuantityMeasurementDTO Compare(QuantityInputDTO request)
        {
            try
            {
                return request.thisQuantityDTO.measurementType switch
                {
                    "LengthUnit" => ExecuteCompare<LengthUnit>(request, LengthConverter.Instance),
                    "VolumeUnit" => ExecuteCompare<VolumeUnit>(request, VolumeConverter.Instance),
                    "WeightUnit" => ExecuteCompare<WeightUnit>(request, WeightConverter.Instance),
                    "TemperatureUnit" => ExecuteCompare<TemperatureUnit>(request, TemperatureConverter.Instance),
                    _ => throw new ArgumentException("Invalid measurement type.")
                };
            }
            catch (Exception ex)
            {
                return HandleError(request, MeasurementOperation.Compare, ex.Message);
            }
        }
        public QuantityMeasurementDTO Add(QuantityInputDTO request)
        {
            try
            {
                return request.thisQuantityDTO.measurementType switch
                {
                    "LengthUnit" => ExecuteAdd<LengthUnit>(request, LengthConverter.Instance),
                    "VolumeUnit" => ExecuteAdd<VolumeUnit>(request, VolumeConverter.Instance),
                    "WeightUnit" => ExecuteAdd<WeightUnit>(request, WeightConverter.Instance),
                    "TemperatureUnit" => throw new InvalidOperationException("Cannot add temperatures."),
                    _ => throw new ArgumentException("Invalid measurement type.")
                };
            }
            catch (Exception ex)
            {
                return HandleError(request, MeasurementOperation.Add, ex.Message);
            }
        }
        public QuantityMeasurementDTO Convert(QuantityInputDTO request)
        {
            try
            {
                return request.thisQuantityDTO.measurementType switch
                {
                    "LengthUnit" => ExecuteConvert<LengthUnit>(request, LengthConverter.Instance),
                    "VolumeUnit" => ExecuteConvert<VolumeUnit>(request, VolumeConverter.Instance),
                    "WeightUnit" => ExecuteConvert<WeightUnit>(request, WeightConverter.Instance),
                    "TemperatureUnit" => ExecuteConvert<TemperatureUnit>(request, TemperatureConverter.Instance),
                    _ => throw new ArgumentException("Invalid measurement type.")
                };
            }
            catch (Exception ex)
            {
                return HandleError(request, MeasurementOperation.Convert, ex.Message);
            }
        }
        private QuantityMeasurementDTO ExecuteCompare<TUnit>(QuantityInputDTO req, IMeasurable<TUnit> converter) where TUnit : struct, Enum
        {
            var (q1, q2, unit1, unit2) = BuildQuantities(req, converter);
            bool areEqual = q1.Equals(q2);
            return SaveAndMap(req, MeasurementOperation.Compare, resultString: areEqual.ToString().ToLower());
        }
        private QuantityMeasurementDTO ExecuteAdd<TUnit>(QuantityInputDTO req, IMeasurable<TUnit> converter) where TUnit : struct, Enum
        {
            var (q1, q2, unit1, unit2) = BuildQuantities(req, converter);
            var resultQuantity = q1.Add(q2, unit1);
            return SaveAndMap(req, MeasurementOperation.Add, resultValue: resultQuantity.ConvertTo(unit1), resultUnit: unit1.ToString());
        }
        private QuantityMeasurementDTO ExecuteConvert<TUnit>(QuantityInputDTO req, IMeasurable<TUnit> converter) where TUnit : struct, Enum
        {
            var (q1, _, _, targetUnit) = BuildQuantities(req, converter);
            double convertedValue = q1.ConvertTo(targetUnit);
            return SaveAndMap(req, MeasurementOperation.Convert, resultValue: convertedValue, resultUnit: targetUnit.ToString());
        }
        private (Quantity<TUnit> q1, Quantity<TUnit> q2, TUnit u1, TUnit u2) BuildQuantities<TUnit>(QuantityInputDTO req, IMeasurable<TUnit> converter) where TUnit : struct, Enum
        {
            if (req.thisQuantityDTO.value < 0 || req.thatQuantityDTO.value < 0)
            {
                throw new Exception("Measurement values cannot be negative.");
            }
            if (!Enum.TryParse(req.thisQuantityDTO.unit, true, out TUnit unit1) || 
                !Enum.TryParse(req.thatQuantityDTO.unit, true, out TUnit unit2))
            {
                throw new Exception("Invalid unit provided for the given measurement type.");
            }
            var q1 = new Quantity<TUnit>(req.thisQuantityDTO.value, unit1, converter);
            var q2 = new Quantity<TUnit>(req.thatQuantityDTO.value, unit2, converter);
            return (q1, q2, unit1, unit2);
        }

        private QuantityMeasurementDTO SaveAndMap(QuantityInputDTO req, MeasurementOperation action, 
            string resultString = null, double resultValue = 0, string resultUnit = null)
        {
            var entity = new QuantityMeasurementEntity
            {
                FirstValue = req.thisQuantityDTO.value,
                FirstUnit = req.thisQuantityDTO.unit,
                Category = req.thisQuantityDTO.measurementType,
                SecondValue = req.thatQuantityDTO.value,
                SecondUnit = req.thatQuantityDTO.unit,
                MeasurementAction = action,
                ResultString = resultString,
                CalculatedResult = resultValue,
                TargetUnit = resultUnit,
                IsError = false,
                CreatedAt = DateTime.UtcNow
            };
            _repository.SaveMeasurement(entity);
            return new QuantityMeasurementDTO
            {
                thisValue = entity.FirstValue, 
                thisUnit = entity.FirstUnit, 
                thisMeasurementType = entity.Category,
                
                thatValue = entity.SecondValue, 
                thatUnit = entity.SecondUnit, 
                thatMeasurementType = entity.Category,
                
                operation = entity.MeasurementAction.ToString(), 
                resultString = entity.ResultString, 
                resultValue = entity.CalculatedResult,
                resultUnit = entity.TargetUnit, 
                resultMeasurementType = entity.Category, 
                error = false
            };
        }
        private QuantityMeasurementDTO HandleError(QuantityInputDTO req, MeasurementOperation action, string errorMessage)
        {
            var entity = new QuantityMeasurementEntity
            {
                FirstValue = req.thisQuantityDTO.value, 
                FirstUnit = req.thisQuantityDTO.unit, 
                Category = req.thisQuantityDTO.measurementType,
                
                SecondValue = req.thatQuantityDTO.value, 
                SecondUnit = req.thatQuantityDTO.unit, 
                
                MeasurementAction = action, 
                IsError = true, 
                ErrorMessage = errorMessage, 
                CreatedAt = DateTime.UtcNow
            };

            _repository.SaveMeasurement(entity);

            return new QuantityMeasurementDTO { error = true, errorMessage = errorMessage };
        }
        public System.Collections.Generic.IEnumerable<QuantityMeasurementDTO> GetHistoryByType(string type)
        {
            var records = _repository.GetAllMeasurements()
                .Where(m => m.Category != null && m.Category.ToLower() == type.ToLower());
            
            return records.Select(MapToReceiptDTO);
        }
        public System.Collections.Generic.IEnumerable<QuantityMeasurementDTO> GetHistoryByOperation(string operation)
        {
            if (Enum.TryParse<MeasurementOperation>(operation, true, out var actionEnum))
            {
                var records = _repository.GetAllMeasurements().Where(m => m.MeasurementAction == actionEnum);
                return records.Select(MapToReceiptDTO);
            }
            return new System.Collections.Generic.List<QuantityMeasurementDTO>();
        }
        public System.Collections.Generic.IEnumerable<QuantityMeasurementDTO> GetErroredHistory()
        {
            var records = _repository.GetAllMeasurements().Where(m => m.IsError);
            return records.Select(MapToReceiptDTO);
        }
        public int GetOperationCount(string operation)
        {
            if (Enum.TryParse<MeasurementOperation>(operation, true, out var actionEnum))
            {
                return _repository.GetAllMeasurements().Count(m => m.MeasurementAction == actionEnum && !m.IsError);
            }
            return 0;
        }
        private QuantityMeasurementDTO MapToReceiptDTO(QuantityMeasurementEntity entity)
        {
            return new QuantityMeasurementDTO
            {
                thisValue = entity.FirstValue, thisUnit = entity.FirstUnit, thisMeasurementType = entity.Category,
                thatValue = entity.SecondValue, thatUnit = entity.SecondUnit, thatMeasurementType = entity.Category,
                operation = entity.MeasurementAction.ToString(), resultString = entity.ResultString, resultValue = entity.CalculatedResult,
                resultUnit = entity.TargetUnit, resultMeasurementType = entity.Category, 
                error = entity.IsError, errorMessage = entity.ErrorMessage
            };
        }
    }
}