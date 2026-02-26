using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityWeightTests
    {
        [Test]
        public void Given1KilogramAnd1KilogramWhenComparedShouldReturnTrue()
        {
            var kg1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var kg2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            Assert.That(kg1.Equals(kg2), Is.True);
        }
        [Test]
        public void Given1KilogramAnd1000GramsWhenComparedShouldReturnTrue()
        {
            var kg = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var grams = new QuantityWeight(1000.0, WeightUnit.GRAM);
            Assert.That(kg.Equals(grams), Is.True);
        }
        [Test]
        public void Given2PoundsAnd2PoundsWhenComparedShouldReturnTrue()
        {
            var pound1 = new QuantityWeight(2.0, WeightUnit.POUND);
            var pound2 = new QuantityWeight(2.0, WeightUnit.POUND);
            Assert.That(pound1.Equals(pound2), Is.True);
        }
        [Test]
        public void Given500GramsAndHalfKilogramWhenComparedShouldReturnTrue()
        {
            var grams = new QuantityWeight(500.0, WeightUnit.GRAM);
            var kg = new QuantityWeight(0.5, WeightUnit.KILOGRAM);
            Assert.That(grams.Equals(kg), Is.True);
        }
        [Test]
        public void Given1PoundAnd453Point592GramsWhenComparedShouldReturnTrue()
        {
            var pound = new QuantityWeight(1.0, WeightUnit.POUND);
            var grams = new QuantityWeight(453.592, WeightUnit.GRAM);
            Assert.That(pound.Equals(grams), Is.True);
        }
        [Test]
        public void GivenNullMeasurementWhenComparedShouldReturnFalse()
        {
            var kg = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            Assert.That(kg.Equals(null), Is.False);
        }
        [Test]
        public void GivenSameReferenceWhenComparedShouldReturnTrue()
        {
            var kg = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            Assert.That(kg.Equals(kg), Is.True);
        }
        [Test]
        public void InstanceConvert_1KilogramToGrams_Returns1000()
        {
            var kg = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            double result = kg.ConvertTo(WeightUnit.GRAM);
            Assert.That(result, Is.EqualTo(1000.0));
        }
        [Test]
        public void StaticConvert_2PoundsToKilograms_ReturnsApproximately0Point907184()
        {
            double result = QuantityWeight.Convert(2.0, WeightUnit.POUND, WeightUnit.KILOGRAM);
            Assert.That(result, Is.EqualTo(0.907184).Within(1e-5));
        }
        [Test]
        public void InstanceConvert_500GramsToPounds_ReturnsApproximately1Point10231()
        {
            var grams = new QuantityWeight(500.0, WeightUnit.GRAM);
            double result = grams.ConvertTo(WeightUnit.POUND);
            Assert.That(result, Is.EqualTo(1.10231).Within(1e-5));
        }
        [Test]
        public void StaticConvert_0KilogramsToGrams_Returns0()
        {
            double result = QuantityWeight.Convert(0.0, WeightUnit.KILOGRAM, WeightUnit.GRAM);
            Assert.That(result, Is.EqualTo(0.0));
        }
        [Test]
        public void Convert_InfiniteValue_ThrowsInvalidMeasurementException()
        {
            Assert.Throws<InvalidMeasurementException>(() =>
                QuantityWeight.Convert(double.PositiveInfinity, WeightUnit.KILOGRAM, WeightUnit.POUND));
        }
        [Test]
        public void Add_1KilogramAnd2Kilograms_Returns3Kilograms()
        {
            var first = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var second = new QuantityWeight(2.0, WeightUnit.KILOGRAM);
            var result = first.Add(second);
            Assert.That(result, Is.EqualTo(new QuantityWeight(3.0, WeightUnit.KILOGRAM)));
        }
        [Test]
        public void Add_1KilogramAnd1000Grams_Returns2Kilograms()
        {
            var kg = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var grams = new QuantityWeight(1000.0, WeightUnit.GRAM);
            var result = kg.Add(grams);
            Assert.That(result, Is.EqualTo(new QuantityWeight(2.0, WeightUnit.KILOGRAM)));
        }
        [Test]
        public void Add_500GramsAndHalfKilogram_Returns1000Grams()
        {
            var grams = new QuantityWeight(500.0, WeightUnit.GRAM);
            var kg = new QuantityWeight(0.5, WeightUnit.KILOGRAM);
            var result = grams.Add(kg);
            Assert.That(result, Is.EqualTo(new QuantityWeight(1000.0, WeightUnit.GRAM)));
        }
        [Test]
        public void Add_1KilogramAnd1000GramsWithTargetGram_Returns2000Grams()
        {
            var kg = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
            var grams = new QuantityWeight(1000.0, WeightUnit.GRAM);
            var result = kg.Add(grams, WeightUnit.GRAM);
            Assert.That(result, Is.EqualTo(new QuantityWeight(2000.0, WeightUnit.GRAM)));
        }
        [Test]
        public void Add_1PoundAnd453Point592GramsWithTargetPound_Returns2Pounds()
        {
            var pound = new QuantityWeight(1.0, WeightUnit.POUND);
            var grams = new QuantityWeight(453.592, WeightUnit.GRAM);
            var result = pound.Add(grams, WeightUnit.POUND);
            Assert.That(result.ConvertTo(WeightUnit.POUND), Is.EqualTo(2.0).Within(1e-5));
        }
        [Test]
        public void Add_2KilogramsAnd4PoundsWithTargetKilogram_ReturnsApproximately3Point814()
        {
            var kg = new QuantityWeight(2.0, WeightUnit.KILOGRAM);
            var pounds = new QuantityWeight(4.0, WeightUnit.POUND);
            var result = kg.Add(pounds, WeightUnit.KILOGRAM);
            Assert.That(result.ConvertTo(WeightUnit.KILOGRAM), Is.EqualTo(3.814369).Within(1e-5));
        }
    }
}