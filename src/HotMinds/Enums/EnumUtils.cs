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

        /// <summary>
        ///     Internal meta data cache by type.
        /// </summary>
        internal static readonly ConcurrentDictionary<Type, IReadOnlyCollection<EnumMetadata>> EnumCache
            = new ConcurrentDictionary<Type, IReadOnlyCollection<EnumMetadata>>();

        /// <summary>
        ///     Internal meta data cache by key.
        /// </summary>
        internal static readonly Dictionary<EnumKey, EnumMetadata> FieldCache
            = new Dictionary<EnumKey, EnumMetadata>();

        #endregion internal static fields


        #region general public methods

        /// <summary>
        ///     Получить коллекцию мета-данных всех доступных значений Enum-типа.
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Тип перечисления. </typeparam>
        /// <returns>
        ///     Коллекция мета-данных всех доступных значений Enum-типа, отсортированная по значению
        ///     <see cref="EnumMetadata.Order"/>. 
        /// </returns>
        public static IReadOnlyCollection<EnumMetadata<TEnum>> GetMetadata<TEnum>() where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new InvalidOperationException($"Passed generic type {typeof(TEnum).FullName} is not Enum.");
            return GetInternal<TEnum>();
        }

        /// <summary>
        ///     Получить коллекцию мета-данных всех доступных значений Enum-типа.
        /// </summary>
        /// <param name="enumType">
        ///     Тип перечисления. </param>
        /// <returns>
        ///     Коллекция мета-данных всех доступных значений Enum-типа, отсортированная по значению
        ///     <see cref="EnumMetadata.Order"/>. 
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
        ///     Получить объект мета-данных указанного Enum-значения.
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Тип перечисления, выводится автоматически. </typeparam>
        /// <param name="value">
        ///     Значение перечисления. </param>
        /// <returns>
        ///     Объект мета-данных Enum-значения.
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
        ///     Получить объект мета-данных указанного Enum-значения.
        /// </summary>
        /// <param name="value">
        ///     Значение перечисления. </param>
        /// <returns>
        ///     Объект мета-данных Enum-значения.
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
        ///     Получить объект типизированных мета-данных перечисления по названию Enum-значения.
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Тип перечисления. </typeparam>
        /// <param name="name">
        ///     Строка с текстовым значением перечисления. </param>
        /// <param name="ignoreCase">
        ///     Сравнивать текстовое значение без учёта регистра. </param>
        /// <returns>
        ///     Объект типизированных мета-данных Enum-значения или null, если такое значение не найдено.
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
        ///     Получить объект мета-данных перечисления по названию Enum-значения.
        /// </summary>
        /// <param name="enumType">
        ///     Тип перечисления. </param>
        /// <param name="name">
        ///     Строка с текстовым значением перечисления. </param>
        /// <param name="ignoreCase">
        ///     Сравнивать текстовое значение без учёта регистра. </param>
        /// <returns>
        ///     Объект мета-данных Enum-значения или null, если такое значение не найдено.
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
        ///     Получить локализованное название Enum-значения.
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Тип перечисления, выводится автоматически. </typeparam>
        /// <param name="value">
        ///     Значение перечисления. </param>
        /// <returns>
        ///     Текст локализованного названия Enum-значение при наличии, или название Enum-значения.
        /// </returns>
        public static string GetDisplayName<TEnum>(TEnum value) where TEnum : struct
        {
            return GetMetadata(value).GetDisplayName();
        }

        /// <summary>
        ///     Получить локализованное название Enum-значения.
        /// </summary>
        /// <param name="value">
        ///     Значение перечисления. </param>
        /// <returns>
        ///     Текст локализованного названия Enum-значение при наличии, или название Enum-значения.
        /// </returns>
        public static string GetDisplayName([NotNull] Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            return GetMetadata(value).GetDisplayName();
        }

        #endregion public display helpers


        #region private methods

        /// <summary>
        ///     Получить коллекцию типизированных мета-данных перечисления из внутреннего кеша. 
        ///     Если данных в кеше нет, извлечь их из типа и сформировать коллекцию.
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Тип перечисления. 
        /// </typeparam>
        /// <returns>
        ///     Коллекция мета-данных перечисления.
        /// </returns>
        private static IReadOnlyCollection<EnumMetadata<TEnum>> GetInternal<TEnum>() where TEnum : struct
        {
            var info = EnumCache.GetOrAdd(typeof(TEnum), BuildInternal<TEnum>);
            return (IReadOnlyCollection<EnumMetadata<TEnum>>)info;
        }

        /// <summary>
        ///     Получить коллекцию мета-данных перечисления из внутреннего кеша. 
        ///     Если данных в кеше нет, извлечь их из типа и сформировать коллекцию.
        /// </summary>
        /// <param name="enumType">
        ///     Тип перечисления. </param>
        /// <returns>
        ///     Коллекция мета-данных перечисления.
        /// </returns>
        private static IReadOnlyCollection<EnumMetadata> GetInternal(Type enumType)
        {
            return EnumCache.GetOrAdd(enumType, t =>
            {
                var buildMethod = BuildMethod.MakeGenericMethod(enumType);
                var result = (IReadOnlyCollection<EnumMetadata>)buildMethod.Invoke(null, new object[] { enumType });
                return result;
            });
        }

        /// <summary>
        ///     Мета-информация о внутреннем статическом методе для построения коллекции мета-данных перечисления.
        /// </summary>
        private static readonly MethodInfo BuildMethod = typeof(EnumUtils).GetMethod(nameof(BuildInternal),
            BindingFlags.NonPublic | BindingFlags.Static);

        /// <summary>
        ///     Построить коллекцию типизированных мета-данных перечисления указанного типа.
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Тип перечисления. </typeparam>
        /// <param name="enumType">
        ///     Тип перечисления (требуется передавать отдельно для делегата). </param>
        /// <returns>
        ///     Новая коллекция типизированных мета-данных перечисления.
        /// </returns>
        private static IReadOnlyCollection<EnumMetadata<TEnum>> BuildInternal<TEnum>(Type enumType) where TEnum : struct
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
                FieldCache[new EnumKey(enumType, item.Enum)] = item;
            }
            return list;
        }

        #endregion private methods


        #region private types

        /// <summary>
        ///     Unique enum value key for storing meta data in cache.
        /// </summary>
        internal struct EnumKey
        {
            // ReSharper disable NotAccessedField.Local

            private readonly Type _enumType;
            private readonly object _value;
            private readonly int _hash;

            // ReSharper restore NotAccessedField.Local

            /// <summary>
            ///     Create unique enum key by type and value.
            /// </summary>
            /// <param name="enumType">
            ///     Enum concrete type. </param>
            /// <param name="value">
            ///     Enum value. </param>
            public EnumKey(Type enumType, object value)
            {
                _enumType = enumType;
                _value = value;
                _hash = enumType.GetHashCode() ^ value.GetHashCode();
            }

            #region Overrides of ValueType

            /// <summary>
            ///     Override struct hash function.
            /// </summary>
            public override int GetHashCode()
            {
                return _hash;
            }

            #endregion
        }

        #endregion private types
    }
}
