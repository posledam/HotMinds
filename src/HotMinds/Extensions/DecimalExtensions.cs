using System;

namespace HotMinds.Extensions
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
        ///    Scale must be &gt;= 0 and &lt; <paramref name="precision"/>.
        /// </exception>
        public static decimal Trim(this decimal d, int scale, int precision = 29)
        {
            if (precision < 1 || precision > 29)
                throw new ArgumentException("Precision must be between 1 and 29.", nameof(precision));
            if (scale < 0 || scale >= precision)
                throw new ArgumentException("The scale must be equal to or greater than 0 and less than precision.", nameof(scale));

            if (scale == 0 && precision == 29)
            {
                return decimal.Truncate(d);
            }

            var integralCount = precision - scale;
            var integralPower = PowerOf10(integralCount);
            var integral = decimal.Truncate(d);

            if (scale == 0)
            {
                return decimal.Remainder(integral, integralPower);
            }

            var scalePower = PowerOf10(scale);
            var fraction = d - integral;

            var result = decimal.Remainder(integral, integralPower) + decimal.Truncate(fraction * scalePower) / scalePower;

            return result;
        }

        /// <summary>
        ///     Returns specified power of 10.
        /// </summary>
        /// <param name="pow">
        ///     Integer number that specifies a power (-28..28).
        /// </param>
        /// <returns>
        ///     Decimal with specified power of 10.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Power must be in range of -28..28.
        /// </exception>
        public static decimal PowerOf10(int pow)
        {
            if (pow < 0) return 1m / PowerOf10(-pow);
            switch (pow)
            {
                case 0: return 1m;
                case 1: return 10m;
                case 2: return 100m;
                case 3: return 1000m;
                case 4: return 10000m;
                case 5: return 100000m;
                case 6: return 1000000m;
                case 7: return 10000000m;
                case 8: return 100000000m;
                case 9: return 1000000000m;
                case 10: return 10000000000m;
                case 11: return 100000000000m;
                case 12: return 1000000000000m;
                case 13: return 10000000000000m;
                case 14: return 100000000000000m;
                case 15: return 1000000000000000m;
                case 16: return 10000000000000000m;
                case 17: return 100000000000000000m;
                case 18: return 1000000000000000000m;
                case 19: return 10000000000000000000m;
                case 20: return 100000000000000000000m;
                case 21: return 1000000000000000000000m;
                case 22: return 10000000000000000000000m;
                case 23: return 100000000000000000000000m;
                case 24: return 1000000000000000000000000m;
                case 25: return 10000000000000000000000000m;
                case 26: return 100000000000000000000000000m;
                case 27: return 1000000000000000000000000000m;
                case 28: return 10000000000000000000000000000m;
                default:
                    throw new ArgumentException("Power must be in range of -28..28.", nameof(pow));
            }
        }

        /// <summary>
        ///     Returns a specified number raised to the specified power (simple).
        /// </summary>
        /// <param name="d">Target decimal.</param>
        /// <param name="pow">Integer number that specifies a power.</param>
        /// <returns>
        ///     The number <paramref name="d"/> raised to the power <paramref name="pow"/>.
        /// </returns>
        /// <exception cref="OverflowException">
        ///     Result of power was either too large or too small for a Decimal.
        /// </exception>
        public static decimal Power(this decimal d, int pow)
        {
            if (pow == 0) return 1m;
            var ret = 1m;
            for (var i = Math.Abs(pow) - 1; i >= 0; i--) ret *= d;
            return pow > 0 ? ret : 1m / ret;
        }
    }
}
