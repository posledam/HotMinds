using System;
using System.Collections.Generic;
using HotMinds.Collections;
using NUnit.Framework;

namespace HotMinds.UnitTests
{
    // ReSharper disable once InconsistentNaming

    [TestFixture]
    public class LinqExtensions_Tests
    {
        [Test]
        public void Split_Test()
        {
            IEnumerable<int> nullSeq = null;
            IEnumerable<int> emptySeq = new int[0];
            IEnumerable<int> oneSeq = new[] { 0 };
            IEnumerable<int> twoSeq = new[] { 0, 1 };
            IEnumerable<int> threeSeq = new[] { 0, 1, 2 };

            IEnumerable<int> seq1 = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            IEnumerable<int> seq2 = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.That(() => nullSeq.Split(2),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("sequence"));

            Assert.That(() => seq1.Split(0),
                Throws.TypeOf<ArgumentOutOfRangeException>().With.Property("ParamName").EqualTo("parts"));
            Assert.That(() => seq1.Split(1),
                Throws.TypeOf<ArgumentOutOfRangeException>().With.Property("ParamName").EqualTo("parts"));

            Assert.That(emptySeq.Split(2),
                Is.Not.Null.And.Empty);

            Assert.That(oneSeq.Split(2),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0 } }));

            Assert.That(twoSeq.Split(2),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0 }, new[] { 1 } }));

            Assert.That(threeSeq.Split(2),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0, 2 }, new[] { 1 } }));

            Assert.That(seq1.Split(2),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0, 2, 4, 6, 8, 10, 12, 14 }, new[] { 1, 3, 5, 7, 9, 11, 13 } }));

            Assert.That(seq2.Split(2),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0, 2, 4, 6, 8, 10, 12, 14 }, new[] { 1, 3, 5, 7, 9, 11, 13, 15 } }));
        }

        [Test]
        public void Partition_Test()
        {
            IEnumerable<int> nullSeq = null;
            IEnumerable<int> emptySeq = new int[0];
            IEnumerable<int> oneSeq = new[] { 0 };
            IEnumerable<int> twoSeq = new[] { 0, 1 };
            IEnumerable<int> threeSeq = new[] { 0, 1, 2 };

            IEnumerable<int> seq2 = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.That(() => nullSeq.Partition(1),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("sequence"));
            Assert.That(() => seq2.Partition(0),
                Throws.TypeOf<ArgumentOutOfRangeException>().With.Property("ParamName").EqualTo("size"));

            Assert.That(emptySeq.Partition(1),
                Is.Not.Null.And.Empty);

            Assert.That(oneSeq.Partition(1),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0 } }));

            Assert.That(oneSeq.Partition(2),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0 } }));

            Assert.That(twoSeq.Partition(1),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0 }, new[] { 1 } }));

            Assert.That(twoSeq.Partition(2),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0, 1 } }));

            Assert.That(twoSeq.Partition(3),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0, 1 } }));

            Assert.That(threeSeq.Partition(2),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0, 1 }, new[] { 2 } }));

            Assert.That(seq2.Partition(3),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, new[] { 6, 7, 8 }, new[] { 9, 10, 11 }, new[] { 12, 13, 14 }, new[] { 15 } }));

            Assert.That(seq2.Partition(10),
                Is.Not.Null.And.EquivalentTo(new[] { new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new[] { 10, 11, 12, 13, 14, 15 } }));
        }
    }
}