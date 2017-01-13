using System.Text;

namespace HotMinds.Cryptography
{
    /// <summary>
    ///     Some hash (and not only) utils.
    /// </summary>
    public static class HashUtils
    {
        /// <summary>
        ///     Convert a byte array to a string of hexadecimal numbers.
        /// </summary>
        /// <param name="bytes">Source byte array.</param>
        /// <returns>Hexadecimal representation of byte array.</returns>
        public static string ByteArrayToHexString(byte[] bytes)
        {
            var result = new StringBuilder(bytes.Length * 2);
            const string hexAlphabet = "0123456789ABCDEF";

            foreach (var b in bytes)
            {
                result.Append(hexAlphabet[b >> 4]);
                result.Append(hexAlphabet[b & 0xF]);
            }

            return result.ToString();
        }

        /// <summary>
        ///     Convert a string of hexadecimal numbers to a byte array.
        /// </summary>
        /// <param name="hex">A string containing the hexadecimal representation of byte array.</param>
        /// <returns>Byte array from string.</returns>
        public static byte[] HexStringToByteArray(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            var hexValue = new[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
               0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
               0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < hex.Length; i += 2, x += 1)
            {
                bytes[x] = (byte)(hexValue[char.ToUpper(hex[i + 0]) - '0'] << 4 |
                                  hexValue[char.ToUpper(hex[i + 1]) - '0']);
            }

            return bytes;
        }
    }
}
