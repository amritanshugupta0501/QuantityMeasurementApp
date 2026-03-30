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
            var inches1 = new Quantity<LengthUnit>(0.0, LengthUnit.INCH, LengthConverter.Instance);
            var inches2 = new Quantity<LengthUnit>(0.0, LengthUnit.INCH, LengthConverter.Instance);
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

        [Test]
        public void Add_OneFootAndTwelveInches_ReturnsTwoFeet()
        {
            var first = new Quantity<LengthUnit>(1.0, LengthUnit.FEET, LengthConverter.Instance);
            var second = new Quantity<LengthUnit>(12.0, LengthUnit.INCH, LengthConverter.Instance);
            // We use the target unit overload
            var result = first.Add(second, LengthUnit.FEET);
            Assert.That(result, Is.EqualTo(new Quantity<LengthUnit>(2.0, LengthUnit.FEET, LengthConverter.Instance)));
        }

        [Test]
        public void Add_OneYardAndOneFoot_ReturnsOnePointThreeThreeYards()
        {
            var yard = new Quantity<LengthUnit>(1.0, LengthUnit.YARD, LengthConverter.Instance);
            var foot = new Quantity<LengthUnit>(1.0, LengthUnit.FEET, LengthConverter.Instance);
            var sum = yard.Add(foot, LengthUnit.YARD);
            Assert.That(sum.ConvertTo(LengthUnit.YARD), Is.EqualTo(1.0 + (1.0 / 3.0)).Within(1e-9));
        }
        [Test]
        public void testSubtraction_SameUnit_FeetMinusFeet()
        {
            var f1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET, LengthConverter.Instance);
            var f2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET, LengthConverter.Instance);
            var result = f1.Subtract(f2, LengthUnit.FEET);
            Assert.That(result, Is.EqualTo(new Quantity<LengthUnit>(5.0, LengthUnit.FEET, LengthConverter.Instance)));
        }
        [Test]
        public void testSubtraction_CrossUnit_FeetMinusInches()
        {
            var feet = new Quantity<LengthUnit>(10.0, LengthUnit.FEET, LengthConverter.Instance);
            var inches = new Quantity<LengthUnit>(6.0, LengthUnit.INCH, LengthConverter.Instance);
            var result = feet.Subtract(inches, LengthUnit.FEET);
            Assert.That(result.ConvertTo(LengthUnit.FEET), Is.EqualTo(9.5));
        }
        [Test]
        public void testSubtraction_Immutability()
        {
            var f1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET, LengthConverter.Instance);
            var f2 = new Quantity<LengthUnit>(5.0, LengthUnit.FEET, LengthConverter.Instance);
            f1.Subtract(f2, LengthUnit.FEET);
            Assert.That(f1.ConvertTo(LengthUnit.FEET), Is.EqualTo(10.0));
        }
        [Test]
        public void testDivision_CrossUnit_FeetDividedByInches()
        {
            var inches = new Quantity<LengthUnit>(24.0, LengthUnit.INCH, LengthConverter.Instance);
            var feet = new Quantity<LengthUnit>(2.0, LengthUnit.FEET, LengthConverter.Instance);
            var ratio = inches.Division(feet, LengthUnit.INCH);
            Assert.That(ratio.ConvertTo(LengthUnit.INCH), Is.EqualTo(1.0));
        }
    }
}