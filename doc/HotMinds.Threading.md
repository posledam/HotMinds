## `ReadLockDisposableBlock`

Disposable pattern for read lock of  object.
```csharp
public struct HotMinds.Threading.ReadLockDisposableBlock
    : IDisposable

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Dispose() | Reduces the recursion count for read mode, and exits read mode if the resulting count is 0 (zero). | 


## `UpgradeableReadLockDisposableBlock`

Disposable pattern for upgradeable read lock of  object.
```csharp
public struct HotMinds.Threading.UpgradeableReadLockDisposableBlock
    : IDisposable

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Dispose() | Reduces the recursion count for upgradeable mode, and exits upgradeable mode if the resulting count is 0 (zero). | 
| `WriteLockDisposableBlock` | UseWrite() | Create disposable object and tries to enter the lock in write mode. | 


## `WriteLockDisposableBlock`

Disposable pattern for write lock of  object.
```csharp
public struct HotMinds.Threading.WriteLockDisposableBlock
    : IDisposable

```

Methods

| Type | Name | Summary | 
| --- | --- | --- | 
| `void` | Dispose() | Reduces the recursion count for write mode, and exits write mode if the resulting count is 0 (zero). | 


