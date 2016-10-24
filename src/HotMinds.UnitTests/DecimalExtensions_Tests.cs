using HotMinds.Utils;
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
            Assert.That(() => d.Trim(4, 3), // max == precision
                Throws.ArgumentException.With.Property("ParamName").EqualTo("scale"));
        }

        [Test]
        public void Trim_Common_Test()
        {
            var d = 123456789.987654321m;

            // only scale
            Assert.That(d.Trim(10), Is.EqualTo(123456789.987654321m));
            Assert.That(d.Trim(09), Is.EqualTo(123456789.987654321m));
            Assert.That(d.Trim(08), Is.EqualTo(123456789.98765432m));
            Assert.That(d.Trim(07), Is.EqualTo(123456789.9876543));
            Assert.That(d.Trim(06), Is.EqualTo(123456789.987654m));
            Assert.That(d.Trim(05), Is.EqualTo(123456789.98765m));
            Assert.That(d.Trim(04), Is.EqualTo(123456789.9876m));
            Assert.That(d.Trim(03), Is.EqualTo(123456789.987m));
            Assert.That(d.Trim(02), Is.EqualTo(123456789.98m));
            Assert.That(d.Trim(01), Is.EqualTo(123456789.9m));
            Assert.That(d.Trim(00), Is.EqualTo(123456789m));

            // only precision
            Assert.That(d.Trim(0, 10), Is.EqualTo(123456789m));
            Assert.That(d.Trim(0, 09), Is.EqualTo(123456789m));
            Assert.That(d.Trim(0, 08), Is.EqualTo(23456789m));
            Assert.That(d.Trim(0, 07), Is.EqualTo(3456789m));
            Assert.That(d.Trim(0, 06), Is.EqualTo(456789m));
            Assert.That(d.Trim(0, 05), Is.EqualTo(56789m));
            Assert.That(d.Trim(0, 04), Is.EqualTo(6789m));
            Assert.That(d.Trim(0, 03), Is.EqualTo(789m));
            Assert.That(d.Trim(0, 02), Is.EqualTo(89m));
            Assert.That(d.Trim(0, 01), Is.EqualTo(9m));

            // scale and precision
            Assert.That(d.Trim(01, 10), Is.EqualTo(123456789.9m));
            Assert.That(d.Trim(02, 10), Is.EqualTo(23456789.98m));
            Assert.That(d.Trim(03, 10), Is.EqualTo(3456789.987m));
            Assert.That(d.Trim(04, 10), Is.EqualTo(456789.9876m));
            Assert.That(d.Trim(05, 10), Is.EqualTo(56789.98765m));
            Assert.That(d.Trim(06, 10), Is.EqualTo(6789.987654m));
            Assert.That(d.Trim(07, 10), Is.EqualTo(789.9876543m));
            Assert.That(d.Trim(08, 10), Is.EqualTo(89.98765432m));
            Assert.That(d.Trim(09, 10), Is.EqualTo(9.987654321m));
            Assert.That(d.Trim(10, 10), Is.EqualTo(0.987654321m));
        }
    }
}
