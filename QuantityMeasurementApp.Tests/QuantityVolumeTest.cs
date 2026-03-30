using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityVolumeTests
    {
        private readonly double _epsilon = 1e-5;
        // Equality Tests
        [Test]
        public void Given1LitreAnd1Litre_WhenCompared_ShouldReturnTrue()
        {
            var l1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE, VolumeConverter.Instance);
            var l2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE, VolumeConverter.Instance);
            Assert.That(l1.Equals(l2), Is.True);
        }
        [Test]
        public void Given1LitreAnd1000Millilitres_WhenCompared_ShouldReturnTrue()
        {
            var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE, VolumeConverter.Instance);
            var ml = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE, VolumeConverter.Instance);
            Assert.That(litre.Equals(ml), Is.True);
        }
        [Test]
        public void Given1GallonAnd1Gallon_WhenCompared_ShouldReturnTrue()
        {
            var g1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.GALLON, VolumeConverter.Instance);
            var g2 = new Quantity<VolumeUnit>(1.0, VolumeUnit.GALLON, VolumeConverter.Instance);
            Assert.That(g1.Equals(g2), Is.True);
        }
        [Test]
        public void Given1LitreAndApprox0264Gallon_WhenCompared_ShouldReturnTrue()
        {
            var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE, VolumeConverter.Instance);
            var gallon = new Quantity<VolumeUnit>(0.264172, VolumeUnit.GALLON, VolumeConverter.Instance);
            Assert.That(litre.Equals(gallon), Is.True);
        }
        [Test]
        public void Given500MillilitresAndPoint5Litres_WhenCompared_ShouldReturnTrue()
        {
            var ml = new Quantity<VolumeUnit>(500.0, VolumeUnit.MILLILITRE, VolumeConverter.Instance);
            var litre = new Quantity<VolumeUnit>(0.5, VolumeUnit.LITRE, VolumeConverter.Instance);
            Assert.That(ml.Equals(litre), Is.True);
        }
        [Test]
        public void Given3Point785LitresAnd1Gallon_WhenCompared_ShouldReturnTrue()
        {
            var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE, VolumeConverter.Instance);
            var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.GALLON, VolumeConverter.Instance);
            Assert.That(litre.Equals(gallon), Is.True);
        }
        // Unit Conversion Tests 
        [Test]
        public void Convert_1LitreToMillilitre_Returns1000()
        {
            var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE, VolumeConverter.Instance);
            Assert.That(litre.ConvertTo(VolumeUnit.MILLILITRE), Is.EqualTo(1000.0));
        }
        [Test]
        public void Convert_2GallonsToLitre_ReturnsApprox7Point57()
        {
            var gallon = new Quantity<VolumeUnit>(2.0, VolumeUnit.GALLON, VolumeConverter.Instance);
            Assert.That(gallon.ConvertTo(VolumeUnit.LITRE), Is.EqualTo(7.57082).Within(_epsilon));
        }
        [Test]
        public void Convert_500MillilitresToGallon_ReturnsApprox0Point132()
        {
            var ml = new Quantity<VolumeUnit>(500.0, VolumeUnit.MILLILITRE, VolumeConverter.Instance);
            Assert.That(ml.ConvertTo(VolumeUnit.GALLON), Is.EqualTo(0.132086).Within(_epsilon));
        }
        [Test]
        public void Convert_0LitresToMillilitre_Returns0()
        {
            var litre = new Quantity<VolumeUnit>(0.0, VolumeUnit.LITRE, VolumeConverter.Instance);
            Assert.That(litre.ConvertTo(VolumeUnit.MILLILITRE), Is.EqualTo(0.0));
        }
        // Addition Tests
        [Test]
        public void Add_1LitreAnd2Litres_Returns3Litres()
        {
            var l1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE, VolumeConverter.Instance);
            var l2 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE, VolumeConverter.Instance);
            var result = l1.Add(l2, VolumeUnit.LITRE);
            Assert.That(result.ConvertTo(VolumeUnit.LITRE), Is.EqualTo(3.0));
        }

        [Test]
        public void Add_1LitreAnd1000Millilitres_Returns2Litres()
        {
            var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE, VolumeConverter.Instance);
            var ml = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE, VolumeConverter.Instance);
            var result = litre.Add(ml, VolumeUnit.LITRE);
            Assert.That(result.ConvertTo(VolumeUnit.LITRE), Is.EqualTo(2.0));
        }

        [Test]
        public void Add_1GallonAnd3Point785LitresWithTargetGallon_Returns2Gallons()
        {
            var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.GALLON, VolumeConverter.Instance);
            var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE, VolumeConverter.Instance);
            var result = gallon.Add(litre, VolumeUnit.GALLON);
            Assert.That(result.ConvertTo(VolumeUnit.GALLON), Is.EqualTo(2.0).Within(_epsilon));
        }

        [Test]
        public void Add_2LitresAnd4GallonsWithTargetLitre_ReturnsApprox17Point14()
        {
            var litre = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE, VolumeConverter.Instance);
            var gallon = new Quantity<VolumeUnit>(4.0, VolumeUnit.GALLON, VolumeConverter.Instance);
            var result = litre.Add(gallon, VolumeUnit.LITRE);
            Assert.That(result.ConvertTo(VolumeUnit.LITRE), Is.EqualTo(17.14164).Within(_epsilon));
        }
    }
}