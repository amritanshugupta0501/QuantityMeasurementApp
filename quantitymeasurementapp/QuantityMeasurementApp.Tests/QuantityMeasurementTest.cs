using NUnit.Framework;
using System;

namespace QuantityMeasurementApp.Tests
{
    // Test class to verify the functionality of InchesMeasurement, FeetMeasurement and QuantityMeasurementServices
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
        // Test to verify that two InchesMeasurement objects with equal values are equal
        [Test]
        public void TwoEqualInchesMeasurementsShouldBeEqual()
        {
            InchesMeasurement feet1 = new InchesMeasurement(9.0);
            InchesMeasurement feet2 = new InchesMeasurement(9.0);
            Assert.That(feet1.Equals(feet2), Is.True);
        }
        // Test to verify that two InchesMeasurement objects with decimal fractional values are equal or not
        [Test]
        public void TwoFractionalInchesMeasurementsShouldBeEqual()
        {
            InchesMeasurement feet1 = new InchesMeasurement(7.5);
            InchesMeasurement feet2 = new InchesMeasurement(5.6);
            Assert.That(feet1.Equals(feet2), Is.False);
        }
    }
}
