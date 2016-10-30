using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HotMinds.Enums;
using HotMinds.Resources;

namespace HotMinds.Utils
{
    public struct FileSize
    {
        private readonly long _byteSize;

        public FileSize(long byteSize)
        {
            _byteSize = byteSize;
            this.Prefix = DeterminePrefix(byteSize);
            this.BinaryPrefix = DetermineBinaryPrefix(byteSize);
            this.Size = CalcSize(byteSize, this.Prefix);
            this.BinarySize = CalcSize(byteSize, this.BinaryPrefix);
        }

        public long ByteSize => _byteSize;

        public FileSizePrefix Prefix { get; }

        public FileSizeBinaryPrefix BinaryPrefix { get; }

        public double Size { get; }

        public double BinarySize { get; }

        public string ToPrefix()
        {
            return $"{Size:##.###} {GetPrefixName(this.Prefix)}";
        }

        public string ToBinaryPrefix()
        {
            return $"{BinarySize:##.###} {GetPrefixName(this.BinaryPrefix)}";
        }

        public static string ToPrefix(long byteSize)
        {
            var size = new FileSize(byteSize);
            return size.ToPrefix();
        }

        public static string ToBinaryPrefix(long byteSize)
        {
            var size = new FileSize(byteSize);
            return size.ToBinaryPrefix();
        }

        public static double CalcSize(long byteSize, FileSizePrefix sizePrefix)
        {
            switch (sizePrefix)
            {
                case FileSizePrefix.Byte:
                    return (double)byteSize;
                case FileSizePrefix.Kilobyte:
                    return (double)byteSize / 1000L;
                case FileSizePrefix.Megabyte:
                    return (double)byteSize / 1000000L;
                case FileSizePrefix.Gigabyte:
                    return (double)byteSize / 1000000000L;
                case FileSizePrefix.Terabyte:
                    return (double)byteSize / 1000000000000L;
                case FileSizePrefix.Petabyte:
                    return (double)byteSize / 1000000000000000L;
                case FileSizePrefix.Exabyte:
                    return (double)byteSize / 1000000000000000000L;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sizePrefix), sizePrefix, null);
            }
        }

        public static double CalcSize(long byteSize, FileSizeBinaryPrefix sizeBinaryPrefix)
        {
            switch (sizeBinaryPrefix)
            {
                case FileSizeBinaryPrefix.Byte:
                    return (double)byteSize;
                case FileSizeBinaryPrefix.Kibibyte:
                    return (double)byteSize / 1024L;
                case FileSizeBinaryPrefix.Mebibyte:
                    return (double)byteSize / 1048576L;
                case FileSizeBinaryPrefix.Gibibyte:
                    return (double)byteSize / 1073741824L;
                case FileSizeBinaryPrefix.Tebibyte:
                    return (double)byteSize / 1099511627776L;
                case FileSizeBinaryPrefix.Pebibyte:
                    return (double)byteSize / 1125899906842624L;
                case FileSizeBinaryPrefix.Exbibyte:
                    return (double)byteSize / 1152921504606846976L;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sizeBinaryPrefix), sizeBinaryPrefix, null);
            }
        }

        public static FileSizePrefix DeterminePrefix(long byteSize)
        {
            if (byteSize > 1000000000000000000L) return FileSizePrefix.Exabyte;
            if (byteSize > 1000000000000000L) return FileSizePrefix.Petabyte;
            if (byteSize > 1000000000000L) return FileSizePrefix.Terabyte;
            if (byteSize > 1000000000L) return FileSizePrefix.Gigabyte;
            if (byteSize > 1000000L) return FileSizePrefix.Megabyte;
            if (byteSize > 1000L) return FileSizePrefix.Kilobyte;
            return FileSizePrefix.Byte;
        }

        public static FileSizeBinaryPrefix DetermineBinaryPrefix(long byteSize)
        {
            if (byteSize > 1152921504606846976L) return FileSizeBinaryPrefix.Exbibyte;
            if (byteSize > 1125899906842624L) return FileSizeBinaryPrefix.Pebibyte;
            if (byteSize > 1099511627776L) return FileSizeBinaryPrefix.Tebibyte;
            if (byteSize > 1073741824L) return FileSizeBinaryPrefix.Gibibyte;
            if (byteSize > 1048576L) return FileSizeBinaryPrefix.Mebibyte;
            if (byteSize > 1024L) return FileSizeBinaryPrefix.Kibibyte;
            return FileSizeBinaryPrefix.Byte;
        }

        private static Dictionary<FileSizePrefix, string> _prefixCache;

        public static string GetPrefixName(FileSizePrefix prefix)
        {
            if (_prefixCache == null)
            {
                _prefixCache = EnumUtils
                    .GetMetadata<FileSizePrefix>()
                    .ToDictionary(k => k.Value, v => v.Display.GetShortName());
            }
            return _prefixCache[prefix];
        }

        private static Dictionary<FileSizeBinaryPrefix, string> _binaryPrefixCache;

        public static string GetPrefixName(FileSizeBinaryPrefix binaryPrefix)
        {
            if (_binaryPrefixCache == null)
            {
                _binaryPrefixCache = EnumUtils
                    .GetMetadata<FileSizeBinaryPrefix>()
                    .ToDictionary(k => k.Value, v => v.Display.GetShortName());
            }
            return _binaryPrefixCache[binaryPrefix];
        }
    }

    public enum FileSizePrefix
    {
        [Display(ResourceType = typeof(Annotations), Name = "FileSizePrefix_Byte", ShortName = "FileSizePrefix_Byte_Short")]
        Byte = 0,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizePrefix_Kilobyte", ShortName = "FileSizePrefix_Kilobyte_Short")]
        Kilobyte = 1,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizePrefix_Megabyte", ShortName = "FileSizePrefix_Megabyte_Short")]
        Megabyte = 2,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizePrefix_Gigabyte", ShortName = "FileSizePrefix_Gigabyte_Short")]
        Gigabyte = 3,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizePrefix_Terabyte", ShortName = "FileSizePrefix_Terabyte_Short")]
        Terabyte = 4,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizePrefix_Petabyte", ShortName = "FileSizePrefix_Petabyte_Short")]
        Petabyte = 5,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizePrefix_Exabyte", ShortName = "FileSizePrefix_Exabyte_Short")]
        Exabyte = 6,

        // now not supported

        [Display(ResourceType = typeof(Annotations), Name = "FileSizePrefix_Zettabyte", ShortName = "FileSizePrefix_Zettabyte_Short")]
        Zettabyte = 7,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizePrefix_Yottabyte", ShortName = "FileSizePrefix_Yottabyte_Short")]
        Yottabyte = 8
    }

    public enum FileSizeBinaryPrefix
    {
        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinaryPrefix_Byte", ShortName = "FileSizeBinaryPrefix_Byte_Short")]
        Byte = 0,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinaryPrefix_Kibibyte", ShortName = "FileSizeBinaryPrefix_Kibibyte_Short")]
        Kibibyte = 1,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinaryPrefix_Mebibyte", ShortName = "FileSizeBinaryPrefix_Mebibyte_Short")]
        Mebibyte = 2,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinaryPrefix_Gibibyte", ShortName = "FileSizeBinaryPrefix_Gibibyte_Short")]
        Gibibyte = 3,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinaryPrefix_Tebibyte", ShortName = "FileSizeBinaryPrefix_Tebibyte_Short")]
        Tebibyte = 4,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinaryPrefix_Pebibyte", ShortName = "FileSizeBinaryPrefix_Pebibyte_Short")]
        Pebibyte = 5,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinaryPrefix_Exbibyte", ShortName = "FileSizeBinaryPrefix_Exbibyte_Short")]
        Exbibyte = 6,

        // now not supported

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinaryPrefix_Zebibyte", ShortName = "FileSizeBinaryPrefix_Zebibyte_Short")]
        Zebibyte = 7,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinaryPrefix_Yobibyte", ShortName = "FileSizeBinaryPrefix_Yobibyte_Short")]
        Yobibyte = 8
    }
}
