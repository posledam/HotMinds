using System.Globalization;
using HotMinds.Strings;
using NUnit.Framework;

namespace HotMinds.UnitTests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class NumberStringExtensions_Tests
    {
        [Test]
        public void ToRaw_Null()
        {
            Assert.AreEqual(null, ((int?)null).ToRaw());
            Assert.AreEqual(null, ((long?)null).ToRaw());
            Assert.AreEqual(null, ((decimal?)null).ToRaw());
            Assert.AreEqual(null, ((float?)null).ToRaw());
            Assert.AreEqual(null, ((double?)null).ToRaw());
        }

        [Test]
        public void ToRaw_Int32()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");

            Assert.AreEqual("0", 0.ToRaw());
            Assert.AreEqual("2147483647", int.MaxValue.ToRaw());
            Assert.AreEqual("-2147483648", int.MinValue.ToRaw());
            Assert.AreEqual("0", ((int?)0).ToRaw());
            Assert.AreEqual("2147483647", ((int?)int.MaxValue).ToRaw());
            Assert.AreEqual("-2147483648", ((int?)int.MinValue).ToRaw());
        }

        [Test]
        public void ToRaw_Int64()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");

            Assert.AreEqual("0", 0L.ToRaw());
            Assert.AreEqual("9223372036854775807", long.MaxValue.ToRaw());
            Assert.AreEqual("-9223372036854775808", long.MinValue.ToRaw());
            Assert.AreEqual("0", ((long?)0L).ToRaw());
            Assert.AreEqual("9223372036854775807", ((long?)long.MaxValue).ToRaw());
            Assert.AreEqual("-9223372036854775808", ((long?)long.MinValue).ToRaw());
        }

        [Test]
        public void ToRaw_Decimal()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");

            Assert.AreEqual("0", 0m.ToRaw());
            Assert.AreEqual("79228162514264337593543950335", decimal.MaxValue.ToRaw());
            Assert.AreEqual("-79228162514264337593543950335", decimal.MinValue.ToRaw());
            Assert.AreEqual("0", ((decimal?)0m).ToRaw());
            Assert.AreEqual("79228162514264337593543950335", ((decimal?)decimal.MaxValue).ToRaw());
            Assert.AreEqual("-79228162514264337593543950335", ((decimal?)decimal.MinValue).ToRaw());
        }

        [Test]
        public void ToRaw_Single()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");

            Assert.AreEqual("0", 0f.ToRaw());
            Assert.AreEqual("3.402823E+38", float.MaxValue.ToRaw());
            Assert.AreEqual("-3.402823E+38", float.MinValue.ToRaw());
            Assert.AreEqual("3.141593", (3.1415926f).ToRaw()); // усечение для float!
            Assert.AreEqual("-3.141593", (-3.1415926f).ToRaw()); // усечение для float!
            Assert.AreEqual("0", ((float?)0f).ToRaw());
            Assert.AreEqual("3.402823E+38", ((float?)float.MaxValue).ToRaw());
            Assert.AreEqual("-3.402823E+38", ((float?)float.MinValue).ToRaw());
            Assert.AreEqual("3.141593", ((float?)(3.1415926f)).ToRaw()); // усечение для float!
            Assert.AreEqual("-3.141593", ((float?)(-3.1415926f)).ToRaw()); // усечение для float!
        }

        [Test]
        public void ToRaw_Double()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");

            Assert.AreEqual("0", 0d.ToRaw());
            Assert.AreEqual("1.79769313486232E+308", double.MaxValue.ToRaw());
            Assert.AreEqual("-1.79769313486232E+308", double.MinValue.ToRaw());
            Assert.AreEqual("3.1415926", (3.1415926d).ToRaw());
            Assert.AreEqual("-3.1415926", (-3.1415926d).ToRaw());
            Assert.AreEqual("0", ((double?)0d).ToRaw());
            Assert.AreEqual("1.79769313486232E+308", ((double?)double.MaxValue).ToRaw());
            Assert.AreEqual("-1.79769313486232E+308", ((double?)double.MinValue).ToRaw());
            Assert.AreEqual("3.1415926", ((double?)(3.1415926d)).ToRaw());
            Assert.AreEqual("-3.1415926", ((double?)(-3.1415926d)).ToRaw());
        }
    }
}
