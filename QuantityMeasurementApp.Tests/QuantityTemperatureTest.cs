using NUnit.Framework;
using QuantityMeasurementModel;   
using QuantityMeasurementService; 

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityMeasurementServiceTemperatureTests
    {
        private IQuantityMeasurementService _service;

        [SetUp]
        public void Setup()
        {
            _service = new QuantityMeasurementServices();
        }

        // Comparison & Equality Tests 
        [Test]
        public void Compare_CelsiusAndFahrenheit_FreezingPoint_ReturnsTrue()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Temperature",
                OperationType = MeasurementAction.Compare,
                MeasurementValueFirst = 0.0,
                MeasurementUnitFirst = "CELSIUS",
                MeasurementValueSecond = 32.0,
                MeasurementUnitSecond = "FAHRENHEIT"
            };

            var response = _service.ProcessMeasurement(request);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.IsComparison, Is.True);
            Assert.That(response.AreEqual, Is.True);
        }

        [Test]
        public void Compare_KelvinAndCelsius_AbsoluteZeroOffset_ReturnsTrue()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Temperature",
                OperationType = MeasurementAction.Compare,
                MeasurementValueFirst = 273.15,
                MeasurementUnitFirst = "KELVIN",
                MeasurementValueSecond = 0.0,
                MeasurementUnitSecond = "CELSIUS"
            };

            var response = _service.ProcessMeasurement(request);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.IsComparison, Is.True);
            Assert.That(response.AreEqual, Is.True);
        }

        [Test]
        public void Compare_FahrenheitAndCelsius_BoilingPoint_ReturnsTrue()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Temperature",
                OperationType = MeasurementAction.Compare,
                MeasurementValueFirst = 212.0,
                MeasurementUnitFirst = "FAHRENHEIT",
                MeasurementValueSecond = 100.0,
                MeasurementUnitSecond = "CELSIUS"
            };

            var response = _service.ProcessMeasurement(request);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.IsComparison, Is.True);
            Assert.That(response.AreEqual, Is.True);
        }
        [TestCase(MeasurementAction.Add)]
        [TestCase(MeasurementAction.Subtract)]
        [TestCase(MeasurementAction.Divide)]
        public void Arithmetic_Temperatures_ReturnsFailedDto(MeasurementAction action)
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Temperature",
                OperationType = action,
                MeasurementValueFirst = 100.0,
                MeasurementUnitFirst = "CELSIUS",
                MeasurementValueSecond = 50.0,
                MeasurementUnitSecond = "CELSIUS",
                TargetMeasurementUnit = "CELSIUS"
            };

            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorMessage, Is.Not.Null.And.Not.Empty);
        }
    }
}