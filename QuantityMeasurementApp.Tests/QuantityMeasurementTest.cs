using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityMeasurementTests
    {
        [Test]
        public void Given0InchesAnd0InchesWhenComparedShouldReturnTrue()
        {
            var inches1 = new QuantityMeasurement(0.0, MeasurementUnit.INCH);
            var inches2 = new QuantityMeasurement(0.0, MeasurementUnit.INCH);
            Assert.That(inches1.Equals(inches2), Is.True);
        }
        [Test]
        public void Given1InchAnd2InchesWhenComparedShouldReturnFalse()
        {
            var inches1 = new QuantityMeasurement(1.0, MeasurementUnit.INCH);
            var inches2 = new QuantityMeasurement(2.0, MeasurementUnit.INCH);
            Assert.That(inches1.Equals(inches2), Is.False);
        }
        [Test]
        public void Given1FootAnd1FootWhenComparedShouldReturnTrue()
        {
            var feet1 = new QuantityMeasurement(1.0, MeasurementUnit.FEET);
            var feet2 = new QuantityMeasurement(1.0, MeasurementUnit.FEET);
            Assert.That(feet1.Equals(feet2), Is.True);
        }
        [Test]
        public void Given1FootAnd12InchesWhenComparedShouldReturnTrue()
        {
            var foot = new QuantityMeasurement(1.0, MeasurementUnit.FEET);
            var inches = new QuantityMeasurement(12.0, MeasurementUnit.INCH);
            Assert.That(foot.Equals(inches), Is.True);
        }
        [Test]
        public void Given12InchesAnd1FootWhenComparedShouldReturnTrue()
        {
            var inches = new QuantityMeasurement(12.0, MeasurementUnit.INCH);
            var foot = new QuantityMeasurement(1.0, MeasurementUnit.FEET);
            Assert.That(inches.Equals(foot), Is.True);
        }
        [Test]
        public void Given1FootAnd1InchWhenComparedShouldReturnFalse()
        {
            var foot = new QuantityMeasurement(1.0, MeasurementUnit.FEET);
            var inch = new QuantityMeasurement(1.0, MeasurementUnit.INCH);
            
            Assert.That(foot.Equals(inch), Is.False);
        }
        [Test]
        public void GivenNullMeasurementWhenComparedShouldReturnFalse()
        {
            var foot = new QuantityMeasurement(1.0, MeasurementUnit.FEET);
            Assert.That(foot.Equals(null), Is.False);
        }
        [Test]
        public void GivenSameReferenceWhenComparedShouldReturnTrue()
        {
            var foot = new QuantityMeasurement(1.0, MeasurementUnit.FEET);
            Assert.That(foot.Equals(foot), Is.True);
        }
        [Test]
        public void Given1YardAnd36InchesWhenComparedShouldReturnTrue()
        {
            var yard = new QuantityMeasurement(1.0, MeasurementUnit.YARD);
            var inches = new QuantityMeasurement(36.0, MeasurementUnit.INCH);
            Assert.That(yard.Equals(inches), Is.True);
        }

        [Test]
        public void Given1YardAnd3FeetWhenComparedShouldReturnTrue()
        {
            var yard = new QuantityMeasurement(1.0, MeasurementUnit.YARD);
            var feet = new QuantityMeasurement(3.0, MeasurementUnit.FEET);
            Assert.That(yard.Equals(feet), Is.True);
        }

        [Test]
        public void Given2Point54CentimetresAnd1InchWhenComparedShouldReturnTrue()
        {
            var cm = new QuantityMeasurement(2.54, MeasurementUnit.CENTIMETRE);
            var inch = new QuantityMeasurement(1.0, MeasurementUnit.INCH);
            Assert.That(cm.Equals(inch), Is.True);
        }

        [Test]
        public void Given1CentimetreAnd1InchWhenComparedShouldReturnFalse()
        {
            var cm = new QuantityMeasurement(1.0, MeasurementUnit.CENTIMETRE);
            var inch = new QuantityMeasurement(1.0, MeasurementUnit.INCH);
            Assert.That(cm.Equals(inch), Is.False);
        }
    }
}