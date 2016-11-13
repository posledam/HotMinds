using System;

namespace HotMinds.Formatting
{
    public struct FileSizeBinary
    {
        public readonly double Size;

        public readonly FileSizeBinarySuffix Suffix;

        public FileSizeBinary(long bytes)
        {
            this.Suffix = DetermineSuffix(bytes);
            this.Size = CalcSize(bytes, this.Suffix);
        }

        public FileSizeBinary(double size, FileSizeBinarySuffix suffix)
        {
            this.Size = size;
            this.Suffix = suffix;
        }

        public static FileSizeBinarySuffix DetermineSuffix(long bytes)
        {
            bytes = Math.Abs(bytes);
            if (bytes > FileSize.ExbibyteSize) return FileSizeBinarySuffix.Exbibyte;
            if (bytes > FileSize.PebibyteSize) return FileSizeBinarySuffix.Pebibyte;
            if (bytes > FileSize.TebibyteSize) return FileSizeBinarySuffix.Tebibyte;
            if (bytes > FileSize.GibibyteSize) return FileSizeBinarySuffix.Gibibyte;
            if (bytes > FileSize.MebibyteSize) return FileSizeBinarySuffix.Mebibyte;
            if (bytes > FileSize.KibibyteSize) return FileSizeBinarySuffix.Kibibyte;
            return FileSizeBinarySuffix.Byte;
        }

        public static double CalcSize(long bytes, FileSizeBinarySuffix sizeBinarySuffix)
        {
            switch (sizeBinarySuffix)
            {
                case FileSizeBinarySuffix.Byte:
                    return (double)bytes;
                case FileSizeBinarySuffix.Kibibyte:
                    return (double)bytes / FileSize.KibibyteSize;
                case FileSizeBinarySuffix.Mebibyte:
                    return (double)bytes / FileSize.MebibyteSize;
                case FileSizeBinarySuffix.Gibibyte:
                    return (double)bytes / FileSize.GibibyteSize;
                case FileSizeBinarySuffix.Tebibyte:
                    return (double)bytes / FileSize.TebibyteSize;
                case FileSizeBinarySuffix.Pebibyte:
                    return (double)bytes / FileSize.PebibyteSize;
                case FileSizeBinarySuffix.Exbibyte:
                    return (double)bytes / FileSize.ExbibyteSize;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sizeBinarySuffix), sizeBinarySuffix, null);
            }
        }
    }
}