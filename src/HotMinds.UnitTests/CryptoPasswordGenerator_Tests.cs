using System;
using System.Diagnostics;
using HotMinds.Utils;
using NUnit.Framework;

namespace HotMinds.UnitTests
{
    [TestFixture]
    public class CryptoPasswordGenerator_Tests
    {
        [Test]
        public void Ctor_Test()
        {
            using (var cryptoPasswordGenerator = new CryptoPasswordGenerator())
            {
                GC.KeepAlive(cryptoPasswordGenerator);
            }
        }

        [Test]
        public void Dispose_Test()
        {
            var cryptoPasswordGenerator = new CryptoPasswordGenerator();
            cryptoPasswordGenerator.Dispose();
            Assert.That(() => cryptoPasswordGenerator.Generate(), Throws
                .InstanceOf<ObjectDisposedException>()
                .With.Property("ObjectName").EqualTo(typeof(CryptoPasswordGenerator).FullName));
        }

        [Test]
        public void Set_MinLength_Test()
        {
            var cryptoPasswordGenerator = new CryptoPasswordGenerator();

            // must be 1 or greater
            Assert.That(() => { cryptoPasswordGenerator.MinLength = -1; }, Throws
                .ArgumentException.With.Property("ParamName").EqualTo("value"));

            // must be 1 or greater
            Assert.That(() => { cryptoPasswordGenerator.MinLength = 0; }, Throws
                .ArgumentException.With.Property("ParamName").EqualTo("value"));

            // must be less or equals MaxLength
            Assert.That(() => { cryptoPasswordGenerator.MinLength = cryptoPasswordGenerator.MaxLength + 1; }, Throws
                .ArgumentException.With.Property("ParamName").EqualTo("value"));

            cryptoPasswordGenerator.MinLength = 1;
            Assert.That(cryptoPasswordGenerator.MinLength, Is.EqualTo(1));

            cryptoPasswordGenerator.MinLength = 2;
            Assert.That(cryptoPasswordGenerator.MinLength, Is.EqualTo(2));

            cryptoPasswordGenerator.MinLength = 3;
            Assert.That(cryptoPasswordGenerator.MinLength, Is.EqualTo(3));
        }

        [Test]
        public void Set_MaxLength_Test()
        {
            var cryptoPasswordGenerator = new CryptoPasswordGenerator();

            // must be greater or equals than MinLength
            Assert.That(() => { cryptoPasswordGenerator.MaxLength = cryptoPasswordGenerator.MinLength - 1; }, Throws
                .ArgumentException.With.Property("ParamName").EqualTo("value"));

            // must be less or equals CryptoPasswordGenerator.LengthLimit
            Assert.That(() => { cryptoPasswordGenerator.MaxLength = CryptoPasswordGenerator.LengthLimit + 1; }, Throws
                .ArgumentException.With.Property("ParamName").EqualTo("value"));

            cryptoPasswordGenerator.MaxLength = 100;
            Assert.That(cryptoPasswordGenerator.MaxLength, Is.EqualTo(100));

            cryptoPasswordGenerator.MaxLength = 101;
            Assert.That(cryptoPasswordGenerator.MaxLength, Is.EqualTo(101));

            cryptoPasswordGenerator.MaxLength = 102;
            Assert.That(cryptoPasswordGenerator.MaxLength, Is.EqualTo(102));
        }

        [Test]
        public void Set_CharsetGroups_Test()
        {
            var cryptoPasswordGenerator = new CryptoPasswordGenerator();

            // null
            Assert.That(() => { cryptoPasswordGenerator.CharsetGroups = null; }, Throws
                .ArgumentNullException.With.Property("ParamName").EqualTo("value"));
        }

        [Test]
        public void Generate_Test()
        {
            var cryptoPasswordGenerator = new CryptoPasswordGenerator();

            var minLength = int.MaxValue;
            var maxLength = int.MinValue;

            for (int i = 0; i < 100; i++)
            {
                var password = cryptoPasswordGenerator.Generate();
                minLength = Math.Min(minLength, password.Length);
                maxLength = Math.Max(maxLength, password.Length);
                TestContext.WriteLine("Generated: {0}", password);
            }

            TestContext.WriteLine("Min length: {0}", minLength);
            TestContext.WriteLine("Max length: {0}", maxLength);
        }

        [Test]
        public void Generate_Speed_Test()
        {
            var cryptoPasswordGenerator = new CryptoPasswordGenerator();

            var sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                cryptoPasswordGenerator.Generate();
            }
            sw.Stop();
            TestContext.WriteLine("Generated: {0}", sw.Elapsed);
            Assert.That(sw.Elapsed.TotalSeconds, Is.LessThan(0.5));
        }
    }
}
