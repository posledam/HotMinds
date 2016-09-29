using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotMinds.Enums
{
    /// <summary>
    ///     Extensions for Enum value and collections of <see cref="EnumMetadata"/>.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     Filter collections of <see cref="EnumMetadata"/> by attribute property <see cref="DisplayAttribute.AutoGenerateFilter"/>.
        /// </summary>
        /// <typeparam name="TEnumData">
        ///     Concrete or base type of <see cref="EnumMetadata"/>.
        /// </typeparam>
        /// <param name="sequence">
        ///     Collection of <typeparamref name="TEnumData"/>. </param>
        /// <param name="filterValue">
        ///     Filter parameter for attribute property <see cref="DisplayAttribute.AutoGenerateFilter"/>.
        ///     If attribute property is empty, use filter strategy by <paramref name="strict"/>.
        /// </param>
        /// <param name="strict">
        ///     Filter strategy for <paramref name="filterValue"/> and empty <see cref="DisplayAttribute.AutoGenerateFilter"/>. 
        ///     If true, empty attribute property values would be excluded.
        /// </param>
        /// <returns>
        ///     Filtered collection. </returns>
        public static IEnumerable<TEnumData> ForFilter<TEnumData>(this IEnumerable<TEnumData> sequence,
            bool filterValue = true, bool strict = false)
            where TEnumData : EnumMetadata
        {
            return
                from p in sequence
                let autoGenerateFilter = p.Display?.GetAutoGenerateFilter()
                where (!strict && autoGenerateFilter == null) || (autoGenerateFilter == filterValue)
                select p;
        }

        /// <summary>
        ///     Filter collections of <see cref="EnumMetadata"/> by attribute property <see cref="DisplayAttribute.AutoGenerateField"/>.
        /// </summary>
        /// <typeparam name="TEnumData">
        ///     Concrete or base type of <see cref="EnumMetadata"/>.
        /// </typeparam>
        /// <param name="sequence">
        ///     Collection of <typeparamref name="TEnumData"/>. </param>
        /// <param name="fieldValue">
        ///     Filter parameter for attribute property <see cref="DisplayAttribute.AutoGenerateField"/>.
        ///     If attribute property is empty, use filter strategy by <paramref name="strict"/>.
        /// </param>
        /// <param name="strict">
        ///     Filter strategy for <paramref name="fieldValue"/> and empty <see cref="DisplayAttribute.AutoGenerateField"/>. 
        ///     If true, empty attribute property values would be excluded.
        /// </param>
        /// <returns>
        ///     Filtered collection. </returns>
        public static IEnumerable<TEnumData> ForField<TEnumData>(this IEnumerable<TEnumData> sequence,
            bool fieldValue = true, bool strict = false)
            where TEnumData : EnumMetadata
        {
            return
                from p in sequence
                let autoGenerateField = p.Display?.GetAutoGenerateField()
                where (!strict && autoGenerateField == null) || (autoGenerateField == fieldValue)
                select p;
        }

        /// <summary>
        ///     Get metadata for Enum value.
        /// </summary>
        /// <param name="value">
        ///     Enum value. </param>
        /// <returns>
        ///     Enum value metadata.
        /// </returns>
        public static EnumMetadata GetMetadata(this Enum value)
        {
            return EnumUtils.GetMetadata(value);
        }

        /// <summary>
        ///     Get enum value display name (from attributes or resources).
        /// </summary>
        /// <param name="value">
        ///     Enum value. </param>
        /// <returns>
        ///     Enum value display name (or enum value name).
        /// </returns>
        public static string GetDisplayName(this Enum value)
        {
            return EnumUtils.GetDisplayName(value);
        }
    }
}
