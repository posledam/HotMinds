using System;
using System.Threading;
using JetBrains.Annotations;

namespace HotMinds.Threading
{
    /// <summary>
    ///     Disposable pattern for upgradeable read lock of <see cref="ReaderWriterLockSlim"/> object.
    /// </summary>
    public struct UpgradeableReadLockDisposableBlock : IDisposable
    {
        private readonly ReaderWriterLockSlim _readerWriterLock;

        /// <summary>
        ///     Create disposable object and tries to enter the lock in upgradeable read mode.
        /// </summary>
        /// <param name="readerWriterLock">Lock object.</param>
        public UpgradeableReadLockDisposableBlock([NotNull] ReaderWriterLockSlim readerWriterLock)
        {
            if (readerWriterLock == null) throw new ArgumentNullException(nameof(readerWriterLock));
            readerWriterLock.EnterUpgradeableReadLock();
            _readerWriterLock = readerWriterLock;
        }

        /// <summary>
        ///     Create disposable object and tries to enter the lock in write mode.
        /// </summary>
        /// <returns></returns>
        public WriteLockDisposableBlock UseWrite()
        {
            return new WriteLockDisposableBlock(_readerWriterLock);
        }

        /// <summary>
        ///     Reduces the recursion count for upgradeable mode, and exits upgradeable mode if the resulting count is 0 (zero).
        /// </summary>
        public void Dispose()
        {
            _readerWriterLock.ExitUpgradeableReadLock();
        }
    }
}