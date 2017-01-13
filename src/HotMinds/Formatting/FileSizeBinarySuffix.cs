using System.ComponentModel.DataAnnotations;
using HotMinds.Resources;

namespace HotMinds.Formatting
{
    public enum FileSizeBinarySuffix
    {
        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinarySuffix_Byte", ShortName = "FileSizeBinarySuffix_Byte_Short")]
        Byte = 0,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinarySuffix_Kibibyte", ShortName = "FileSizeBinarySuffix_Kibibyte_Short")]
        Kibibyte = 1,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinarySuffix_Mebibyte", ShortName = "FileSizeBinarySuffix_Mebibyte_Short")]
        Mebibyte = 2,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinarySuffix_Gibibyte", ShortName = "FileSizeBinarySuffix_Gibibyte_Short")]
        Gibibyte = 3,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinarySuffix_Tebibyte", ShortName = "FileSizeBinarySuffix_Tebibyte_Short")]
        Tebibyte = 4,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinarySuffix_Pebibyte", ShortName = "FileSizeBinarySuffix_Pebibyte_Short")]
        Pebibyte = 5,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinarySuffix_Exbibyte", ShortName = "FileSizeBinarySuffix_Exbibyte_Short")]
        Exbibyte = 6,

        // now not supported

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinarySuffix_Zebibyte", ShortName = "FileSizeBinarySuffix_Zebibyte_Short")]
        Zebibyte = 7,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeBinarySuffix_Yobibyte", ShortName = "FileSizeBinarySuffix_Yobibyte_Short")]
        Yobibyte = 8
    }
}