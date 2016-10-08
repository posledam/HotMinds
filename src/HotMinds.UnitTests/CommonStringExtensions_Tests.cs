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
        public void GetLength_Test()
        {
            Assert.That(((string)null).GetLength(), Is.EqualTo(0));
            Assert.That(string.Empty.GetLength(), Is.EqualTo(0));
            Assert.That("".GetLength(), Is.EqualTo(0));
            Assert.That(" ".GetLength(), Is.EqualTo(1));
            Assert.That("abc".GetLength(), Is.EqualTo(3));
            Assert.That(" a b c ".GetLength(), Is.EqualTo(7));
        }

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

        [Test]
        public void Limit_Test()
        {
            Assert.That(() => "abc".Limit(int.MinValue), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abc".Limit(-1), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abc".Limit(0), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));

            Assert.That(((string)null).Limit(1), Is.Null);
            Assert.That(string.Empty.Limit(1), Is.EqualTo(string.Empty));
            Assert.That("".Limit(1), Is.EqualTo(string.Empty));
            Assert.That("abc".Limit(1), Is.EqualTo("a"));
            Assert.That("abc".Limit(2), Is.EqualTo("ab"));
            Assert.That("abc".Limit(3), Is.EqualTo("abc"));
            Assert.That("abc".Limit(4), Is.EqualTo("abc"));
            Assert.That("abc".Limit(int.MaxValue), Is.EqualTo("abc"));
        }

        [Test]
        public void Truncate_Default_Test()
        {
            Assert.That(() => "abcdef".Truncate(int.MinValue), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abcdef".Truncate(-1), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abcdef".Truncate(0), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abcdef".Truncate(1), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));

            Assert.That(((string)null).Truncate(2), Is.Null);
            Assert.That(string.Empty.Truncate(2), Is.EqualTo(string.Empty));
            Assert.That("".Truncate(2), Is.EqualTo(string.Empty));
            Assert.That("abcdef".Truncate(2), Is.EqualTo("a…"));
            Assert.That("abcdef".Truncate(3), Is.EqualTo("ab…"));
            Assert.That("abcdef".Truncate(4), Is.EqualTo("abc…"));
            Assert.That("abcdef".Truncate(5), Is.EqualTo("abcd…"));
            Assert.That("abcdef".Truncate(6), Is.EqualTo("abcdef"));
            Assert.That("abcdef".Truncate(7), Is.EqualTo("abcdef"));
            Assert.That("abcdef".Truncate(int.MaxValue), Is.EqualTo("abcdef"));
        }

        [Test]
        public void Truncate_CustomEmpty_Test()
        {
            Assert.That(() => "abcdef".Truncate(100, null), Throws.ArgumentNullException.With.Property("ParamName").EqualTo("ellipsis"));

            Assert.That(() => "abcdef".Truncate(int.MinValue, ""), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abcdef".Truncate(-1, ""), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abcdef".Truncate(0, ""), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));

            Assert.That(((string)null).Truncate(1, ""), Is.Null);
            Assert.That(string.Empty.Truncate(1, ""), Is.EqualTo(string.Empty));
            Assert.That("".Truncate(1, ""), Is.EqualTo(string.Empty));
            Assert.That("abcdef".Truncate(1, ""), Is.EqualTo("a"));
            Assert.That("abcdef".Truncate(2, ""), Is.EqualTo("ab"));
            Assert.That("abcdef".Truncate(3, ""), Is.EqualTo("abc"));
            Assert.That("abcdef".Truncate(4, ""), Is.EqualTo("abcd"));
            Assert.That("abcdef".Truncate(5, ""), Is.EqualTo("abcde"));
            Assert.That("abcdef".Truncate(6, ""), Is.EqualTo("abcdef"));
            Assert.That("abcdef".Truncate(7, ""), Is.EqualTo("abcdef"));
            Assert.That("abcdef".Truncate(int.MaxValue, ""), Is.EqualTo("abcdef"));
        }

        [Test]
        public void Truncate_CustomDots_Test()
        {
            Assert.That(() => "abcdef".Truncate(int.MinValue, "..."), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abcdef".Truncate(-1, "..."), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abcdef".Truncate(0, "..."), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abcdef".Truncate(1, "..."), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abcdef".Truncate(2, "..."), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));
            Assert.That(() => "abcdef".Truncate(3, "..."), Throws.ArgumentException.With.Property("ParamName").EqualTo("length"));

            Assert.That(((string)null).Truncate(4, "..."), Is.Null);
            Assert.That(string.Empty.Truncate(4, "..."), Is.EqualTo(string.Empty));
            Assert.That("".Truncate(4, "..."), Is.EqualTo(string.Empty));
            Assert.That("abcdef".Truncate(4, "..."), Is.EqualTo("a..."));
            Assert.That("abcdef".Truncate(5, "..."), Is.EqualTo("ab..."));
            Assert.That("abcdef".Truncate(6, "..."), Is.EqualTo("abcdef"));
            Assert.That("abcdef".Truncate(7, "..."), Is.EqualTo("abcdef"));
            Assert.That("abcdef".Truncate(int.MaxValue, "..."), Is.EqualTo("abcdef"));
        }

        [Test]
        public void TrimCollapse_Test()
        {
            Assert.That(((string)null).TrimCollapse(), Is.Null);

            Assert.That("".TrimCollapse(), Is.Empty);
            Assert.That(string.Empty.TrimCollapse(), Is.Empty);
            Assert.That(" ".TrimCollapse(), Is.Empty);
            Assert.That("   ".TrimCollapse(), Is.Empty);
            Assert.That(" \t \t \t ".TrimCollapse(), Is.Empty);

            Assert.That("abcdef".TrimCollapse(), Is.EqualTo("abcdef"));
            Assert.That("   abcdef".TrimCollapse(), Is.EqualTo("abcdef"));
            Assert.That("   \t\t\t   abcdef".TrimCollapse(), Is.EqualTo("abcdef"));
            Assert.That("abcdef   ".TrimCollapse(), Is.EqualTo("abcdef"));
            Assert.That("abcdef   \t\t\t   ".TrimCollapse(), Is.EqualTo("abcdef"));
            Assert.That("   abcdef   ".TrimCollapse(), Is.EqualTo("abcdef"));
            Assert.That("   \t\t\t   abcdef   \t\t\t   ".TrimCollapse(), Is.EqualTo("abcdef"));

            Assert.That("abc 123 def 4bc de5".TrimCollapse(), Is.EqualTo("abc 123 def 4bc de5"));
            Assert.That("   abc 123 def 4bc de5   ".TrimCollapse(), Is.EqualTo("abc 123 def 4bc de5"));
            Assert.That("   \t\t\t   abc 123 def 4bc de5   \t\t\t   ".TrimCollapse(), Is.EqualTo("abc 123 def 4bc de5"));
            Assert.That(" abc   123     def     4bc      de5       ".TrimCollapse(), Is.EqualTo("abc 123 def 4bc de5"));
            Assert.That("\tabc\t123\tdef\t4bc\tde5\t".TrimCollapse(), Is.EqualTo("abc 123 def 4bc de5"));
            Assert.That(" \t abc \t  123 \t  \t  def\t   \t  4bc \t\t     \tde5      \t ".TrimCollapse(), 
                Is.EqualTo("abc 123 def 4bc de5"));
        }
    }
}
