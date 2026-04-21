using QuantityMeasurementModel;
using QuantityMeasurementRepository;

namespace QuantityMeasurementService
{
    public interface IAuthService
    {
        AuthResponseDTO Register(UserRegisterDTO request);
        AuthResponseDTO Login(UserLoginDTO request);
    }
}