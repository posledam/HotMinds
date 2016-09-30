using JetBrains.Annotations;

namespace HotMinds.Strings
{
    /// <summary>
    ///     Common string extensions.
    /// </summary>
    public static class CommonStringExtensions
    {
        /// <summary>
        ///     Extension analog of String.IsNullOrEmpty.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <returns>True if <paramref name="str"/> is null or empty string.</returns>
        public static bool IsNullOrEmpty([CanBeNull] this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        ///     Extension analog of String.IsNullOrWhiteSpace.
        /// </summary>
        /// <param name="str">Source string.</param>
        /// <returns>True if <paramref name="str"/> is null, empty or whitespace string.</returns>
        public static bool IsNullOrWhiteSpace([CanBeNull] this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
