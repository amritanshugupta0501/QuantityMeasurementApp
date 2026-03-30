using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModel
{
    public class UserRegisterDTO
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}