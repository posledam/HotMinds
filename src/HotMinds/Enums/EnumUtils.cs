using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace HotMinds.Enums
{
    /// <summary>
    ///     Enum utils.
    /// </summary>
    public static class EnumUtils
    {
        #region internal static fields

        // Internal Enum metadata cache by Enum type.
        internal static readonly ConcurrentDictionary<Type, IReadOnlyCollection<EnumMetadata>> EnumCache
            = new ConcurrentDictionary<Type, IReadOnlyCollection<EnumMetadata>>();

        // Internal Enum metadata cache by special Enum key.
        internal static readonly ConcurrentDictionary<EnumKey, EnumMetadata> FieldCache
            = new ConcurrentDictionary<EnumKey, EnumMetadata>();

        #endregion internal static fields


        #region general public methods

        /// <summary>
        ///     Get Enum values metadata collection.
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Concrete Enum type. </typeparam>
        /// <returns>
        ///     Collection of Enum values metadata, ordered by <see cref="EnumMetadata.Order"/>.
        /// </returns>
        public static IReadOnlyCollection<EnumMetadata<TEnum>> GetMetadata<TEnum>() where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new InvalidOperationException($"Passed generic type {typeof(TEnum).FullName} is not Enum.");
            return GetInternal<TEnum>();
        }

        /// <summary>
        ///     Get Enum values metadata collection.
        /// </summary>
        /// <param name="enumType">
        ///     Concrete Enum type. </param>
        /// <returns>
        ///     Collection of Enum values metadata, ordered by <see cref="EnumMetadata.Order"/>.
        /// </returns>
        public static IReadOnlyCollection<EnumMetadata> GetMetadata([NotNull] Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException(nameof(enumType));
            if (!enumType.IsEnum)
                throw new ArgumentException($"Passed generic type {enumType.FullName} is not Enum.", nameof(enumType));
            return GetInternal(enumType);
        }

        /// <summary>
        ///     Get Enum value metadata.
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Enum type (get at compile time). </typeparam>
        /// <param name="value">
        ///     Enum value. </param>
        /// <returns>
        ///     Enum value metadata (typed).
        /// </returns>
        public static EnumMetadata<TEnum> GetMetadata<TEnum>(TEnum value)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new InvalidOperationException($"Passed generic type {typeof(TEnum).FullName} is not Enum.");
            var key = new EnumKey(typeof(TEnum), value);
            EnumMetadata metadata;
            if (!FieldCache.TryGetValue(key, out metadata))
            {
                var list = GetInternal<TEnum>();
                var comparer = EqualityComparer<TEnum>.Default;
                return list.FirstOrDefault(p => comparer.Equals(value, p.Value));
            }
            return (EnumMetadata<TEnum>)metadata;
        }

        /// <summary>
        ///     Get Enum value metadata.
        /// </summary>
        /// <param name="value">
        ///     Enum value. </param>
        /// <returns>
        ///     Enum value metadata (typed).
        /// </returns>
        public static EnumMetadata GetMetadata([NotNull] Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            var enumType = value.GetType();
            var key = new EnumKey(enumType, value);
            EnumMetadata metadata;
            if (!FieldCache.TryGetValue(key, out metadata))
            {
                var list = GetInternal(enumType);
                return list.FirstOrDefault(p => value.Equals(p.Enum));
            }
            return metadata;
        }

        /// <summary>
        ///     Get Enum value metadata by name.
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Enum type. </typeparam>
        /// <param name="name">
        ///     Enum value name. </param>
        /// <param name="ignoreCase">
        ///     Ignore case for searching metadata by name. </param>
        /// <returns>
        ///     Enum value metadata or null (if not found).
        /// </returns>
        public static EnumMetadata<TEnum> GetMetadata<TEnum>([NotNull] string name, bool ignoreCase = false)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new InvalidOperationException($"Passed generic type {typeof(TEnum).FullName} is not Enum.");
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            var list = GetInternal<TEnum>();
            return ignoreCase
                ? list.FirstOrDefault(p => string.Equals(name, p.Name, StringComparison.InvariantCultureIgnoreCase))
                : list.FirstOrDefault(p => string.Equals(name, p.Name, StringComparison.InvariantCulture));
        }

        /// <summary>
        ///     Get Enum value metadata by name.
        /// </summary>
        /// <param name="enumType">
        ///     Enum type. </param>
        /// <param name="name">
        ///     Enum value name. </param>
        /// <param name="ignoreCase">
        ///     Ignore case for searching metadata by name. </param>
        /// <returns>
        ///     Enum value metadata or null (if not found).
        /// </returns>
        public static EnumMetadata GetMetadata([NotNull] Type enumType, [NotNull] string name, bool ignoreCase = false)
        {
            if (enumType == null)
                throw new ArgumentNullException(nameof(enumType));
            if (!enumType.IsEnum)
                throw new ArgumentException($"Passed generic type {enumType.FullName} is not Enum.", nameof(enumType));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            var list = GetInternal(enumType);
            return ignoreCase
                ? list.FirstOrDefault(p => string.Equals(name, p.Name, StringComparison.InvariantCultureIgnoreCase))
                : list.FirstOrDefault(p => string.Equals(name, p.Name, StringComparison.InvariantCulture));
        }

        #endregion general public methods


        #region public display helpers

        /// <summary>
        ///     Get Enum value display name (from attribute/resources or Enum value name).
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Enum value type. </typeparam>
        /// <param name="value">
        ///     Enum value. </param>
        /// <returns>
        ///     Enum value display name (may be localized).
        /// </returns>
        public static string GetDisplayName<TEnum>(TEnum value) where TEnum : struct
        {
            return GetMetadata(value).GetDisplayName();
        }

        /// <summary>
        ///     Get Enum value display name (from attribute/resources or Enum value name).
        /// </summary>
        /// <param name="value">
        ///     Enum value. </param>
        /// <returns>
        ///     Enum value display name (may be localized).
        /// </returns>
        public static string GetDisplayName([NotNull] Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            return GetMetadata(value).GetDisplayName();
        }

        #endregion public display helpers


        #region private methods

        // Get cached Enum metadata values list by generic Enum type <typeparamref name="TEnum"/>.
        private static IReadOnlyCollection<EnumMetadata<TEnum>> GetInternal<TEnum>() where TEnum : struct
        {
            var info = EnumCache.GetOrAdd(typeof(TEnum), BuildInternal<TEnum>);
            return (IReadOnlyCollection<EnumMetadata<TEnum>>)info;
        }

        // Get cached Enum metadata values list by Enum type <paramref name="enumType"/>.
        private static IReadOnlyCollection<EnumMetadata> GetInternal([NotNull] Type enumType)
        {
            return EnumCache.GetOrAdd(enumType, t =>
            {
                var buildMethod = BuildMethod.MakeGenericMethod(enumType);
                var result = (IReadOnlyCollection<EnumMetadata>)buildMethod.Invoke(null, new object[] { enumType });
                return result;
            });
        }

        // Internal MethodInfo referenced to build Enum metadata cache generic method. Used for
        // fast make generic method call via reflection.
        private static readonly MethodInfo BuildMethod = typeof(EnumUtils).GetMethod(nameof(BuildInternal),
            BindingFlags.NonPublic | BindingFlags.Static);

        // Build Enum metadata for Enum type <typeparamref name="TEnum"/> for caching.
        private static IReadOnlyCollection<EnumMetadata<TEnum>> BuildInternal<TEnum>([NotNull] Type enumType) where TEnum : struct
        {
#pragma warning disable 618
            var enumItems =
                // extracts enum values meta data (include enum values with duplicate underlying values)
                from name in Enum.GetNames(enumType)
                let memberInfo = enumType.GetField(name)
                let value = memberInfo.GetValue(null)
                let display = memberInfo.GetCustomAttribute<DisplayAttribute>()
                let displayName = memberInfo.GetCustomAttribute<DisplayNameAttribute>() // for support old code only
                let description = memberInfo.GetCustomAttribute<DescriptionAttribute>() // for support old code only
                let order = display?.GetOrder() ?? 0
                orderby order, value // use custom sorting
                select new EnumMetadata<TEnum>
                {
                    Enum = (Enum)value,
                    Name = name,
                    Value = (TEnum)value,
                    MemberInfo = memberInfo,
                    Display = display,
                    DisplayName = displayName, // for support old code only
                    Description = description, // for support old code only
                    Order = order
                };
#pragma warning restore 618
            var list = enumItems.ToList();
            // internal cache
            foreach (var item in list)
            {
                FieldCache.TryAdd(new EnumKey(enumType, item.Enum), item);
                //FieldCache[new EnumKey(enumType, item.Enum)] = item;
            }
            return list;
        }

        #endregion private methods


        #region private types

        // Unique enum value key for storing meta data in cache.
        internal struct EnumKey
        {
            // ReSharper disable NotAccessedField.Local

            private readonly Type _enumType;
            private readonly object _value;
            private readonly int _hash;

            // ReSharper restore NotAccessedField.Local

            // Create unique enum key by type and value.
            public EnumKey([NotNull] Type enumType, [NotNull] object value)
            {
                _enumType = enumType;
                _value = value;
                _hash = enumType.GetHashCode() ^ value.GetHashCode();
            }

            #region Overrides of ValueType

            // Override struct hash function.
            public override int GetHashCode()
            {
                return _hash;
            }

            #endregion
        }

        #endregion private types
    }
}
