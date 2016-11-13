using System;
using HotMinds.Extensions;
using NUnit.Framework;

namespace HotMinds.UnitTests
{
    [TestFixture]
    public class DecimalExtensions_Tests
    {
        [Test]
        public void Power_Test()
        {
            // 2
            Assert.That(2m.Power(-5), Is.EqualTo(0.03125m));
            Assert.That(2m.Power(-4), Is.EqualTo(0.0625m));
            Assert.That(2m.Power(-3), Is.EqualTo(0.125m));
            Assert.That(2m.Power(-2), Is.EqualTo(0.25m));
            Assert.That(2m.Power(-1), Is.EqualTo(0.5m));
            Assert.That(2m.Power(0), Is.EqualTo(1m));
            Assert.That(2m.Power(1), Is.EqualTo(2m));
            Assert.That(2m.Power(2), Is.EqualTo(4m));
            Assert.That(2m.Power(3), Is.EqualTo(8m));
            Assert.That(2m.Power(4), Is.EqualTo(16m));
            Assert.That(2m.Power(5), Is.EqualTo(32m));

            // 10
            Assert.That(10m.Power(-5), Is.EqualTo(0.00001m));
            Assert.That(10m.Power(-4), Is.EqualTo(0.0001m));
            Assert.That(10m.Power(-3), Is.EqualTo(0.001m));
            Assert.That(10m.Power(-2), Is.EqualTo(0.01m));
            Assert.That(10m.Power(-1), Is.EqualTo(0.1m));
            Assert.That(10m.Power(0), Is.EqualTo(1m));
            Assert.That(10m.Power(1), Is.EqualTo(10m));
            Assert.That(10m.Power(2), Is.EqualTo(100m));
            Assert.That(10m.Power(3), Is.EqualTo(1000m));
            Assert.That(10m.Power(4), Is.EqualTo(10000m));
            Assert.That(10m.Power(5), Is.EqualTo(100000m));
        }

        [Test]
        public void PowerOf10_Test()
        {
            Assert.That(() => DecimalExtensions.PowerOf10(-29), Throws.ArgumentException.With.Property("ParamName").EqualTo("pow"));
            Assert.That(() => DecimalExtensions.PowerOf10(29), Throws.ArgumentException.With.Property("ParamName").EqualTo("pow"));
            Assert.That(() => DecimalExtensions.PowerOf10(-100), Throws.ArgumentException.With.Property("ParamName").EqualTo("pow"));
            Assert.That(() => DecimalExtensions.PowerOf10(100), Throws.ArgumentException.With.Property("ParamName").EqualTo("pow"));

            for (var pow = -28; pow < 29; ++pow)
            {
                Assert.That(DecimalExtensions.PowerOf10(pow), Is.EqualTo(10m.Power(pow)));
            }
        }

        [Test]
        public void Trim_Exception_Test()
        {
            var d = 789.9786m;

            // precision
            Assert.That(() => d.Trim(0, -1), // min 1
                Throws.ArgumentException.With.Property("ParamName").EqualTo("precision"));
            Assert.That(() => d.Trim(0, 0),  // min 1
                Throws.ArgumentException.With.Property("ParamName").EqualTo("precision"));
            Assert.That(() => d.Trim(0, 30), // max 29
                Throws.ArgumentException.With.Property("ParamName").EqualTo("precision"));

            // scale
            Assert.That(() => d.Trim(-1, 1), // min 0
                Throws.ArgumentException.With.Property("ParamName").EqualTo("scale"));
            Assert.That(() => d.Trim(29), // scale < precision
                Throws.ArgumentException.With.Property("ParamName").EqualTo("scale"));
            Assert.That(() => d.Trim(3, 3), // scale < precision
                Throws.ArgumentException.With.Property("ParamName").EqualTo("scale"));
            Assert.That(() => d.Trim(4, 3), // scale < precision
                Throws.ArgumentException.With.Property("ParamName").EqualTo("scale"));
        }

        [Test]
        public void Trim_Common_Test()
        {
            var testData = new[]
            {
                // max value, no scale
                Tuple.Create(79228162514264337593543950335m, 79228162514264337593543950335m, 0, 29),
                Tuple.Create(79228162514264337593543950335m, 9228162514264337593543950335m, 0, 28),
                Tuple.Create(79228162514264337593543950335m, 14264337593543950335m, 0, 20),
                Tuple.Create(79228162514264337593543950335m, 3543950335m, 0, 10),
                Tuple.Create(79228162514264337593543950335m, 50335m, 0, 5),
                Tuple.Create(79228162514264337593543950335m, 5m, 0, 1),
                // max value, scale
                Tuple.Create(7.9228162514264337593543950335m, 7.9228162514264337593543950335m, 28, 29),
                Tuple.Create(7.9228162514264337593543950335m, 7.922816251426433759354395033m, 27, 29),
                Tuple.Create(7.9228162514264337593543950335m, 7.92281625142643375935m, 20, 29),
                Tuple.Create(7.9228162514264337593543950335m, 7.9228162514m, 10, 29),
                Tuple.Create(7.9228162514264337593543950335m, 7.92281m, 5, 29),
                Tuple.Create(7.9228162514264337593543950335m, 7.9m, 1, 29),
                // only scale, 0.(1)
                Tuple.Create(0.1111111111111111111111111111m, 0.1111111111111111111111111111m, 28, 29),
                Tuple.Create(0.1111111111111111111111111111m, 0.1111111111111111111111111m, 25, 29),
                Tuple.Create(0.1111111111111111111111111111m, 0.11111111111111111111m, 20, 29),
                Tuple.Create(0.1111111111111111111111111111m, 0.111111111111111m, 15, 29),
                Tuple.Create(0.1111111111111111111111111111m, 0.1111111111m, 10, 29),
                Tuple.Create(0.1111111111111111111111111111m, 0.11111m, 5, 29),
                Tuple.Create(0.1111111111111111111111111111m, 0.1m, 1, 29),
                Tuple.Create(0.1111111111111111111111111111m, 0m, 0, 29),
                // only scale, 0.(9)
                Tuple.Create(0.9999999999999999999999999999m, 0.9999999999999999999999999999m, 28, 29),
                Tuple.Create(0.9999999999999999999999999999m, 0.9999999999999999999999999m, 25, 29),
                Tuple.Create(0.9999999999999999999999999999m, 0.99999999999999999999m, 20, 29),
                Tuple.Create(0.9999999999999999999999999999m, 0.999999999999999m, 15, 29),
                Tuple.Create(0.9999999999999999999999999999m, 0.9999999999m, 10, 29),
                Tuple.Create(0.9999999999999999999999999999m, 0.99999m, 5, 29),
                Tuple.Create(0.9999999999999999999999999999m, 0.9m, 1, 29),
                Tuple.Create(0.9999999999999999999999999999m, 0m, 0, 29),
                // different
                Tuple.Create(79228162514264.337593543950335m, 79228162514264m, 0, 29),
                Tuple.Create(79228162514264.337593543950335m, 14264.33759m, 5, 10),
                Tuple.Create(79228162514264.337593543950335m, 4.337m, 3, 4),
                Tuple.Create(79228162514264.337593543950335m, 4.337593543950335m, 20, 21),
                Tuple.Create(79228162514264.337593543950335m, 14264.3375935439m, 10, 15),
                Tuple.Create(79228162514264.337593543950335m, 162514264.3m, 1, 10),
                Tuple.Create(79228162514264.337593543950335m, 79228162514264.33m, 2, 20),
                Tuple.Create(79228162514264.337593543950335m, 4.337593543950335m, 25, 26),
                Tuple.Create(79228162514264.337593543950335m, 79228162514264.3375m, 4, 18),
                Tuple.Create(79228162514264.337593543950335m, 264.3375935m, 7, 10),
            };

            foreach (var t in testData)
            {
                Assert.That(t.Item1.Trim(t.Item3, t.Item4), Is.EqualTo(t.Item2), $"Scale: {t.Item3}, Precision: {t.Item4}");
                Assert.That(decimal.Negate(t.Item1).Trim(t.Item3, t.Item4), Is.EqualTo(decimal.Negate(t.Item2)));
            }
        }
    }
}
