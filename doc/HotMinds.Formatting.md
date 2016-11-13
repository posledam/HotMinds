## `FileSize`

```csharp
public struct HotMinds.Formatting.FileSize
    : IFormattable

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | Exabytes |  | 
| `Double` | Exbibytes |  | 
| `Double` | Gibibytes |  | 
| `Double` | Gigabytes |  | 
| `Double` | Kibibytes |  | 
| `Double` | Kilobytes |  | 
| `Double` | Mebibytes |  | 
| `Double` | Megabytes |  | 
| `Int64` | Original |  | 
| `Double` | Pebibytes |  | 
| `Double` | Petabytes |  | 
| `Double` | Tebibytes |  | 
| `Double` | Terabytes |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | To(`FileSizeDecimalSuffix` decimalSuffix) |  | 
| `Double` | To(`FileSizeBinarySuffix` binarySuffix) |  | 
| `String` | ToString(`String` format, `IFormatProvider` provider) |  | 
| `String` | ToString(`String` format) |  | 
| `String` | ToString() |  | 
| `String` | ToString(`FileSizeDecimalSuffix` decimalSuffix, `IFormatProvider` provider) |  | 
| `String` | ToString(`FileSizeDecimalSuffix` decimalSuffix) |  | 
| `String` | ToString(`FileSizeBinarySuffix` binarySuffix, `IFormatProvider` provider) |  | 
| `String` | ToString(`FileSizeBinarySuffix` binarySuffix) |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | BinaryFormat |  | 
| `String` | ByteFormat |  | 
| `String` | DecimalFormat |  | 
| `String` | DefaultFormat |  | 
| `String` | DefaultSizeFormatString |  | 
| `String` | ExabyteFormat |  | 
| `Int64` | ExabyteSize |  | 
| `String` | ExbibyteFormat |  | 
| `Int64` | ExbibyteSize |  | 
| `String` | GibibyteFormat |  | 
| `Int64` | GibibyteSize |  | 
| `String` | GigabyteFormat |  | 
| `Int64` | GigabyteSize |  | 
| `String` | KibibyteFormat |  | 
| `Int64` | KibibyteSize |  | 
| `String` | KilobyteFormat |  | 
| `Int64` | KilobyteSize |  | 
| `String` | MebibyteFormat |  | 
| `Int64` | MebibyteSize |  | 
| `String` | MegabyteFormat |  | 
| `Int64` | MegabyteSize |  | 
| `String` | PebibyteFormat |  | 
| `Int64` | PebibyteSize |  | 
| `String` | PetabyteFormat |  | 
| `Int64` | PetabyteSize |  | 
| `String` | TebibyteFormat |  | 
| `Int64` | TebibyteSize |  | 
| `String` | TerabyteFormat |  | 
| `Int64` | TerabyteSize |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | Format(`Int64` bytes, `String` format, `IFormatProvider` provider) |  | 
| `String` | Format(`Int64` bytes, `String` format) |  | 
| `String` | Format(`Double` size, `FileSizeDecimalSuffix` decimalSuffix, `IFormatProvider` provider) |  | 
| `String` | Format(`Double` size, `FileSizeBinarySuffix` binarySuffix, `IFormatProvider` provider) |  | 
| `String` | Format(`Double` size, `FileSizeDecimalSuffix` decimalSuffix) |  | 
| `String` | Format(`Double` size, `FileSizeBinarySuffix` binarySuffix) |  | 
| `String` | GetSuffixShortName(`FileSizeDecimalSuffix` decimalSuffix) |  | 
| `String` | GetSuffixShortName(`FileSizeBinarySuffix` binarySuffix) |  | 


## `FileSizeBinary`

```csharp
public struct HotMinds.Formatting.FileSizeBinary

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | Size |  | 
| `FileSizeBinarySuffix` | Suffix |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | CalcSize(`Int64` bytes, `FileSizeBinarySuffix` sizeBinarySuffix) |  | 
| `FileSizeBinarySuffix` | DetermineSuffix(`Int64` bytes) |  | 


## `FileSizeBinarySuffix`

```csharp
public enum HotMinds.Formatting.FileSizeBinarySuffix
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | Byte |  | 
| `1` | Kibibyte |  | 
| `2` | Mebibyte |  | 
| `3` | Gibibyte |  | 
| `4` | Tebibyte |  | 
| `5` | Pebibyte |  | 
| `6` | Exbibyte |  | 
| `7` | Zebibyte |  | 
| `8` | Yobibyte |  | 


## `FileSizeDecimal`

```csharp
public struct HotMinds.Formatting.FileSizeDecimal

```

Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | Size |  | 
| `FileSizeDecimalSuffix` | Suffix |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | CalcSize(`Int64` bytes, `FileSizeDecimalSuffix` sizeDecimalSuffix) |  | 
| `FileSizeDecimalSuffix` | DetermineSuffix(`Int64` bytes) |  | 


## `FileSizeDecimalSuffix`

```csharp
public enum HotMinds.Formatting.FileSizeDecimalSuffix
    : Enum, IComparable, IFormattable, IConvertible

```

Enum

| Value | Name | Summary | 
| --- | --- | --- | 
| `0` | Byte |  | 
| `1` | Kilobyte |  | 
| `2` | Megabyte |  | 
| `3` | Gigabyte |  | 
| `4` | Terabyte |  | 
| `5` | Petabyte |  | 
| `6` | Exabyte |  | 
| `7` | Zettabyte |  | 
| `8` | Yottabyte |  | 


