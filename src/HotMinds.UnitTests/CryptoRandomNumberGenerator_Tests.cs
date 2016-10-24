using System;
using HotMinds.Utils;
using NUnit.Framework;

namespace HotMinds.UnitTests
{
    [TestFixture]
    public class CryptoRandomNumberGenerator_Tests
    {
        [Test]
        public void Common_Test()
        {
            var generator = new CryptoRandomNumberGenerator(16);

            Int32 i32;
            UInt32 u32;
            UInt64 u64;

            // some gen
            i32 = generator.NextInt32();
            i32 = generator.NextInt32();
            i32 = generator.NextInt32();

            // some gen
            u32 = generator.NextUInt32();
            u32 = generator.NextUInt32();
            u32 = generator.NextUInt32();

            // some gen
            u64 = generator.NextUInt64();
            u64 = generator.NextUInt64();
            u64 = generator.NextUInt64();
        }

        [Test]
        public void Dispose_Test()
        {
            var generator = new CryptoRandomNumberGenerator();

            generator.Dispose();

            Assert.That(() => generator.NextByte(), Throws.InstanceOf<ObjectDisposedException>());
            Assert.That(() => generator.NextInt32(), Throws.InstanceOf<ObjectDisposedException>());
            Assert.That(() => generator.NextUInt32(), Throws.InstanceOf<ObjectDisposedException>());
            Assert.That(() => generator.NextUInt64(), Throws.InstanceOf<ObjectDisposedException>());

            // double dispose not throws
            generator.Dispose();
            generator.Dispose();
        }
    }
}
