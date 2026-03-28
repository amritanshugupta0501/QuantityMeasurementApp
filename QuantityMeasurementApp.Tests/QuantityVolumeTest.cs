using NUnit.Framework;
using QuantityMeasurementModel;   
using QuantityMeasurementService;
using QuantityMeasurementRepository;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityMeasurementServiceVolumeTests
    {
        private IQuantityMeasurementService _service;
        private const double Epsilon = 1e-5;

        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementServices();
        }

        // Comparison Tests
        [Test]
        public void Compare_LitreAndMillilitre_SameMagnitude_ReturnsTrue()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Volume",
                OperationType = MeasurementOperation.Compare,
                MeasurementValueFirst = 1.0,
                MeasurementUnitFirst = "LITRE",
                MeasurementValueSecond = 1000.0,
                MeasurementUnitSecond = "MILLILITRE"
            };

            var response = _service.ProcessMeasurement(request);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.IsComparison, Is.True);
            Assert.That(response.AreEqual, Is.True);
        }

        [Test]
        public void Compare_GallonAndLitre_EquivalentValues_ReturnsTrue()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Volume",
                OperationType = MeasurementOperation.Compare,
                MeasurementValueFirst = 1.0,
                MeasurementUnitFirst = "GALLON",
                MeasurementValueSecond = 3.78541,
                MeasurementUnitSecond = "LITRE"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.IsComparison, Is.True);
            Assert.That(response.AreEqual, Is.True);
        }

        // Arithmetic Tests 
        [Test]
        public void Add_LitreAndMillilitre_ReturnsSumInLitres()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Volume",
                OperationType = MeasurementOperation.Add,
                MeasurementValueFirst = 1.0,
                MeasurementUnitFirst = "LITRE",
                MeasurementValueSecond = 1000.0,
                MeasurementUnitSecond = "MILLILITRE",
                TargetMeasurementUnit = "LITRE"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.CalculatedValue, Is.EqualTo(2.0).Within(Epsilon));
        }

        [Test]
        public void Subtract_Litres_ReturnsCorrectRemainingVolume()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Volume",
                OperationType = MeasurementOperation.Subtract,
                MeasurementValueFirst = 10.0,
                MeasurementUnitFirst = "LITRE",
                MeasurementValueSecond = 3.0,
                MeasurementUnitSecond = "LITRE",
                TargetMeasurementUnit = "LITRE"
            };

            var response = _service.ProcessMeasurement(request);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.CalculatedValue, Is.EqualTo(7.0).Within(Epsilon));
        }

        [Test]
        public void Division_ByZeroValue_ReturnsInfinity()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Volume",
                OperationType = MeasurementOperation.Divide,
                MeasurementValueFirst = 10.0,
                MeasurementUnitFirst = "LITRE",
                MeasurementValueSecond = 0.0,
                MeasurementUnitSecond = "LITRE",
                TargetMeasurementUnit = "LITRE"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(double.IsInfinity(response.CalculatedValue), Is.True);
        }

        // Validation Logic
        [Test]
        public void Validate_NegativeVolume_ReturnsFailedDtoWithError()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Volume",
                OperationType = MeasurementOperation.Add,
                MeasurementValueFirst = -5.0, // Invalid negative value
                MeasurementUnitFirst = "LITRE",
                MeasurementValueSecond = 2.0,
                MeasurementUnitSecond = "LITRE",
                TargetMeasurementUnit = "LITRE"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorMessage, Does.Contain("invalid"));
        }
    }
}