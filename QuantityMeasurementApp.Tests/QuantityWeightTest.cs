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
    }
}