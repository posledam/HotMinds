using System;
using System.Globalization;
using HotMinds.Enums;

namespace HotMinds.Formatting
{
    public struct FileSize : IFormattable
    {
        #region Public constants

        public const long KilobyteSize = 1000;
        public const long MegabyteSize = 1000000;
        public const long GigabyteSize = 1000000000;
        public const long TerabyteSize = 1000000000000;
        public const long PetabyteSize = 1000000000000000;
        public const long ExabyteSize = 1000000000000000000;

        public const long KibibyteSize = 0x400;
        public const long MebibyteSize = 0x100000;
        public const long GibibyteSize = 0x40000000;
        public const long TebibyteSize = 0x10000000000;
        public const long PebibyteSize = 0x4000000000000;
        public const long ExbibyteSize = 0x1000000000000000;

        public const string DecimalFormat = "D";
        public const string BinaryFormat = "I";

        public const string ByteFormat = "B";

        public const string KilobyteFormat = "KB"; // kB
        public const string MegabyteFormat = "MB";
        public const string GigabyteFormat = "GB";
        public const string TerabyteFormat = "TB";
        public const string PetabyteFormat = "PB";
        public const string ExabyteFormat = "EB";

        public const string KibibyteFormat = "KiB";
        public const string MebibyteFormat = "MiB";
        public const string GibibyteFormat = "GiB";
        public const string TebibyteFormat = "TiB";
        public const string PebibyteFormat = "PiB";
        public const string ExbibyteFormat = "EiB";

        // JDEC now not supported
        //public const string JdecKilobyteFormat = "K";
        //public const string JdecMegabyteFormat = "M";
        //public const string JdecGigabyteFormat = "G";
        //public const string JdecTerabyteFormat = "T";
        //public const string JdecPetabyteFormat = "P";
        //public const string JdecExabyteFormat = "E";

        #endregion


        #region Public static settings

        // [ThreadStatic]
        public static string DefaultSizeFormatString = "{0:N2} {1}";

        // [ThreadStatic]
        public static string DefaultFormat = BinaryFormat;

        #endregion


        #region Struct Fields

        private readonly long _bytes;

        #endregion


        #region ctor

        public FileSize(long bytes)
        {
            _bytes = bytes;
        }

        #endregion


        #region Properties

        public long Original => _bytes;

        #endregion


        #region Decimal concrete conversion

        public double Kilobytes => (double)_bytes / KilobyteSize;

        public double Megabytes => (double)_bytes / MegabyteSize;

        public double Gigabytes => (double)_bytes / GigabyteSize;

        public double Terabytes => (double)_bytes / TerabyteSize;

        public double Petabytes => (double)_bytes / PetabyteSize;

        public double Exabytes => (double)_bytes / ExabyteSize;

        #endregion


        #region Binary concrete conversion

        public double Kibibytes => (double)_bytes / KibibyteSize;

        public double Mebibytes => (double)_bytes / MebibyteSize;

        public double Gibibytes => (double)_bytes / GibibyteSize;

        public double Tebibytes => (double)_bytes / TebibyteSize;

        public double Pebibytes => (double)_bytes / PebibyteSize;

        public double Exbibytes => (double)_bytes / ExbibyteSize;

        #endregion


        #region Common conversion

        public double To(FileSizeDecimalSuffix decimalSuffix)
        {
            switch (decimalSuffix)
            {
                case FileSizeDecimalSuffix.Byte: return _bytes;
                case FileSizeDecimalSuffix.Kilobyte: return this.Kilobytes;
                case FileSizeDecimalSuffix.Megabyte: return this.Megabytes;
                case FileSizeDecimalSuffix.Gigabyte: return this.Gigabytes;
                case FileSizeDecimalSuffix.Terabyte: return this.Terabytes;
                case FileSizeDecimalSuffix.Petabyte: return this.Petabytes;
                case FileSizeDecimalSuffix.Exabyte: return this.Exabytes;
                // not supported
                case FileSizeDecimalSuffix.Zettabyte:
                case FileSizeDecimalSuffix.Yottabyte:
                default:
                    throw new ArgumentOutOfRangeException(nameof(decimalSuffix), decimalSuffix, null);
            }
        }

        public double To(FileSizeBinarySuffix binarySuffix)
        {
            switch (binarySuffix)
            {
                case FileSizeBinarySuffix.Byte: return _bytes;
                case FileSizeBinarySuffix.Kibibyte: return this.Kibibytes;
                case FileSizeBinarySuffix.Mebibyte: return this.Mebibytes;
                case FileSizeBinarySuffix.Gibibyte: return this.Gibibytes;
                case FileSizeBinarySuffix.Tebibyte: return this.Tebibytes;
                case FileSizeBinarySuffix.Pebibyte: return this.Pebibytes;
                case FileSizeBinarySuffix.Exbibyte: return this.Exbibytes;
                // not supported
                case FileSizeBinarySuffix.Zebibyte:
                case FileSizeBinarySuffix.Yobibyte:
                default:
                    throw new ArgumentOutOfRangeException(nameof(binarySuffix), binarySuffix, null);
            }
        }

        #endregion


        #region ToString routines

        public string ToString(string format, IFormatProvider provider)
        {
            return this.ToStringInternal(format, provider);
        }

        public string ToString(string format)
        {
            return this.ToStringInternal(format, null);
        }

        public override string ToString()
        {
            return this.ToStringInternal(null, null);
        }

        public string ToString(FileSizeDecimalSuffix decimalSuffix, IFormatProvider provider)
        {
            return FormatInternal(this.To(decimalSuffix), decimalSuffix, provider);
        }

        public string ToString(FileSizeDecimalSuffix decimalSuffix)
        {
            return FormatInternal(this.To(decimalSuffix), decimalSuffix, null);
        }

        public string ToString(FileSizeBinarySuffix binarySuffix, IFormatProvider provider)
        {
            return FormatInternal(this.To(binarySuffix), binarySuffix, provider);
        }

        public string ToString(FileSizeBinarySuffix binarySuffix)
        {
            return FormatInternal(this.To(binarySuffix), binarySuffix, null);
        }

        #endregion


        #region Private methods

        private string ToStringInternal(string format, IFormatProvider provider)
        {
            format = string.IsNullOrEmpty(format) ? DefaultFormat : format.ToUpperInvariant();

            switch (format)
            {
                case ByteFormat:
                    return FormatInternal(_bytes, FileSizeDecimalSuffix.Byte, provider);

                case KilobyteFormat:
                    return FormatInternal(this.Kilobytes, FileSizeDecimalSuffix.Kilobyte, provider);
                case MegabyteFormat:
                    return FormatInternal(this.Megabytes, FileSizeDecimalSuffix.Megabyte, provider);
                case GigabyteFormat:
                    return FormatInternal(this.Gigabytes, FileSizeDecimalSuffix.Gigabyte, provider);
                case TerabyteFormat:
                    return FormatInternal(this.Terabytes, FileSizeDecimalSuffix.Terabyte, provider);
                case PetabyteFormat:
                    return FormatInternal(this.Petabytes, FileSizeDecimalSuffix.Petabyte, provider);
                case ExabyteFormat:
                    return FormatInternal(this.Exabytes, FileSizeDecimalSuffix.Exabyte, provider);

                case KibibyteFormat:
                    return FormatInternal(this.Kibibytes, FileSizeBinarySuffix.Kibibyte, provider);
                case MebibyteFormat:
                    return FormatInternal(this.Mebibytes, FileSizeBinarySuffix.Mebibyte, provider);
                case GibibyteFormat:
                    return FormatInternal(this.Gibibytes, FileSizeBinarySuffix.Gibibyte, provider);
                case TebibyteFormat:
                    return FormatInternal(this.Tebibytes, FileSizeBinarySuffix.Tebibyte, provider);
                case PebibyteFormat:
                    return FormatInternal(this.Pebibytes, FileSizeBinarySuffix.Pebibyte, provider);
                case ExbibyteFormat:
                    return FormatInternal(this.Exbibytes, FileSizeBinarySuffix.Exbibyte, provider);

                case DecimalFormat:
                    var dec = new FileSizeDecimal(_bytes);
                    return FormatInternal(dec.Size, dec.Suffix, provider);
                case BinaryFormat:
                    var bin = new FileSizeBinary(_bytes);
                    return FormatInternal(bin.Size, bin.Suffix, provider);

                default:
                    throw new FormatException($"The \"{format}\" format string is not supported.");
            }
        }

        #endregion


        #region Public static methods

        public static string Format(long bytes, string format, IFormatProvider provider)
        {
            var fileSize = new FileSize(bytes);
            return fileSize.ToStringInternal(format, provider);
        }

        public static string Format(long bytes, string format)
        {
            var fileSize = new FileSize(bytes);
            return fileSize.ToStringInternal(format, null);
        }

        public static string Format(double size, FileSizeDecimalSuffix decimalSuffix, IFormatProvider provider)
        {
            return FormatInternal(size, decimalSuffix, provider);
        }

        public static string Format(double size, FileSizeBinarySuffix binarySuffix, IFormatProvider provider)
        {
            return FormatInternal(size, binarySuffix, provider);
        }

        public static string Format(double size, FileSizeDecimalSuffix decimalSuffix)
        {
            return FormatInternal(size, decimalSuffix, null);
        }

        public static string Format(double size, FileSizeBinarySuffix binarySuffix)
        {
            return FormatInternal(size, binarySuffix, null);
        }

        public static string GetSuffixShortName(FileSizeDecimalSuffix decimalSuffix)
        {
            return EnumUtils.GetMetadata(decimalSuffix).Display.GetShortName();
        }

        public static string GetSuffixShortName(FileSizeBinarySuffix binarySuffix)
        {
            return EnumUtils.GetMetadata(binarySuffix).Display.GetShortName();
        }

        #endregion


        #region Private static methods

        private static string FormatInternal(double size, FileSizeDecimalSuffix decimalSuffix, IFormatProvider provider)
        {
            if (provider == null) provider = CultureInfo.CurrentCulture;
            return string.Format(provider, DefaultSizeFormatString, size, GetSuffixShortName(decimalSuffix));
        }

        private static string FormatInternal(double size, FileSizeBinarySuffix binarySuffix, IFormatProvider provider)
        {
            if (provider == null) provider = CultureInfo.CurrentCulture;
            return string.Format(provider, DefaultSizeFormatString, size, GetSuffixShortName(binarySuffix));
        }

        #endregion
    }
}
