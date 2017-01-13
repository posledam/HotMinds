using System.ComponentModel.DataAnnotations;
using HotMinds.Resources;

namespace HotMinds.Formatting
{
    public enum FileSizeDecimalSuffix
    {
        [Display(ResourceType = typeof(Annotations), Name = "FileSizeDecimalSuffix_Byte", ShortName = "FileSizeDecimalSuffix_Byte_Short")]
        Byte = 0,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeDecimalSuffix_Kilobyte", ShortName = "FileSizeDecimalSuffix_Kilobyte_Short")]
        Kilobyte = 1,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeDecimalSuffix_Megabyte", ShortName = "FileSizeDecimalSuffix_Megabyte_Short")]
        Megabyte = 2,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeDecimalSuffix_Gigabyte", ShortName = "FileSizeDecimalSuffix_Gigabyte_Short")]
        Gigabyte = 3,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeDecimalSuffix_Terabyte", ShortName = "FileSizeDecimalSuffix_Terabyte_Short")]
        Terabyte = 4,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeDecimalSuffix_Petabyte", ShortName = "FileSizeDecimalSuffix_Petabyte_Short")]
        Petabyte = 5,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeDecimalSuffix_Exabyte", ShortName = "FileSizeDecimalSuffix_Exabyte_Short")]
        Exabyte = 6,

        // now not supported

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeDecimalSuffix_Zettabyte", ShortName = "FileSizeDecimalSuffix_Zettabyte_Short")]
        Zettabyte = 7,

        [Display(ResourceType = typeof(Annotations), Name = "FileSizeDecimalSuffix_Yottabyte", ShortName = "FileSizeDecimalSuffix_Yottabyte_Short")]
        Yottabyte = 8
    }
}