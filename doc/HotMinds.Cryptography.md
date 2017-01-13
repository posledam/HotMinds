## `CryptoPasswordGenerator`

Easy to use advanced generator of strong passwords.
```csharp
public class HotMinds.Cryptography.CryptoPasswordGenerator
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
public class HotMinds.Cryptography.CryptoRandomNumberGenerator
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


## `HashStreamWrapper`

Stream wrapper for calculating the hash on the fly when reading or writing.
```csharp
public class HotMinds.Cryptography.HashStreamWrapper
    : Stream, IDisposable

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `Boolean` | CanRead |  | 
| `Boolean` | CanSeek |  | 
| `Boolean` | CanWrite |  | 
| `Int64` | Length |  | 
| `Int64` | Position |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Dispose(`Boolean` disposing) |  | 
| `void` | Flush() |  | 
| `Byte[]` | GetHash() | Complete the hash calculation and return the result. | 
| `String` | GetHashString() | Complete the hash calculation and return the result in a string with hexadecimal representation. | 
| `Int32` | Read(`Byte[]` buffer, `Int32` offset, `Int32` count) |  | 
| `Int64` | Seek(`Int64` offset, `SeekOrigin` origin) |  | 
| `void` | SetLength(`Int64` value) |  | 
| `void` | Write(`Byte[]` buffer, `Int32` offset, `Int32` count) |  | 


## `HashUtils`

Some hash (and not only) utils.
```csharp
public static class HotMinds.Cryptography.HashUtils

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | ByteArrayToHexString(`Byte[]` bytes) | Convert a byte array to a string of hexadecimal numbers. | 
| `Byte[]` | HexStringToByteArray(`String` hex) | Convert a string of hexadecimal numbers to a byte array. | 


