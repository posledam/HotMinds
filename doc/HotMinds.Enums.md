## `EnumMetadata`

Base enum value meta data class.
```csharp
public abstract class HotMinds.Enums.EnumMetadata

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `DisplayAttribute` | Display | Enum value [Display] attribute. | 
| `Enum` | Enum | Untyped enum value reference. | 
| `MemberInfo` | MemberInfo | Enum value member info. | 
| `String` | Name | Enum value name. | 
| `Int32` | Order | Enum value order number (for sorting). | 


Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | GetDisplayName() | Get enum value display name (from attributes or resources). | 


## `EnumMetadata<TEnum>`

Generic enum value meta data class.
```csharp
public class HotMinds.Enums.EnumMetadata<TEnum>
    : EnumMetadata

```

Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| `TEnum` | Value | Concrete Enum type. | 


## `EnumUtils`

Enum utils.
```csharp
public static class HotMinds.Enums.EnumUtils

```

Static Fields

| Type | Name | Summary | 
| --- | --- | --- | 
| `ConcurrentDictionary<Type, IReadOnlyCollection<EnumMetadata>>` | EnumCache |  | 
| `Dictionary<EnumKey, EnumMetadata>` | FieldCache |  | 


Static Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `String` | GetDisplayName(`TEnum` value) | Get Enum value display name (from attribute/resources or Enum value name). | 
| `String` | GetDisplayName(`Enum` value) | Get Enum value display name (from attribute/resources or Enum value name). | 
| `IReadOnlyCollection<EnumMetadata<TEnum>>` | GetMetadata() | Get Enum values metadata collection. | 
| `IReadOnlyCollection<EnumMetadata>` | GetMetadata(`Type` enumType) | Get Enum values metadata collection. | 
| `EnumMetadata<TEnum>` | GetMetadata(`TEnum` value) | Get Enum values metadata collection. | 
| `EnumMetadata` | GetMetadata(`Enum` value) | Get Enum values metadata collection. | 
| `EnumMetadata<TEnum>` | GetMetadata(`String` name, `Boolean` ignoreCase = False) | Get Enum values metadata collection. | 
| `EnumMetadata` | GetMetadata(`Type` enumType, `String` name, `Boolean` ignoreCase = False) | Get Enum values metadata collection. | 


