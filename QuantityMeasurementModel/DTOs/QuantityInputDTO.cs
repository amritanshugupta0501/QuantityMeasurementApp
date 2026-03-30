using System.ComponentModel.DataAnnotations;
namespace QuantityMeasurementModel
{
    public class QuantityInputDTO
    {
        [Required(ErrorMessage = "The first quantity details are required.")]
        public QuantityDTO thisQuantityDTO { get; set; }
        [Required(ErrorMessage = "The second quantity details are required.")]
        public QuantityDTO thatQuantityDTO { get; set; }
    }
}