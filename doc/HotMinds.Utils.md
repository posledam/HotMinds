## `CryptoPasswordGenerator`

Easy to use advanced generator of strong passwords.
```csharp
public class HotMinds.Utils.CryptoPasswordGenerator
    : IDisposable

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `CharsetGroup[]` | CharsetGroups | Setup a new collection of groups of character sets. | 
| `Int32` | MaxLength | The maximum password length. | 
| `Int32` | MinLength | The minimum password length. | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Dispose() |  | 
| `String` | Generate() | Generate new crypto strong password. | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Int32` | LengthLimit | The maximum possible length of the generated password. | 
| `String` | SafeDigitSymbols | The character set of digits. From the set exclude look-alike characters.  This set of characters safe for human use. | 
| `String` | SafeLowerCaseLetterSymbols | The character set of Latin letters in lowercase. From the set exclude look-alike characters.  This set of characters safe for human use. | 
| `String` | SafeSpecialSymbols | The character set of special symbols. The character set does not include look-alike characters.  This set of characters safe for human use and in most cases (e.g., send by network or store in a text files). | 
| `String` | SafeUpperCaseLetterSymbols | The character set of Latin letters in uppercase. From the set exclude look-alike characters.  This set of characters safe for human use. | 


## `CryptoRandomNumberGenerator`

Сrypto-strength random number generator (based on ).
```csharp
public class HotMinds.Utils.CryptoRandomNumberGenerator
    : IDisposable

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Dispose() |  | 
| `Byte` | NextByte() | Get next random byte. | 
| `Int32` | NextInt32() | Get next random integer. | 
| `UInt32` | NextUInt32() | Get next random unsigned integer. | 
| `UInt64` | NextUInt64() | Get next random unsigned long integer. | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Int32` | DefaultBufferSize | The default buffer size for storing generated random bytes. | 


## `FileSize`

```csharp
public struct HotMinds.Utils.FileSize

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `FileSizeBinaryPrefix` | BinaryPrefix |  | 
| `Double` | BinarySize |  | 
| `Int64` | ByteSize |  | 
| `FileSizePrefix` | Prefix |  | 
| `Double` | Size |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | ToBinaryPrefix() |  | 
| `String` | ToPrefix() |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Double` | CalcSize(`Int64` byteSize, `FileSizePrefix` sizePrefix) |  | 
| `Double` | CalcSize(`Int64` byteSize, `FileSizeBinaryPrefix` sizeBinaryPrefix) |  | 
| `FileSizeBinaryPrefix` | DetermineBinaryPrefix(`Int64` byteSize) |  | 
| `FileSizePrefix` | DeterminePrefix(`Int64` byteSize) |  | 
| `String` | GetPrefixName(`FileSizePrefix` prefix) |  | 
| `String` | GetPrefixName(`FileSizeBinaryPrefix` binaryPrefix) |  | 
| `String` | ToBinaryPrefix(`Int64` byteSize) |  | 
| `String` | ToPrefix(`Int64` byteSize) |  | 


## `FileSizeBinaryPrefix`

```csharp
public enum HotMinds.Utils.FileSizeBinaryPrefix
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


## `FileSizePrefix`

```csharp
public enum HotMinds.Utils.FileSizePrefix
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


