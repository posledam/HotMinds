#HotMinds


##Enums.EnumExtensions
            
Extensions for Enum value and collections of .
        
###Methods


####ForFilter``1(System.Collections.Generic.IEnumerable{``0},System.Boolean,System.Boolean)
Filter collections of by attribute property .
> #####Parameters
> **sequence:** Collection of .

> **filterValue:** Filter parameter for attribute property . If attribute property is empty, use filter strategy by .

> **strict:** Filter strategy for and empty . If true, empty attribute property values would be excluded.

> #####Return value
> Filtered collection.

####ForField``1(System.Collections.Generic.IEnumerable{``0},System.Boolean,System.Boolean)
Filter collections of by attribute property .
> #####Parameters
> **sequence:** Collection of .

> **fieldValue:** Filter parameter for attribute property . If attribute property is empty, use filter strategy by .

> **strict:** Filter strategy for and empty . If true, empty attribute property values would be excluded.

> #####Return value
> Filtered collection.

####GetMetadata(System.Enum)
Get metadata for Enum value.
> #####Parameters
> **value:** Enum value.

> #####Return value
> Enum value metadata.

####GetDisplayName(System.Enum)
Get enum value display name (from attributes or resources).
> #####Parameters
> **value:** Enum value.

> #####Return value
> Enum value display name (or enum value name).

##Enums.EnumMetadata
            
Base enum value meta data class.
        
###Properties

####Enum
Untyped enum value reference.
####Name
Enum value name.
####Order
Enum value order number (for sorting).
####MemberInfo
Enum value member info.
####Display
Enum value [Display] attribute.
####DisplayName
Enum value [DisplayName] attribute. IMPORTANT: don't use , use instead.
####Description
Enum value [Description] attribute. IMPORTANT: don't use , use instead.
####
Concrete Enum type.
###Methods


####GetDisplayName
Get enum value display name (from attributes or resources).

##Enums.EnumMetadata`1
            
Generic enum value meta data class.
            
                Concrete Enum type.
            
        
###Properties

####Value
Concrete Enum type.

##Enums.EnumUtils
            
Enum utils.
        
###Fields

####EnumCache
Internal Enum metadata cache by Enum type.
####FieldCache
Internal Enum metadata cache by special Enum key.
####BuildMethod
Internal MethodInfo referenced to build Enum metadata cache generic method. Used for fast make generic method call via reflection.
###Methods


####GetMetadata``1
Get Enum values metadata collection.
> #####Return value
> Collection of Enum values metadata, ordered by .

####GetMetadata(System.Type)
Get Enum values metadata collection.
> #####Parameters
> **enumType:** Concrete Enum type.

> #####Return value
> Collection of Enum values metadata, ordered by .

####GetMetadata``1(``0)
Get Enum value metadata.
> #####Parameters
> **value:** Enum value.

> #####Return value
> Enum value metadata (typed).

####GetMetadata(System.Enum)
Get Enum value metadata.
> #####Parameters
> **value:** Enum value.

> #####Return value
> Enum value metadata (typed).

####GetMetadata``1(System.String,System.Boolean)
Get Enum value metadata by name.
> #####Parameters
> **name:** Enum value name.

> **ignoreCase:** Ignore case for searching metadata by name.

> #####Return value
> Enum value metadata or null (if not found).

####GetMetadata(System.Type,System.String,System.Boolean)
Get Enum value metadata by name.
> #####Parameters
> **enumType:** Enum type.

> **name:** Enum value name.

> **ignoreCase:** Ignore case for searching metadata by name.

> #####Return value
> Enum value metadata or null (if not found).

####GetDisplayName``1(``0)
Get Enum value display name (from attribute/resources or Enum value name).
> #####Parameters
> **value:** Enum value.

> #####Return value
> Enum value display name (may be localized).

####GetDisplayName(System.Enum)
Get Enum value display name (from attribute/resources or Enum value name).
> #####Parameters
> **value:** Enum value.

> #####Return value
> Enum value display name (may be localized).

####GetInternal``1
Get cached Enum metadata values list by generic Enum type .

####GetInternal(System.Type)
Get cached Enum metadata values list by Enum type .

####BuildInternal``1(System.Type)
Build Enum metadata for Enum type for caching.

####Constructor
Create unique enum key by type and value.
> #####Parameters
> **enumType:** Enum concrete type.

> **value:** Enum value.


####EnumKey.GetHashCode
Override struct hash function.

##Enums.EnumUtils.EnumKey
            
Unique enum value key for storing meta data in cache.
        
###Methods


####Constructor
Create unique enum key by type and value.
> #####Parameters
> **enumType:** Enum concrete type.

> **value:** Enum value.


####GetHashCode
Override struct hash function.