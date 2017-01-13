using System;
using System.Threading;
using JetBrains.Annotations;

namespace HotMinds.Threading
{
    /// <summary>
    ///     Disposable pattern for write lock of <see cref="ReaderWriterLockSlim"/> object.
    /// </summary>
    public struct WriteLockDisposableBlock : IDisposable
    {
        private readonly ReaderWriterLockSlim _readerWriterLock;

        /// <summary>
        ///     Create disposable object and tries to enter the lock in write mode.
        /// </summary>
        /// <param name="readerWriterLock">Lock object.</param>
        public WriteLockDisposableBlock([NotNull] ReaderWriterLockSlim readerWriterLock)
        {
            if (readerWriterLock == null) throw new ArgumentNullException(nameof(readerWriterLock));
            readerWriterLock.EnterWriteLock();
            _readerWriterLock = readerWriterLock;
        }

        /// <summary>
        ///     Reduces the recursion count for write mode, and exits write mode if the resulting count is 0 (zero).
        /// </summary>
        public void Dispose()
        {
            _readerWriterLock.ExitWriteLock();
        }
    }
}