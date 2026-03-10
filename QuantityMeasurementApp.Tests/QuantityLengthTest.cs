using NUnit.Framework;
using System;
using QuantityMeasurementModel;   
using QuantityMeasurementService; 

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
            
            Assert.That(foot, Is.EqualTo(inch)); 
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

            TestDelegate action = op switch
            {
                ArithmeticOperation.Add => () => q.Add(null!, LengthUnit.INCH),
                ArithmeticOperation.Subtract => () => q.Subtract(null!, LengthUnit.INCH),
                ArithmeticOperation.Divide => () => q.Division(null!, LengthUnit.INCH),
                _ => throw new ArgumentOutOfRangeException(nameof(op), "Invalid operation")
            };

            Assert.Throws<ArgumentNullException>(action);
        }
    }
}