using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityLengthTests
    {
        [Test]
        public void Given0InchesAnd0InchesWhenComparedShouldReturnTrue()
        {
            var inches1 = new QuantityLength(0.0, LengthUnit.INCH);
            var inches2 = new QuantityLength(0.0, LengthUnit.INCH);
            Assert.That(inches1.Equals(inches2), Is.True);
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
        public void Given1YardAnd3FeetWhenComparedShouldReturnTrue()
        {
            var yard = new QuantityLength(1.0, LengthUnit.YARD);
            var feet = new QuantityLength(3.0, LengthUnit.FEET);
            Assert.That(yard.Equals(feet), Is.True);
        }

        [Test]
        public void Convert_2Point54CentimetresToInches_ReturnsApproximatelyOne()
        {
            double result = QuantityLength.Convert(2.54, LengthUnit.CENTIMETRE, LengthUnit.INCH);
            Assert.That(result, Is.EqualTo(1.0).Within(1e-6));
        }

        [Test]
        public void Given1CentimetreAnd1InchWhenComparedShouldReturnFalse()
        {
            var cm = new QuantityLength(1.0, LengthUnit.CENTIMETRE);
            var inch = new QuantityLength(1.0, LengthUnit.INCH);
            Assert.That(cm.Equals(inch), Is.False);
        }
        [Test]
        public void StaticConvert_OneFootToInches_ReturnsTwelve()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.FEET, LengthUnit.INCH);
            Assert.That(result, Is.EqualTo(12.0));
        }
        [Test]
        public void InstanceConvert_OneYardToInches_ReturnsThirtySix()
        {
            var yard = new QuantityLength(1.0, LengthUnit.YARD);
            double result = yard.ConvertTo(LengthUnit.INCH);
            Assert.That(result, Is.EqualTo(36.0));
        }
        [Test]
        public void StaticConvert_TwoPointFiftyFourCentimetresToFeet_ReturnsOneTwelfth()
        {
            double value = QuantityLength.Convert(2.54, LengthUnit.CENTIMETRE, LengthUnit.FEET);
            Assert.That(value, Is.EqualTo(1.0 / 12.0).Within(1e-9));
        }
        [Test]
        public void Convert_InfiniteValue_ThrowsInvalidMeasurementException()
        {
            Assert.Throws<InvalidMeasurementException>(() =>
                QuantityLength.Convert(double.PositiveInfinity, LengthUnit.INCH, LengthUnit.FEET));
        }
        [Test]
        public void Add_OneFootAndTwelveInches_ReturnsTwoFeet()
        {
            var first = new QuantityLength(1.0, LengthUnit.FEET);
            var second = new QuantityLength(12.0, LengthUnit.INCH);
            var result = first.Add(second);
            Assert.That(result, Is.EqualTo(new QuantityLength(2.0, LengthUnit.FEET)));
        }

        [Test]
        public void Add_OneYardAndOneFoot_ReturnsOnePointThreeThreeYards()
        {
            var yard = new QuantityLength(1.0, LengthUnit.YARD);
            var foot = new QuantityLength(1.0, LengthUnit.FEET);
            var sum = yard.Add(foot);
            Assert.That(sum.ConvertTo(LengthUnit.YARD), Is.EqualTo(1.0 + (1.0/3.0)).Within(1e-9));
        }
    }
}