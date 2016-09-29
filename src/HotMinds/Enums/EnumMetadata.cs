using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HotMinds.Enums
{
    /// <summary>
    ///     Base enum value meta data class.
    /// </summary>
    public abstract class EnumMetadata
    {
        /// <summary>
        ///     Untyped enum value reference.
        /// </summary>
        public Enum Enum { get; internal set; }

        /// <summary>
        ///     Enum value name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     Enum value order number (for sorting).
        /// </summary>
        public int Order { get; internal set; }

        /// <summary>
        ///     Enum value member info.
        /// </summary>
        public MemberInfo MemberInfo { get; internal set; }

        /// <summary>
        ///     Enum value [Display] attribute.
        /// </summary>
        public DisplayAttribute Display { get; internal set; }

        /// <summary>
        ///     Enum value [DisplayName] attribute.
        /// </summary>
        /// <remarks>
        ///     IMPORTANT: don't use <see cref="DisplayNameAttribute"/>, use <see cref="DisplayAttribute"/> instead.
        /// </remarks>
        [Obsolete("Don't use DisplayNameAttribute, use DisplayAttrubute instead.")]
        public DisplayNameAttribute DisplayName { get; set; }

        /// <summary>
        ///     Enum value [Description] attribute.
        /// </summary>
        /// <remarks>
        ///     IMPORTANT: don't use <see cref="DescriptionAttribute"/>, use <see cref="DisplayAttribute"/> instead.
        /// </remarks>
        [Obsolete("Don't use DescriptionAttribute, use DisplayAttrubute instead.")]
        public DescriptionAttribute Description { get; set; }

        /// <summary>
        ///     Get enum value display name (from attributes or resources).
        /// </summary>
        public string GetDisplayName()
        {
#pragma warning disable 618
            return Display?.GetName()
                ?? DisplayName?.DisplayName // for support old code only
                ?? Description?.Description // for support old code only
                ?? Name;
#pragma warning restore 618
        }
    }

    /// <summary>
    ///     Generic enum value meta data class.
    /// </summary>
    /// <typeparam name="TEnum">
    ///     Concrete Enum type.
    /// </typeparam>
    public class EnumMetadata<TEnum> : EnumMetadata
        where TEnum : struct
    {
        /// <summary>
        ///     Concrete Enum type.
        /// </summary>
        public TEnum Value { get; internal set; }
    }
}