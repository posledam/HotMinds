using System;
using HotMinds.Strings;
using NUnit.Framework;

namespace HotMinds.UnitTests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class CommonStringExtensions_Tests
    {
        [Test]
        public void IsNullOrEmpty_Test()
        {
            Assert.That(((string)null).IsNullOrEmpty, Is.True);
            Assert.That(String.Empty.IsNullOrEmpty, Is.True);
            Assert.That("".IsNullOrEmpty, Is.True);
            Assert.That(" ".IsNullOrEmpty, Is.False);
            Assert.That("\t".IsNullOrEmpty, Is.False);
            Assert.That("\t\t\t   ".IsNullOrEmpty, Is.False);
            Assert.That("123".IsNullOrEmpty, Is.False);
            Assert.That(" 123 ".IsNullOrEmpty, Is.False);
        }

        [Test]
        public void IsNullOrWhiteSpace_Test()
        {
            Assert.That(((string)null).IsNullOrWhiteSpace, Is.True);
            Assert.That(String.Empty.IsNullOrWhiteSpace, Is.True);
            Assert.That("".IsNullOrWhiteSpace, Is.True);
            Assert.That(" ".IsNullOrWhiteSpace, Is.True);
            Assert.That("\t".IsNullOrWhiteSpace, Is.True);
            Assert.That("\t\t\t   ".IsNullOrWhiteSpace, Is.True);
            Assert.That("123".IsNullOrWhiteSpace, Is.False);
            Assert.That(" 123 ".IsNullOrWhiteSpace, Is.False);
        }
    }
}
