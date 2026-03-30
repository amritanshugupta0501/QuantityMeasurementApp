using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityWeightTests
    {
        [Test]
        public void Given1KilogramAnd1000GramsWhenComparedShouldReturnTrue()
        {
            var kg = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            var grams = new Quantity<WeightUnit>(1000.0, WeightUnit.GRAM, WeightConverter.Instance);
            Assert.That(kg.Equals(grams), Is.True);
        }

        [Test]
        public void Given1PoundAnd453Point592GramsWhenComparedShouldReturnTrue()
        {
            var pound = new Quantity<WeightUnit>(1.0, WeightUnit.POUND, WeightConverter.Instance);
            var grams = new Quantity<WeightUnit>(453.592, WeightUnit.GRAM, WeightConverter.Instance);
            Assert.That(pound.Equals(grams), Is.True);
        }

        [Test]
        public void InstanceConvert_1KilogramToGrams_Returns1000()
        {
            var kg = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            double result = kg.ConvertTo(WeightUnit.GRAM);
            Assert.That(result, Is.EqualTo(1000.0));
        }

        [Test]
        public void Add_1KilogramAnd1000Grams_Returns2Kilograms()
        {
            var kg = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            var grams = new Quantity<WeightUnit>(1000.0, WeightUnit.GRAM, WeightConverter.Instance);
            var result = kg.Add(grams, WeightUnit.KILOGRAM);
            Assert.That(result, Is.EqualTo(new Quantity<WeightUnit>(2.0, WeightUnit.KILOGRAM, WeightConverter.Instance)));
        }

        [Test]
        public void Add_2KilogramsAnd4PoundsWithTargetKilogram_ReturnsApproximately3Point814()
        {
            var kg = new Quantity<WeightUnit>(2.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            var pounds = new Quantity<WeightUnit>(4.0, WeightUnit.POUND, WeightConverter.Instance);
            var result = kg.Add(pounds, WeightUnit.KILOGRAM);
            Assert.That(result.ConvertTo(WeightUnit.KILOGRAM), Is.EqualTo(3.814369).Within(1e-5));
        }
        [Test]
        public void testSubtraction_WithNegativeValues()
        {
            var w1 = new Quantity<WeightUnit>(5.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            var w2 = new Quantity<WeightUnit>(-2.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            var result = w1.Subtract(w2, WeightUnit.KILOGRAM);
            Assert.That(result.ConvertTo(WeightUnit.KILOGRAM), Is.EqualTo(7.0));
        }
        [Test]
        public void testSubtraction_Immutability()
        {
            var w1 = new Quantity<WeightUnit>(10.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            var w2 = new Quantity<WeightUnit>(5.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            w1.Subtract(w2, WeightUnit.KILOGRAM);
            Assert.That(w1.ConvertTo(WeightUnit.KILOGRAM), Is.EqualTo(10.0));
        }
        [Test]
        public void testSubtraction_NullOperand()
        {
            var w1 = new Quantity<WeightUnit>(10.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            Assert.Throws<ArgumentNullException>(() => w1.Subtract(null, WeightUnit.KILOGRAM));
        }
        [Test]
        public void testDivision_CrossUnit_KilogramDividedByGram()
        {
            var kg = new Quantity<WeightUnit>(2.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            var grams = new Quantity<WeightUnit>(2000.0, WeightUnit.GRAM, WeightConverter.Instance);

            var result = kg.Division(grams, WeightUnit.KILOGRAM);

            Assert.That(result.ConvertTo(WeightUnit.KILOGRAM), Is.EqualTo(1.0));
        }
        [Test]
        public void testDivision_WithSmallRatio()
        {
            var w1 = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM, WeightConverter.Instance);
            var w2 = new Quantity<WeightUnit>(1e6, WeightUnit.KILOGRAM, WeightConverter.Instance);

            var result = w1.Division(w2, WeightUnit.GRAM);

            Assert.That(result.ConvertTo(WeightUnit.GRAM), Is.EqualTo(1e-6).Within(1e-9));
        }
    }
}