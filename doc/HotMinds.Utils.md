## `CryptoPasswordGenerator`

```csharp
public class HotMinds.Utils.CryptoPasswordGenerator
    : IDisposable

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `CharsetGroup[]` | CharsetGroups |  | 
| `Int32` | MaxLength |  | 
| `Int32` | MinLength |  | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Dispose() |  | 
| `String` | Generate() |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Int32` | LengthLimit |  | 
| `String` | SafeDigitSymbols |  | 
| `String` | SafeLowerCaseLetterSymbols |  | 
| `String` | SafeSpecialSymbols |  | 
| `String` | SafeUpperCaseLetterSymbols |  | 


## `CryptoRandomNumberGenerator`

```csharp
public class HotMinds.Utils.CryptoRandomNumberGenerator
    : IDisposable

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Dispose() |  | 
| `Byte` | NextByte() |  | 
| `UInt32` | NextUInt32() |  | 
| `UInt64` | NextUInt64() |  | 


Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `Int32` | DefaultBufferSize |  | 


