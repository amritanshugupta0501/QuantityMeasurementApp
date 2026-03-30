using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModel
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public string Message { get; set; }
    }
}