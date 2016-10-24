using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using HotMinds.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace HotMinds.UnitTests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public class CommonCollectionExtensions_Tests
    {
        [Test]
        public void IsEmpty_Null_Test()
        {
            var intPreset = new CollectionsPreset<int>(null);
            var strPreset = new CollectionsPreset<string>(null);
            intPreset.AssertIsEmpty(Is.True);
            strPreset.AssertIsEmpty(Is.True);
        }

        [Test]
        public void IsEmpty_Zero_Test()
        {
            var intPreset = new CollectionsPreset<int>(new List<int>());
            var strPreset = new CollectionsPreset<string>(new List<string>());
            intPreset.AssertIsEmpty(Is.True);
            strPreset.AssertIsEmpty(Is.True);
        }

        [Test]
        public void IsEmpty_One_Test()
        {
            var intPreset = new CollectionsPreset<int>(new List<int> { 55 });
            var strPreset = new CollectionsPreset<string>(new List<string> { "!first!" });
            intPreset.AssertIsEmpty(Is.False);
            strPreset.AssertIsEmpty(Is.False);
        }

        [Test]
        public void IsEmpty_Many_Test()
        {
            var intPreset = new CollectionsPreset<int>(new List<int> { 11, 22, 33, 444, 55 });
            var strPreset = new CollectionsPreset<string>(new List<string> { "first", "second", "third", "fourth", "soon" });
            intPreset.AssertIsEmpty(Is.False);
            strPreset.AssertIsEmpty(Is.False);
        }

        [Test]
        public void GetOrDefault_IntStr_Test()
        {
            // test null
            Assert.That(((IDictionary<int, string>)null).GetOrDefault(555), Is.Null);
            Assert.That(((IDictionary<int, string>)null).GetOrDefault(555, "some"), Is.EqualTo("some"));

            var intStrDic = new Dictionary<int, string> { { 123, "custom" } };

            // default default
            Assert.That(intStrDic.GetOrDefault(123), Is.EqualTo("custom"));
            Assert.That(intStrDic.GetOrDefault(0), Is.Null);
            Assert.That(intStrDic.GetOrDefault(555), Is.Null);

            // custom default
            Assert.That(intStrDic.GetOrDefault(123, "some"), Is.EqualTo("custom"));
            Assert.That(intStrDic.GetOrDefault(0, "some"), Is.EqualTo("some"));
            Assert.That(intStrDic.GetOrDefault(555, "some"), Is.EqualTo("some"));
        }

        [Test]
        public void GetOrDefault_StrInt_Test()
        {
            // test null
            Assert.That(((IDictionary<string, int>)null).GetOrDefault("some"), Is.EqualTo(0));
            Assert.That(((IDictionary<string, int>)null).GetOrDefault("some", 555), Is.EqualTo(555));

            var strIntDic = new Dictionary<string, int> { { "custom", 123 } };

            // default default
            Assert.That(strIntDic.GetOrDefault("custom"), Is.EqualTo(123));
            Assert.That(strIntDic.GetOrDefault(null), Is.EqualTo(0));
            Assert.That(strIntDic.GetOrDefault(string.Empty), Is.EqualTo(0));
            Assert.That(strIntDic.GetOrDefault("abracadabra"), Is.EqualTo(0));

            // custom default
            Assert.That(strIntDic.GetOrDefault("custom", 555), Is.EqualTo(123));
            Assert.That(strIntDic.GetOrDefault(null, 555), Is.EqualTo(555));
            Assert.That(strIntDic.GetOrDefault(string.Empty, 555), Is.EqualTo(555));
            Assert.That(strIntDic.GetOrDefault("abracadabra", 555), Is.EqualTo(555));
        }

        public class CollectionsPreset<T>
        {
            public IEnumerable<T> EnumerableInterface => List?.Skip(0);
            public ICollection<T> CollectionInterface => Collection;
            public IReadOnlyCollection<T> ReadOnlyCollectionInterface => Collection;
            public IReadOnlyList<T> ReadOnlyListInterface => List;

            public IReadOnlyDictionary<string, T> ReadOnlyDictionaryInterface => Dictionary;
            public IDictionary<string, T> DictionaryInterface => Dictionary;

            public List<T> List { get; }
            public Collection<T> Collection { get; }
            public T[] Array { get; }
            public Dictionary<string, T> Dictionary { get; }

            public CollectionsPreset(List<T> initList)
            {
                List = initList;
                Collection = initList == null ? null : new Collection<T>(initList);
                Array = initList?.ToArray();
                Dictionary = initList?.ToDictionary(k => k.ToString(), v => v);
            }

            [Test]
            public void AssertIsEmpty(IResolveConstraint constraint)
            {
                Assert.That(EnumerableInterface.IsEmpty(), constraint);
                Assert.That(CollectionInterface.IsEmpty(), constraint);
                Assert.That(ReadOnlyCollectionInterface.IsEmpty(), constraint);
                Assert.That(ReadOnlyListInterface.IsEmpty(), constraint);
                Assert.That(ReadOnlyDictionaryInterface.IsEmpty(), constraint);
                Assert.That(List.IsEmpty(), constraint);
                Assert.That(Collection.IsEmpty(), constraint);
                Assert.That(Array.IsEmpty(), constraint);
                Assert.That(Dictionary.IsEmpty(), constraint);
                Assert.That(DictionaryInterface.IsEmpty(), constraint);
            }
        }
    }
}
