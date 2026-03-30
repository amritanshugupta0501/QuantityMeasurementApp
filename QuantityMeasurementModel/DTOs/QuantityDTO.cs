using System.ComponentModel.DataAnnotations;
namespace QuantityMeasurementModel
{
    public class QuantityDTO
    {
        [Required(ErrorMessage = "Measurement value is required.")]
        public double value { get; set; }
        [Required(ErrorMessage = "Measurement unit is required.")]
        public string unit { get; set; }
        [Required(ErrorMessage = "Measurement type is required.")]
        [RegularExpression("^(LengthUnit|VolumeUnit|WeightUnit|TemperatureUnit)$", 
            ErrorMessage = "Type must be LengthUnit, VolumeUnit, WeightUnit, or TemperatureUnit")]
        public string measurementType { get; set; }
    }
}