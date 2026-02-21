using NUnit.Framework;
using System;

namespace QuantityMeasurementApp.Tests
{
    // Test class to verify the functionality of FeetMeasurement and QuantityMeasurementServices
    [TestFixture]
    public class QuantityMeasurementTest
    {
        // Test to verify that two FeetMeasurement objects with equal values are equal
        [Test]
        public void TwoEqualFeetMeasurementsShouldBeEqual()
        {
            FeetMeasurement feet1 = new FeetMeasurement(5.0);
            FeetMeasurement feet2 = new FeetMeasurement(5.0);
            Assert.That(feet1.Equals(feet2), Is.True);
        }
        // Test to verify that two FeetMeasurement objects with decimal fractional values are equal or not
        [Test]
        public void TwoFractionalFeetMeasurementsShouldBeEqual()
        {
            FeetMeasurement feet1 = new FeetMeasurement(5.5);
            FeetMeasurement feet2 = new FeetMeasurement(5.6);
            Assert.That(feet1.Equals(feet2), Is.False);
        }
        // Test to verify that InvalidMeasurementException inherits from Exception
        [Test]
        public void InvalidMeasurementExceptionShouldBeAnException()
        {
            var exception = new InvalidMeasurementException("Test");
            Assert.That(exception, Is.TypeOf<InvalidMeasurementException>());
            Assert.That(exception, Is.InstanceOf<Exception>());
        }
    }
}
