using System.Threading;
using HotMinds.Extensions;
using NUnit.Framework;

namespace HotMinds.UnitTests
{
    [TestFixture]
    public class ReaderWriterLockSlimExtensions_Tests
    {
        [Test]
        public void Exception_Tests()
        {
            Assert.That(() => ((ReaderWriterLockSlim)null).UseRead(), Throws.ArgumentNullException);
            Assert.That(() => ((ReaderWriterLockSlim)null).UseWrite(), Throws.ArgumentNullException);
            Assert.That(() => ((ReaderWriterLockSlim)null).UseUpgradeableRead(), Throws.ArgumentNullException);
        }

        [Test]
        public void UseRead_Test()
        {
            var rwLock = new ReaderWriterLockSlim();
            var readObj = rwLock.UseRead();
            Assert.IsTrue(rwLock.IsReadLockHeld); //
            Assert.IsFalse(rwLock.IsWriteLockHeld);
            Assert.IsFalse(rwLock.IsUpgradeableReadLockHeld);

            readObj.Dispose();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsFalse(rwLock.IsWriteLockHeld);
            Assert.IsFalse(rwLock.IsUpgradeableReadLockHeld);
        }

        [Test]
        public void UseWrite_Test()
        {
            var rwLock = new ReaderWriterLockSlim();
            var writeObj = rwLock.UseWrite();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsTrue(rwLock.IsWriteLockHeld); //
            Assert.IsFalse(rwLock.IsUpgradeableReadLockHeld);

            writeObj.Dispose();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsFalse(rwLock.IsWriteLockHeld);
            Assert.IsFalse(rwLock.IsUpgradeableReadLockHeld);
        }

        [Test]
        public void UseUpgradeableRead_Simlpe_Test()
        {
            var rwLock = new ReaderWriterLockSlim();
            var upgrObj = rwLock.UseUpgradeableRead();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsFalse(rwLock.IsWriteLockHeld);
            Assert.IsTrue(rwLock.IsUpgradeableReadLockHeld); //

            upgrObj.Dispose();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsFalse(rwLock.IsWriteLockHeld);
            Assert.IsFalse(rwLock.IsUpgradeableReadLockHeld);
        }

        [Test]
        public void UseUpgradeableRead_Write_Test()
        {
            var rwLock = new ReaderWriterLockSlim();
            var upgrObj = rwLock.UseUpgradeableRead();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsFalse(rwLock.IsWriteLockHeld);
            Assert.IsTrue(rwLock.IsUpgradeableReadLockHeld); //

            var writeObj = upgrObj.UseWrite();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsTrue(rwLock.IsWriteLockHeld); //
            Assert.IsTrue(rwLock.IsUpgradeableReadLockHeld); //

            writeObj.Dispose();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsFalse(rwLock.IsWriteLockHeld);
            Assert.IsTrue(rwLock.IsUpgradeableReadLockHeld); //

            upgrObj.Dispose();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsFalse(rwLock.IsWriteLockHeld);
            Assert.IsFalse(rwLock.IsUpgradeableReadLockHeld);
        }

        [Test]
        public void UseUpgradeableRead_Write2_Test()
        {
            var rwLock = new ReaderWriterLockSlim();
            var upgrObj = rwLock.UseUpgradeableRead();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsFalse(rwLock.IsWriteLockHeld);
            Assert.IsTrue(rwLock.IsUpgradeableReadLockHeld); //

            var writeObj = upgrObj.UseWrite();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsTrue(rwLock.IsWriteLockHeld); //
            Assert.IsTrue(rwLock.IsUpgradeableReadLockHeld); //

            // NO exit write
            //writeObj.Dispose();
            //Assert.IsFalse(rwLock.IsReadLockHeld);
            //Assert.IsFalse(rwLock.IsWriteLockHeld);
            //Assert.IsTrue(rwLock.IsUpgradeableReadLockHeld); //

            upgrObj.Dispose();
            Assert.IsFalse(rwLock.IsReadLockHeld);
            Assert.IsTrue(rwLock.IsWriteLockHeld); //
            Assert.IsFalse(rwLock.IsUpgradeableReadLockHeld);
        }
    }
}
