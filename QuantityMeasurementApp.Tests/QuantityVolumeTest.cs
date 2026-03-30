using NUnit.Framework;
using QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class QuantityVolumeTests
    {
        private readonly IUnitConverter<VolumeUnit> _converter = VolumeConverter.Instance;
        private const double Epsilon = 1e-5;
        // Comparison Tests
        [Test]
        public void Equals_LitreAndMillilitre_SameMagnitude_ReturnsTrue()
        {
            var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE, _converter);
            var ml = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE, _converter);
            Assert.That(litre.Equals(ml), Is.True);
        }
        [Test]
        public void Equals_GallonAndLitre_EquivalentValues_ReturnsTrue()
        {
            var gallon = new Quantity<VolumeUnit>(1.0, VolumeUnit.GALLON, _converter);
            var litre = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE, _converter);
            Assert.That(gallon.Equals(litre), Is.True);
        }
        // Arithmetic Tests 
        [Test]
        public void Add_LitreAndMillilitre_ReturnsSumInLitres()
        {
            var litre = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE, _converter);
            var ml = new Quantity<VolumeUnit>(1000.0, VolumeUnit.MILLILITRE, _converter);

            var result = litre.Add(ml, VolumeUnit.LITRE);

            Assert.That(result.ConvertTo(VolumeUnit.LITRE), Is.EqualTo(2.0).Within(Epsilon));
        }
        [Test]
        public void Subtract_ChainedOperations_ReturnsCorrectRemainingVolume()
        {
            var q1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.LITRE, _converter);
            var q2 = new Quantity<VolumeUnit>(2.0, VolumeUnit.LITRE, _converter);
            var q3 = new Quantity<VolumeUnit>(1.0, VolumeUnit.LITRE, _converter);
            var result = q1.Subtract(q2, VolumeUnit.LITRE).Subtract(q3, VolumeUnit.LITRE);

            Assert.That(result.ConvertTo(VolumeUnit.LITRE), Is.EqualTo(7.0).Within(Epsilon));
        }
        [Test]
        public void Division_ByZeroValue_ReturnsInfinity()
        {
            var v1 = new Quantity<VolumeUnit>(10.0, VolumeUnit.LITRE, _converter);
            var v2 = new Quantity<VolumeUnit>(0.0, VolumeUnit.LITRE, _converter);

            var result = v1.Division(v2, VolumeUnit.LITRE);

            Assert.That(double.IsInfinity(result.ConvertTo(VolumeUnit.LITRE)), Is.True);
        }
        // Validation Logic
        [Test]
        public void Arithmetic_NullOtherQuantity_ThrowsArgumentException()
        {
            var volume = new Quantity<VolumeUnit>(5.0, VolumeUnit.LITRE, _converter);
            Assert.Throws<ArgumentNullException>(() => volume.Add(null!, VolumeUnit.LITRE));
        }
    }
}