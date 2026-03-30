using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityLengthTests
    {
        private readonly IMeasurable<LengthUnit> _converter = LengthConverter.Instance;
        // Comparison Tests 
        [Test]
        public void Equals_SameValuesInDifferentUnits_ReturnsTrue()
        {
            var foot = new Quantity<LengthUnit>(1.0, LengthUnit.FEET, _converter);
            var inch = new Quantity<LengthUnit>(12.0, LengthUnit.INCH, _converter);
            Assert.That(foot, Is.EqualTo(inch)); // NUnit uses your .Equals() here
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
            var first = new Quantity<LengthUnit>(1.0, LengthUnit.FEET, _converter);
            var second = new Quantity<LengthUnit>(12.0, LengthUnit.INCH, _converter);

            var result = first.Add(second, LengthUnit.FEET);

            Assert.That(result.ConvertTo(LengthUnit.FEET), Is.EqualTo(2.0).Within(1e-6));
        }
        [Test]
        public void Subtract_FeetAndInches_ReturnsCorrectFeet()
        {
            var feet = new Quantity<LengthUnit>(10.0, LengthUnit.FEET, _converter);
            var inches = new Quantity<LengthUnit>(6.0, LengthUnit.INCH, _converter);

            var result = feet.Subtract(inches, LengthUnit.FEET);

            Assert.That(result.ConvertTo(LengthUnit.FEET), Is.EqualTo(9.5).Within(1e-6));
        }
        // Validation Tests 
        [TestCase(ArithmeticOperation.Add)]
        [TestCase(ArithmeticOperation.Subtract)]
        [TestCase(ArithmeticOperation.Divide)]
        public void Arithmetic_NullOperand_ThrowsArgumentNullException(ArithmeticOperation op)
        {
            var q = new Quantity<LengthUnit>(1.0, LengthUnit.INCH, _converter);
            Assert.Throws<ArgumentNullException>(() => q.Add(null!, LengthUnit.INCH));
        }
    }
}