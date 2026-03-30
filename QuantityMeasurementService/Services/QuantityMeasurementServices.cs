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

        public void ValidateValue(double checkValue)
        {
            if (double.IsNegative(checkValue) || double.IsInfinity(checkValue))
            {
                throw new InvalidMeasurementException($"The measurement value {checkValue} is invalid.");
            }
        }

        public MeasurementResponseDTO ProcessMeasurement(MeasurementRequestDTO request)
        {
            try
            {
                MeasurementResponseDTO response = request.MeasurementCategory.ToLower() switch
                {
                    "length" => ProcessCategory<LengthUnit>(request, LengthConverter.Instance),
                    "volume" => ProcessCategory<VolumeUnit>(request, VolumeConverter.Instance),
                    "weight" => ProcessCategory<WeightUnit>(request, WeightConverter.Instance),
                    "temperature" => ProcessCategory<TemperatureUnit>(request, TemperatureConverter.Instance),
                    _ => throw new ArgumentException("Invalid category")
                };
                if (response.IsSuccess)
                {
                    SaveToDatabase(request, response);
                }
                return response;
            }
            catch (InvalidMeasurementException ex)
            {
                return new MeasurementResponseDTO { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }

        private MeasurementResponseDTO ProcessCategory<TUnit>(MeasurementRequestDTO req, IMeasurable<TUnit> converter) where TUnit : struct, Enum
        {
            ValidateValue(req.MeasurementValueFirst);
            ValidateValue(req.MeasurementValueSecond);

            if (!Enum.TryParse(req.MeasurementUnitFirst, true, out TUnit u1) || !Enum.TryParse(req.MeasurementUnitSecond, true, out TUnit u2))
            {
                throw new InvalidMeasurementException("Invalid unit provided.");
            }
            var q1 = new Quantity<TUnit>(req.MeasurementValueFirst, u1, converter);
            var q2 = new Quantity<TUnit>(req.MeasurementValueSecond, u2, converter);
            if (req.OperationType == MeasurementAction.Compare)
            {
                return new MeasurementResponseDTO
                {
                    IsSuccess = true,
                    IsComparison = true,
                    AreEqual = q1.Equals(q2)
                };
            }

            if (!Enum.TryParse(req.TargetMeasurementUnit, true, out TUnit targetUnit))
            {
                throw new InvalidMeasurementException("Invalid target unit provided.");
            }
            bool isTemp = typeof(TUnit) == typeof(TemperatureUnit);
            if (isTemp && req.OperationType == MeasurementAction.Divide)
            {
                throw new InvalidMeasurementException("Temperature cannot be divided.");
            }
            Quantity<TUnit> result = req.OperationType switch
            {
                MeasurementAction.Add => q1.Add(q2, targetUnit),
                MeasurementAction.Subtract => q1.Subtract(q2, targetUnit),
                MeasurementAction.Divide => q1.Division(q2, targetUnit),
                _ => throw new InvalidMeasurementException("Invalid operation")
            };

            string symbol = req.OperationType switch { MeasurementAction.Add => "+", MeasurementAction.Subtract => "-", _ => "/" };

            return new MeasurementResponseDTO
            {
                IsSuccess = true,
                IsComparison = false,
                CalculatedValue = result.ConvertTo(targetUnit),
                FormattedMessage = $"{req.MeasurementValueFirst} {u1} {symbol} {req.MeasurementValueSecond} {u2} = {result.ConvertTo(targetUnit)} {targetUnit}"
            };
        }
        private void SaveToDatabase(MeasurementRequestDTO request, MeasurementResponseDTO response)
        {
            var entity = new MeasurementDetails
            {
                MeasurementCategory = request.MeasurementCategory,
                MeasurementAction = request.OperationType.ToString(),
                MeasurementValueFirst = request.MeasurementValueFirst,
                MeasurementUnitFirst = request.MeasurementUnitFirst,
                MeasurementValueSecond = request.MeasurementValueSecond,
                MeasurementUnitSecond = request.MeasurementUnitSecond,
                TargetMeasurementUnit = request.OperationType == MeasurementAction.Compare ? null : request.TargetMeasurementUnit,
                CalculatedResult = response.IsComparison ? null : response.CalculatedValue,
                ComparisonResult = response.IsComparison ? response.AreEqual : null
            };

            _repository.SaveMeasurement(entity);
        }
    }
}