using System;

namespace HotMinds.Formatting
{
    public struct FileSizeDecimal
    {
        public readonly double Size;

        public readonly FileSizeDecimalSuffix Suffix;

        public FileSizeDecimal(long bytes)
        {
            this.Suffix = DetermineSuffix(bytes);
            this.Size = CalcSize(bytes, this.Suffix);
        }

        public FileSizeDecimal(double size, FileSizeDecimalSuffix suffix)
        {
            this.Size = size;
            this.Suffix = suffix;
        }

        public static FileSizeDecimalSuffix DetermineSuffix(long bytes)
        {
            bytes = Math.Abs(bytes);
            if (bytes > FileSize.ExabyteSize) return FileSizeDecimalSuffix.Exabyte;
            if (bytes > FileSize.PetabyteSize) return FileSizeDecimalSuffix.Petabyte;
            if (bytes > FileSize.TerabyteSize) return FileSizeDecimalSuffix.Terabyte;
            if (bytes > FileSize.GigabyteSize) return FileSizeDecimalSuffix.Gigabyte;
            if (bytes > FileSize.MegabyteSize) return FileSizeDecimalSuffix.Megabyte;
            if (bytes > FileSize.KilobyteSize) return FileSizeDecimalSuffix.Kilobyte;
            return FileSizeDecimalSuffix.Byte;
        }

        public static double CalcSize(long bytes, FileSizeDecimalSuffix sizeDecimalSuffix)
        {
            switch (sizeDecimalSuffix)
            {
                case FileSizeDecimalSuffix.Byte:
                    return (double)bytes;
                case FileSizeDecimalSuffix.Kilobyte:
                    return (double)bytes / FileSize.KilobyteSize;
                case FileSizeDecimalSuffix.Megabyte:
                    return (double)bytes / FileSize.MegabyteSize;
                case FileSizeDecimalSuffix.Gigabyte:
                    return (double)bytes / FileSize.GigabyteSize;
                case FileSizeDecimalSuffix.Terabyte:
                    return (double)bytes / FileSize.TerabyteSize;
                case FileSizeDecimalSuffix.Petabyte:
                    return (double)bytes / FileSize.PetabyteSize;
                case FileSizeDecimalSuffix.Exabyte:
                    return (double)bytes / FileSize.ExabyteSize;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sizeDecimalSuffix), sizeDecimalSuffix, null);
            }
        }
    }
}