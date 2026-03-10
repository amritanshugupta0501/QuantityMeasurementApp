using NUnit.Framework;
using System;
using QuantityMeasurementModel;   
using QuantityMeasurementService; 

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityWeightTests
    {
        private readonly IMeasurable<WeightUnit> _converter = WeightConverter.Instance;
        private const double Epsilon = 1e-6;

        // Comparison Tests 
        [Test]
        public void Equals_KilogramsAndGrams_SameMagnitude_ReturnsTrue()
        {
            var kg = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM, _converter);
            var grams = new Quantity<WeightUnit>(1000.0, WeightUnit.GRAM, _converter);

            Assert.That(kg.Equals(grams), Is.True);
        }

        [Test]
        public void Equals_PoundsAndGrams_EquivalentValues_ReturnsTrue()
        {
            var pound = new Quantity<WeightUnit>(1.0, WeightUnit.POUND, _converter);
            var grams = new Quantity<WeightUnit>(453.592, WeightUnit.GRAM, _converter);

            Assert.That(pound.Equals(grams), Is.True);
        }

        // Arithmetic Tests 
        [Test]
        public void Add_KilogramAndGrams_ReturnsCorrectSumInKilograms()
        {
            var kg = new Quantity<WeightUnit>(1.0, WeightUnit.KILOGRAM, _converter);
            var grams = new Quantity<WeightUnit>(1000.0, WeightUnit.GRAM, _converter);
            
            var result = kg.Add(grams, WeightUnit.KILOGRAM);
            
            Assert.That(result.ConvertTo(WeightUnit.KILOGRAM), Is.EqualTo(2.0).Within(Epsilon));
        }

        [Test]
        public void Subtract_ImmutabilityCheck_OriginalValueShouldNotChange()
        {
            var w1 = new Quantity<WeightUnit>(10.0, WeightUnit.KILOGRAM, _converter);
            var w2 = new Quantity<WeightUnit>(5.0, WeightUnit.KILOGRAM, _converter);
            
            w1.Subtract(w2, WeightUnit.KILOGRAM);
            
            Assert.That(w1.ConvertTo(WeightUnit.KILOGRAM), Is.EqualTo(10.0));
        }

        [Test]
        public void Division_KilogramsAndGrams_ReturnsRatioOfOne()
        {
            var kg = new Quantity<WeightUnit>(2.0, WeightUnit.KILOGRAM, _converter);
            var grams = new Quantity<WeightUnit>(2000.0, WeightUnit.GRAM, _converter);

            var result = kg.Division(grams, WeightUnit.KILOGRAM);

            Assert.That(result.ConvertTo(WeightUnit.KILOGRAM), Is.EqualTo(1.0).Within(Epsilon));
        }

        // Validation Logic
        [Test]
        public void Arithmetic_NullOperand_ThrowsArgumentNullException()
        {
            var w1 = new Quantity<WeightUnit>(10.0, WeightUnit.KILOGRAM, _converter);
            
            Assert.Throws<ArgumentNullException>(() => w1.Subtract(null!, WeightUnit.KILOGRAM));
        }
    }
}