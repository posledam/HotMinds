using System;
using System.Threading;
using JetBrains.Annotations;

namespace HotMinds.Threading
{
    /// <summary>
    ///     Disposable pattern for read lock of <see cref="ReaderWriterLockSlim"/> object.
    /// </summary>
    public struct ReadLockDisposableBlock : IDisposable
    {
        private readonly ReaderWriterLockSlim _readerWriterLock;

        /// <summary>
        ///     Create disposable object and tries to enter the lock in read mode.
        /// </summary>
        /// <param name="readerWriterLock">Lock object.</param>
        public ReadLockDisposableBlock([NotNull] ReaderWriterLockSlim readerWriterLock)
        {
            if (readerWriterLock == null) throw new ArgumentNullException(nameof(readerWriterLock));
            readerWriterLock.EnterReadLock();
            _readerWriterLock = readerWriterLock;
        }

        /// <summary>
        ///     Reduces the recursion count for read mode, and exits read mode if the resulting count is 0 (zero).
        /// </summary>
        public void Dispose()
        {
            _readerWriterLock.ExitReadLock();
        }
    }
}
