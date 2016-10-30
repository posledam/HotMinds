## `CommonCollectionExtensions`

Common collections (enumerable) extensions.
```csharp
public static class HotMinds.Extensions.CommonCollectionExtensions

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `TValue` | GetOrDefault(this `IEnumerable<KeyValuePair<TKey, TValue>>` dictionary, `TKey` key, `TValue` defaultValue = null) |  | 
| `Boolean` | IsEmpty(this `IEnumerable<T>` collection) |  | 


## `CommonStringExtensions`

Common string extensions.
```csharp
public static class HotMinds.Extensions.CommonStringExtensions

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | DefaultIfNullOrEmpty(this `String` str, `String` defaultValue = null) | Get the default value if the source string is empty or null. | 
| `String` | DefaultIfNullOrWhiteSpace(this `String` str, `String` defaultValue = null) | Get the default value if the source string is empty, whitespaces or null. | 
| `Int32` | GetLength(this `String` str) | Gets the string length, or zero if string is null. | 
| `Boolean` | IsNullOrEmpty(this `String` str) | Extension analog of String.IsNullOrEmpty. | 
| `Boolean` | IsNullOrWhiteSpace(this `String` str) | Extension analog of String.IsNullOrWhiteSpace. | 
| `String` | Limit(this `String` str, `Int32` length) | If the string length is greater than the specified length, truncate it. | 
| `String` | TrimCollapse(this `String` str) | Trim the string and replace whitespaces between words by a single space (collapse spaces). | 
| `String` | Truncate(this `String` str, `Int32` length, `String` ellipsis = …) | If the string length is greater than the specified length, truncate it and append ellipsis. | 


## `DecimalExtensions`

Decimal number useful extensions.
```csharp
public static class HotMinds.Extensions.DecimalExtensions

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `Decimal` | Power(this `Decimal` d, `Int32` pow) | Returns a specified number raised to the specified power (simple). | 
| `Decimal` | PowerOf10(`Int32` pow) | Returns specified power of 10. | 
| `Decimal` | Trim(this `Decimal` d, `Int32` scale, `Int32` precision = 29) | Trim the decimal number to the specified scale and precision. The integral part is truncated on the left.  The fractional part is truncated on the right. Nothing is rounded. | 


## `EnumExtensions`

Extensions for Enum value and collections of .
```csharp
public static class HotMinds.Extensions.EnumExtensions

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IEnumerable<TEnumData>` | ForField(this `IEnumerable<TEnumData>` sequence, `Boolean` fieldValue = True, `Boolean` strict = False) |  | 
| `IEnumerable<TEnumData>` | ForFilter(this `IEnumerable<TEnumData>` sequence, `Boolean` filterValue = True, `Boolean` strict = False) |  | 
| `String` | GetDisplayName(this `Enum` value) | Get enum value display name (from attributes or resources). | 
| `EnumMetadata` | GetMetadata(this `Enum` value) | Get metadata for Enum value. | 


## `LinqExtensions`

Very useful simple LINQ extensions.
```csharp
public static class HotMinds.Extensions.LinqExtensions

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `IEnumerable<IEnumerable<T>>` | Partition(this `IEnumerable<T>` sequence, `Int32` size) |  | 
| `IEnumerable<IEnumerable<T>>` | Split(this `IEnumerable<T>` sequence, `Int32` parts) |  | 


## `NumberStringExtensions`

Numeric types extensions with string interactions.
```csharp
public static class HotMinds.Extensions.NumberStringExtensions

```

Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | ToRaw(this `Int32` n) | Convert integer to raw string (invariant culture). | 
| `String` | ToRaw(this `Nullable<Int32>` n) | Convert integer to raw string (invariant culture). | 
| `String` | ToRaw(this `Int64` n) | Convert integer to raw string (invariant culture). | 
| `String` | ToRaw(this `Nullable<Int64>` n) | Convert integer to raw string (invariant culture). | 
| `String` | ToRaw(this `Decimal` n) | Convert integer to raw string (invariant culture). | 
| `String` | ToRaw(this `Nullable<Decimal>` n) | Convert integer to raw string (invariant culture). | 
| `String` | ToRaw(this `Single` n) | Convert integer to raw string (invariant culture). | 
| `String` | ToRaw(this `Nullable<Single>` n) | Convert integer to raw string (invariant culture). | 
| `String` | ToRaw(this `Double` n) | Convert integer to raw string (invariant culture). | 
| `String` | ToRaw(this `Nullable<Double>` n) | Convert integer to raw string (invariant culture). | 


