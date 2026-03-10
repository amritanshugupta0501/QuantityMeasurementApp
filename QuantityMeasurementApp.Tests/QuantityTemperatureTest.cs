using NUnit.Framework;
using System;
using QuantityMeasurementModel;   
using QuantityMeasurementService; 

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityTemperatureTests
    {
        private readonly IMeasurable<TemperatureUnit> _converter = TemperatureConverter.Instance;
        private const double Epsilon = 1e-5;

        // Comparison & Equality Tests 
        [Test]
        public void Equals_CelsiusAndFahrenheit_FreezingPoint_ReturnsTrue()
        {
            var celsius = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS, _converter);
            var fahrenheit = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT, _converter);
            
            Assert.That(celsius.Equals(fahrenheit), Is.True);
        }

        [Test]
        public void Equals_KelvinAndCelsius_AbsoluteZeroOffset_ReturnsTrue()
        {
            var kelvin = new Quantity<TemperatureUnit>(273.15, TemperatureUnit.KELVIN, _converter);
            var celsius = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS, _converter);
            
            Assert.That(kelvin.Equals(celsius), Is.True);
        }

        [Test]
        public void Equals_FahrenheitAndCelsius_BoilingPoint_ReturnsTrue()
        {
            var fahrenheit = new Quantity<TemperatureUnit>(212.0, TemperatureUnit.FAHRENHEIT, _converter);
            var celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS, _converter);
            
            Assert.That(fahrenheit.Equals(celsius), Is.True);
        }

        
        
        // Conversion Tests 
        [Test]
        public void ConvertTo_CelsiusToFahrenheit_ReturnsCorrectValue()
        {
            var celsius = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS, _converter);
            double result = celsius.ConvertTo(TemperatureUnit.FAHRENHEIT);
            
            Assert.That(result, Is.EqualTo(212.0).Within(Epsilon));
        }

        [Test]
        public void ConvertTo_FahrenheitToCelsius_ReturnsCorrectValue()
        {
            var fahrenheit = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT, _converter);
            double result = fahrenheit.ConvertTo(TemperatureUnit.CELSIUS);
            
            Assert.That(result, Is.EqualTo(0.0).Within(Epsilon));
        }

        [Test]
        public void ConvertTo_CelsiusToFahrenheit_EqualPoint_ReturnsMinusForty()
        {
            var celsius = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.CELSIUS, _converter);
            double result = celsius.ConvertTo(TemperatureUnit.FAHRENHEIT);
            
            Assert.That(result, Is.EqualTo(-40.0).Within(Epsilon));
        }

        // Unsupported Operations 
        [Test]
        public void Add_Temperatures_ThrowsUnsupportedOperationException()
        {
            var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS, _converter);
            var t2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS, _converter);

            Assert.Throws<InvalidMeasurementException>(() => t1.Add(t2, TemperatureUnit.CELSIUS),
                "Temperature should not support addition per UC14.");
        }

        [Test]
        public void Subtract_Temperatures_ThrowsUnsupportedOperationException()
        {
            var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS, _converter);
            var t2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS, _converter);

            Assert.Throws<InvalidMeasurementException>(() => t1.Subtract(t2, TemperatureUnit.CELSIUS));
        }

        [Test]
        public void Divide_Temperatures_ThrowsUnsupportedOperationException()
        {
            var t1 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS, _converter);
            var t2 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS, _converter);

            Assert.Throws<InvalidMeasurementException>(() => t1.Division(t2, TemperatureUnit.CELSIUS));
        }
    }
}