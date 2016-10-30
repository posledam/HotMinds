using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using HotMinds.Extensions;
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

        private void GetOrDefault_Tester<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> dictionary,
            TKey hasKey,
            TValue hasValue,
            TKey absentKey,
            TValue defaultValue)
        {
            // check null
            Assert.That(((IEnumerable<KeyValuePair<TKey, TValue>>)null).GetOrDefault(hasKey), Is.EqualTo(default(TValue)));
            Assert.That(((IEnumerable<KeyValuePair<TKey, TValue>>)null).GetOrDefault(hasKey, defaultValue), Is.EqualTo(defaultValue));

            // commoon check
            Assert.That(dictionary.GetOrDefault(hasKey), Is.EqualTo(hasValue));
            Assert.That(dictionary.GetOrDefault(hasKey, defaultValue), Is.EqualTo(hasValue));

            Assert.That(dictionary.GetOrDefault(absentKey), Is.EqualTo(default(TValue)));
            Assert.That(dictionary.GetOrDefault(absentKey, defaultValue), Is.EqualTo(defaultValue));
        }

        [Test]
        public void GetOrDefault_Test()
        {
            // test key null
            Assert.That(() => ((IDictionary<string, int>)null).GetOrDefault(null), Throws
                .ArgumentNullException.With.Property("ParamName").EqualTo("key"));
            Assert.That(() => ((IDictionary<string, int>)null).GetOrDefault(null, 555), Throws
                .ArgumentNullException.With.Property("ParamName").EqualTo("key"));
            Assert.That(() => new Dictionary<string, string>().GetOrDefault(null), Throws
                .ArgumentNullException.With.Property("ParamName").EqualTo("key"));
            Assert.That(() => new Dictionary<string, string>().GetOrDefault(null, "123"), Throws
                .ArgumentNullException.With.Property("ParamName").EqualTo("key"));

            // IReadOnlyDictionary
            this.GetOrDefault_Tester(new Dictionary<int, string> { { 123, "-555-" } },
                123, "-555-", 111, "it is default value");
            this.GetOrDefault_Tester(new Dictionary<string, int> { { "123", 555 } },
                "123", 555, "222", 1000);

            // IDictionary
            this.GetOrDefault_Tester(new OnlyIDictionaryImplementation<int, string> { { 123, "-555-" } },
                123, "-555-", 111, "it is default value");
            this.GetOrDefault_Tester(new OnlyIDictionaryImplementation<string, int> { { "123", 555 } },
                "123", 555, "222", 1000);

            // IEnumerable
            this.GetOrDefault_Tester(new[] { new KeyValuePair<int, string>(123, "-555-") },
                123, "-555-", 111, "it is default value");
            this.GetOrDefault_Tester(new[] { new KeyValuePair<string, int>("123", 555) },
                "123", 555, "222", 1000);
        }

        [Test]
        public void In_Test()
        {
            // default use
            Assert.That(1.In(1, 2, 3), Is.True);
            Assert.That(5.In(1, 2, 3), Is.False);
            Assert.That("a".In("a", "b", "c", null), Is.True);
            Assert.That("d".In("a", "b", "c", null), Is.False);

            // empty and null source
            Assert.That(1.In(), Is.False);
            Assert.That(1.In((int[])null), Is.False);

            // null value
            Assert.That(((string)null).In("a", "b", "c", null), Is.True);
            Assert.That(((string)null).In("a", "b", "c", null), Is.True);
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

    public class OnlyIDictionaryImplementation<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dictionary;

        public OnlyIDictionaryImplementation()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>)_dictionary).Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)_dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>)_dictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)_dictionary).Remove(item);
        }

        public int Count => _dictionary.Count;

        public bool IsReadOnly => ((IDictionary<TKey, TValue>)_dictionary).IsReadOnly;

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get { return _dictionary[key]; }
            set { _dictionary[key] = value; }
        }

        public ICollection<TKey> Keys => _dictionary.Keys;

        public ICollection<TValue> Values => _dictionary.Values;
    }
}
