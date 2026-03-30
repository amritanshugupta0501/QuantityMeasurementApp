using NUnit.Framework;
using QuantityMeasurementModel;   
using QuantityMeasurementService; 

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
                OperationType = MeasurementAction.Compare,
                MeasurementValue1 = 1.0,
                MeasurementUnit1 = "FEET",
                MeasurementValue2 = 12.0,
                MeasurementUnit2 = "INCH"
            };

            var response = _service.ProcessMeasurement(request);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.IsComparison, Is.True);
            Assert.That(response.AreEqual, Is.True); 
        }
        [Test]
        public void Given1InchAnd2InchesWhenComparedShouldReturnFalse()
        {
            var inches1 = new QuantityLength(1.0, LengthUnit.INCH);
            var inches2 = new QuantityLength(2.0, LengthUnit.INCH);
            Assert.That(inches1.Equals(inches2), Is.False);
        }
        [Test]
        public void Given1FootAnd1FootWhenComparedShouldReturnTrue()
        {
            var feet1 = new QuantityLength(1.0, LengthUnit.FEET);
            var feet2 = new QuantityLength(1.0, LengthUnit.FEET);
            Assert.That(feet1.Equals(feet2), Is.True);
        }
        [Test]
        public void Given1FootAnd12InchesWhenComparedShouldReturnTrue()
        {
            var foot = new QuantityLength(1.0, LengthUnit.FEET);
            var inches = new QuantityLength(12.0, LengthUnit.INCH);
            Assert.That(foot.Equals(inches), Is.True);
        }
        [Test]
        public void Given12InchesAnd1FootWhenComparedShouldReturnTrue()
        {
            var inches = new QuantityLength(12.0, LengthUnit.INCH);
            var foot = new QuantityLength(1.0, LengthUnit.FEET);
            Assert.That(inches.Equals(foot), Is.True);
        }
        [Test]
        public void Given1FootAnd1InchWhenComparedShouldReturnFalse()
        {
            var foot = new QuantityLength(1.0, LengthUnit.FEET);
            var inch = new QuantityLength(1.0, LengthUnit.INCH);
            
            Assert.That(foot.Equals(inch), Is.False);
        }
        [Test]
        public void GivenNullMeasurementWhenComparedShouldReturnFalse()
        {
            var foot = new QuantityLength(1.0, LengthUnit.FEET);
            Assert.That(foot.Equals(null), Is.False);
        }

        [Test]
        public void GivenSameReferenceWhenComparedShouldReturnTrue()
        {
            var foot = new QuantityLength(1.0, LengthUnit.FEET);
            Assert.That(foot.Equals(foot), Is.True);
        }
        [Test]
        public void Given1YardAnd36InchesWhenComparedShouldReturnTrue()
        {
            var yard = new QuantityLength(1.0, LengthUnit.YARD);
            var inches = new QuantityLength(36.0, LengthUnit.INCH);
            Assert.That(yard.Equals(inches), Is.True);
        }

        [Test]
        public void Given1InchAnd2InchesWhenComparedShouldReturnFalse()
        {
            var inches1 = new Quantity<LengthUnit>(1.0, LengthUnit.INCH, LengthConverter.Instance);
            var inches2 = new Quantity<LengthUnit>(2.0, LengthUnit.INCH, LengthConverter.Instance);
            Assert.That(inches1.Equals(inches2), Is.False);
        }

        [Test]
        public void Given1FootAnd12InchesWhenComparedShouldReturnTrue()
        {
            var foot = new Quantity<LengthUnit>(1.0, LengthUnit.FEET, LengthConverter.Instance);
            var inches = new Quantity<LengthUnit>(12.0, LengthUnit.INCH, LengthConverter.Instance);
            Assert.That(foot.Equals(inches), Is.True);
        }

        [Test]
        public void Given1YardAnd36InchesWhenComparedShouldReturnTrue()
        {
            var yard = new Quantity<LengthUnit>(1.0, LengthUnit.YARD, LengthConverter.Instance);
            var inches = new Quantity<LengthUnit>(36.0, LengthUnit.INCH, LengthConverter.Instance);
            Assert.That(yard.Equals(inches), Is.True);
        }

        [Test]
        public void Convert_2Point54CentimetresToInches_ReturnsApproximatelyOne()
        {
            var quantity = new Quantity<LengthUnit>(2.54, LengthUnit.CENTIMETRE, LengthConverter.Instance);
            double result = quantity.ConvertTo(LengthUnit.INCH);
            Assert.That(result, Is.EqualTo(1.0).Within(1e-6));
        }

        // Arithmetic Tests 
        [Test]
        public void Add_OneFootAndTwelveInches_ReturnsTwoFeet()
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Length",
                OperationType = MeasurementAction.Add,
                MeasurementValue1 = 1.0,
                MeasurementUnit1 = "FEET",
                MeasurementValue2 = 12.0,
                MeasurementUnit2 = "INCH",
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
                OperationType = MeasurementAction.Subtract,
                MeasurementValue1 = 10.0,
                MeasurementUnit1 = "FEET",
                MeasurementValue2 = 6.0,
                MeasurementUnit2 = "INCH",
                TargetMeasurementUnit = "FEET"
            };

            var response = _service.ProcessMeasurement(request);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.CalculatedValue, Is.EqualTo(9.5).Within(1e-6));
        }

        // Validation Tests 
        [TestCase(MeasurementAction.Add)]
        [TestCase(MeasurementAction.Subtract)]
        [TestCase(MeasurementAction.Divide)]
        public void Arithmetic_InvalidUnit_ReturnsFailedDto(MeasurementAction op)
        {
            var request = new MeasurementRequestDTO
            {
                MeasurementCategory = "Length",
                OperationType = op,
                MeasurementValue1 = 1.0,
                MeasurementUnit1 = "NOT_A_REAL_UNIT", 
                MeasurementValue2 = 12.0,
                MeasurementUnit2 = "INCH",
                TargetMeasurementUnit = "FEET"
            };

            var response = _service.ProcessMeasurement(request);
            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.ErrorMessage, Is.EqualTo("Invalid unit provided."));
        }
    }
}