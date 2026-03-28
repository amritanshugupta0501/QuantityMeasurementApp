using NUnit.Framework;
using QuantityMeasurementModel; 
using QuantityMeasurementService;
using QuantityMeasurementRepository;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityMeasurementServiceLengthTests
    {
        private IQuantityMeasurementService _service; 
        // Runs once before every single test to ensure a fresh Service
        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementServices();
        }
        // Comparison Tests 
        [Test]
        public void Compare_SameValuesInDifferentUnits_ReturnsTrue()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Length",
                OperationType = MeasurementOperation.Compare,
                MeasurementValueFirst = 1.0,
                MeasurementUnitFirst = "FEET",
                MeasurementValueSecond = 12.0,
                MeasurementUnitSecond = "INCH"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.IsComparison, Is.True);
            Assert.That(response.AreEqual, Is.True); 
        }
        // Arithmetic Tests 
        [Test]
        public void Add_OneFootAndTwelveInches_ReturnsTwoFeet()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Length",
                OperationType = MeasurementOperation.Add,
                MeasurementValueFirst = 1.0,
                MeasurementUnitFirst = "FEET",
                MeasurementValueSecond = 12.0,
                MeasurementUnitSecond = "INCH",
                TargetMeasurementUnit = "FEET"
            };

            var response = _service.ProcessMeasurement(request);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.CalculatedValue, Is.EqualTo(2.0).Within(1e-6));
            Assert.That(response.FormattedMessage, Is.EqualTo("1 FEET + 12 INCH = 2 FEET")); 
        }
        [Test]
        public void Subtract_FeetAndInches_ReturnsCorrectFeet()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Length",
                OperationType = MeasurementOperation.Subtract,
                MeasurementValueFirst = 10.0,
                MeasurementUnitFirst = "FEET",
                MeasurementValueSecond = 6.0,
                MeasurementUnitSecond = "INCH",
                TargetMeasurementUnit = "FEET"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.CalculatedValue, Is.EqualTo(9.5).Within(1e-6));
        }
        // Validation Tests 
        [TestCase(MeasurementOperation.Add)]
        [TestCase(MeasurementOperation.Subtract)]
        [TestCase(MeasurementOperation.Divide)]
        public void Arithmetic_InvalidUnit_ReturnsFailedDto(MeasurementOperation op)
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Length",
                OperationType = op,
                MeasurementValueFirst = 1.0,
                MeasurementUnitFirst = "NOT_A_REAL_UNIT", 
                MeasurementValueSecond = 12.0,
                MeasurementUnitSecond = "INCH",
                TargetMeasurementUnit = "FEET"
            };
            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorMessage, Is.EqualTo("Invalid unit provided."));
        }
    }
}