using NUnit.Framework;
using QuantityMeasurementModel;   
using QuantityMeasurementService; 

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityMeasurementServiceWeightTests
    {
        private IQuantityMeasurementService _service;
        private const double Epsilon = 1e-6;

        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementServices();
        }

        // Comparison Tests 
        [Test]
        public void Compare_KilogramsAndGrams_SameMagnitude_ReturnsTrue()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Weight",
                OperationType = MeasurementAction.Compare,
                MeasurementValueFirst = 1.0,
                MeasurementUnitFirst = "KILOGRAM",
                MeasurementValueSecond = 1000.0,
                MeasurementUnitSecond = "GRAM"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.IsComparison, Is.True);
            Assert.That(response.AreEqual, Is.True);
        }

        [Test]
        public void Compare_PoundsAndGrams_EquivalentValues_ReturnsTrue()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Weight",
                OperationType = MeasurementAction.Compare,
                MeasurementValueFirst = 1.0,
                MeasurementUnitFirst = "POUND",
                MeasurementValueSecond = 453.592,
                MeasurementUnitSecond = "GRAM"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.IsComparison, Is.True);
            Assert.That(response.AreEqual, Is.True);
        }

        // Arithmetic Tests 
        [Test]
        public void Add_KilogramAndGrams_ReturnsCorrectSumInKilograms()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Weight",
                OperationType = MeasurementAction.Add,
                MeasurementValueFirst = 1.0,
                MeasurementUnitFirst = "KILOGRAM",
                MeasurementValueSecond = 1000.0,
                MeasurementUnitSecond = "GRAM",
                TargetMeasurementUnit = "KILOGRAM"
            };

            var response = _service.ProcessMeasurement(request);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.CalculatedValue, Is.EqualTo(2.0).Within(Epsilon));
        }

        [Test]
        public void Subtract_Kilograms_ReturnsCorrectDifference()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Weight",
                OperationType = MeasurementAction.Subtract,
                MeasurementValueFirst = 10.0,
                MeasurementUnitFirst = "KILOGRAM",
                MeasurementValueSecond = 5.0,
                MeasurementUnitSecond = "KILOGRAM",
                TargetMeasurementUnit = "KILOGRAM"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.CalculatedValue, Is.EqualTo(5.0).Within(Epsilon));
        }

        [Test]
        public void Division_KilogramsAndGrams_ReturnsRatioOfOne()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Weight",
                OperationType = MeasurementAction.Divide,
                MeasurementValueFirst = 2.0,
                MeasurementUnitFirst = "KILOGRAM",
                MeasurementValueSecond = 2000.0,
                MeasurementUnitSecond = "GRAM",
                TargetMeasurementUnit = "KILOGRAM"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.CalculatedValue, Is.EqualTo(1.0).Within(Epsilon));
        }

        // Validation Logic
        [Test]
        public void Validate_MissingOrInvalidUnit_ReturnsFailedDtoWithError()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Weight",
                OperationType = MeasurementAction.Subtract,
                MeasurementValueFirst = 10.0,
                MeasurementUnitFirst = "KILOGRAM",
                MeasurementValueSecond = 5.0,
                MeasurementUnitSecond = "",
                TargetMeasurementUnit = "KILOGRAM"
            };

            var response = _service.ProcessMeasurement(request);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorMessage, Does.Contain("Invalid unit"));
        }
    }
}