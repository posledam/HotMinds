using System;

namespace HotMinds.Utils
{
    /// <summary>
    ///     Decimal number useful extensions.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        ///     Trim the decimal number to the specified scale and precision. The integral part is truncated on the left. 
        ///     The fractional part is truncated on the right. Nothing is rounded.
        /// </summary>
        /// <param name="d">Source decimal.</param>
        /// <param name="scale">Specify target scale.</param>
        /// <param name="precision">Specify target precision.</param>
        /// <returns>Truncated source decimal to specified scale and precision.</returns>
        /// <exception cref="ArgumentException">
        ///    Precision must be between 1 and 29.
        ///    Scale must be between zero and precision.
        /// </exception>
        public static decimal Trim(this decimal d, int scale, int precision = 29)
        {
            if (precision < 1 || precision > 29) throw new ArgumentException("Precision must be between 1 and 29.", nameof(precision));
            if (scale < 0 || scale > precision) throw new ArgumentException("Scale must be between zero and precision.", nameof(scale));

            var integralCount = precision - scale;

            var integral = decimal.Truncate(d);
            var fraction = d - integral;

            var integralFormat = integral.ToString("##############################");
            if (integralFormat.Length > integralCount)
            {
                integralFormat = integralFormat.Substring(integralFormat.Length - integralCount, integralCount);
            }
            var fractionFormat = fraction.ToString(".##############################");
            if (fractionFormat.Length  > scale + 1)
            {
                fractionFormat = fractionFormat.Substring(0, scale + 1);
            }

            var ret = decimal.Parse(integralFormat + fractionFormat);

            return ret;
        }

        /// <summary>
        ///     Returns a specified number raised to the specified power (simple).
        /// </summary>
        /// <param name="d">Target decimal.</param>
        /// <param name="pow">Integer number that specifies a power.</param>
        /// <returns>
        ///     The number <paramref name="d"/> raised to the power <paramref name="pow"/>.
        /// </returns>
        public static decimal Power(this decimal d, int pow)
        {
            if (pow == 0) return 1m;
            var ret = 1m;
            for (var i = Math.Abs(pow) - 1; i >= 0; i--) ret *= d;
            return pow > 0 ? ret : 1m / ret;
        }
    }
}
