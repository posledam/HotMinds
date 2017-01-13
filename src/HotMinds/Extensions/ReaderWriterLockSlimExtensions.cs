using System;
using System.Threading;
using HotMinds.Threading;
using JetBrains.Annotations;

namespace HotMinds.Extensions
{
    /// <summary>
    ///     Useful extensions for <see cref="ReaderWriterLockSlim"/>.
    /// </summary>
    public static class ReaderWriterLockSlimExtensions
    {
        /// <summary>
        ///     Create disposable object and tries to enter the lock in read mode.
        /// </summary>
        /// <param name="readerWriterLock">Lock object.</param>
        /// <returns>Disposable object, that exits read mode on dispose.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ReadLockDisposableBlock UseRead([NotNull] this ReaderWriterLockSlim readerWriterLock)
        {
            if (readerWriterLock == null) throw new ArgumentNullException(nameof(readerWriterLock));
            return new ReadLockDisposableBlock(readerWriterLock);
        }

        /// <summary>
        ///     Create disposable object and tries to enter the lock in write mode.
        /// </summary>
        /// <param name="readerWriterLock">Lock object.</param>
        /// <returns>Disposable object, that exits write mode on dispose.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static WriteLockDisposableBlock UseWrite([NotNull] this ReaderWriterLockSlim readerWriterLock)
        {
            if (readerWriterLock == null) throw new ArgumentNullException(nameof(readerWriterLock));
            return new WriteLockDisposableBlock(readerWriterLock);
        }

        /// <summary>
        ///     Create disposable object and tries to enter the lock in upgradeable read mode.
        /// </summary>
        /// <param name="readerWriterLock">Lock object.</param>
        /// <returns>Disposable object, that exits upgradeable read mode on dispose.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static UpgradeableReadLockDisposableBlock UseUpgradeableRead(
            [NotNull] this ReaderWriterLockSlim readerWriterLock)
        {
            if (readerWriterLock == null) throw new ArgumentNullException(nameof(readerWriterLock));
            return new UpgradeableReadLockDisposableBlock(readerWriterLock);
        }
    }
}
