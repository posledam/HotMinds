using System.Globalization;

namespace HotMinds.Extensions
{
    /// <summary>
    ///     Numeric types extensions with string interactions.
    /// </summary>
    public static class NumberStringExtensions
    {
        /// <summary>
        ///     Convert integer to raw string (invariant culture).
        /// </summary>
        /// <param name="n">Source integer.</param>
        /// <returns>Integer string representation (invariant culture).</returns>
        public static string ToRaw(this int n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert nullable integer to raw string (invariant culture).
        /// </summary>
        /// <param name="n">Source nullable integer.</param>
        /// <returns>Integer string representation (invariant culture) or null.</returns>
        public static string ToRaw(this int? n)
        {
            return n?.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert long to raw string (invariant culture).
        /// </summary>
        /// <param name="n">Source long.</param>
        /// <returns>Long string representation (invariant culture).</returns>
        public static string ToRaw(this long n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert nullable long to raw string (invariant culture).
        /// </summary>
        /// <param name="n">Source nullable long.</param>
        /// <returns>Long string representation (invariant culture) or null.</returns>
        public static string ToRaw(this long? n)
        {
            return n?.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert decimal to raw string (invariant culture).
        /// </summary>
        /// <param name="n">Source decimal.</param>
        /// <returns>Decimal string representation (invariant culture).</returns>
        public static string ToRaw(this decimal n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert nullable decimal to raw string (invariant culture).
        /// </summary>
        /// <param name="n">Source nullable decimal.</param>
        /// <returns>Decimal string representation (invariant culture) or null.</returns>
        public static string ToRaw(this decimal? n)
        {
            return n?.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert float to raw string (invariant culture).
        /// </summary>
        /// <param name="n">Source float.</param>
        /// <returns>Float string representation (invariant culture).</returns>
        public static string ToRaw(this float n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert nullable float to raw string (invariant culture).
        /// </summary>
        /// <param name="n">Source nullable float.</param>
        /// <returns>Float string representation (invariant culture) or null.</returns>
        public static string ToRaw(this float? n)
        {
            return n?.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert double to raw string (invariant culture).
        /// </summary>
        /// <param name="n">Source double.</param>
        /// <returns>Double string representation (invariant culture).</returns>
        public static string ToRaw(this double n)
        {
            return n.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Convert nullable double to raw string (invariant culture).
        /// </summary>
        /// <param name="n">Source nullable double.</param>
        /// <returns>Double string representation (invariant culture) or null.</returns>
        public static string ToRaw(this double? n)
        {
            return n?.ToString(CultureInfo.InvariantCulture);
        }
    }
}
