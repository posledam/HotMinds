## `CommonStringExtensions`

Common string extensions.
```csharp
public static class HotMinds.Strings.CommonStringExtensions

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


## `NumberStringExtensions`

Numeric types extensions with string interactions.
```csharp
public static class HotMinds.Strings.NumberStringExtensions

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


