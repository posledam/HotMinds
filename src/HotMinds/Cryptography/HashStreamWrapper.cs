using System;
using System.IO;
using System.Security.Cryptography;
using JetBrains.Annotations;

namespace HotMinds.Cryptography
{
    /// <summary>
    ///     Stream wrapper for calculating the hash on the fly when reading or writing.
    /// </summary>
    public class HashStreamWrapper : Stream
    {
        private readonly Stream _stream;
        private readonly HashAlgorithm _hashAlgorithm;

        private bool _isFinal;

        /// <summary>
        ///     Create hash stream wrapper.
        /// </summary>
        /// <param name="stream">The stream is reading from or writing to which you want to calculate the hash.</param>
        /// <param name="hashAlgorithm">Hash algorithm with CanTransformMultipleBlocks property must be true.</param>
        /// <exception cref="ArgumentNullException">
        ///     Arguments <paramref name="stream"/> and <paramref name="hashAlgorithm"/> must be not null.
        /// </exception>
        public HashStreamWrapper([NotNull] Stream stream, [NotNull] HashAlgorithm hashAlgorithm)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (hashAlgorithm == null) throw new ArgumentNullException(nameof(hashAlgorithm));

            _stream = stream;
            _hashAlgorithm = hashAlgorithm;

            _hashAlgorithm.Initialize();
        }

        #region Own method and properties

        /// <summary>
        ///     Complete the hash calculation and return the result.
        /// </summary>
        /// <returns>Calculated hash.</returns>
        public byte[] GetHash()
        {
            if (!_isFinal)
            {
                _hashAlgorithm.TransformFinalBlock(new byte[0], 0, 0);
                _isFinal = true;
            }
            return _hashAlgorithm.Hash;
        }

        /// <summary>
        ///     Complete the hash calculation and return the result in a string with hexadecimal representation.
        /// </summary>
        /// <returns>Calculated hash.</returns>
        public string GetHashString()
        {
            return HashUtils.ByteArrayToHexString(this.GetHash());
        }

        #endregion


        #region Wrapped methods and properties

        /// <inheritdoc />
        public override void Flush()
        {
            _stream.Flush();
        }

        /// <inheritdoc />
        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        /// <inheritdoc />
        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        /// <inheritdoc />
        public override int Read(byte[] buffer, int offset, int count)
        {
            var readCount = _stream.Read(buffer, offset, count);
            _hashAlgorithm.TransformBlock(buffer, offset, readCount, null, 0);
            return readCount;
        }

        /// <inheritdoc />
        public override void Write(byte[] buffer, int offset, int count)
        {
            _hashAlgorithm.TransformBlock(buffer, offset, count, null, 0);
            _stream.Write(buffer, offset, count);
        }

        /// <inheritdoc />
        public override bool CanRead => _stream.CanRead;

        /// <inheritdoc />
        public override bool CanSeek => _stream.CanSeek;

        /// <inheritdoc />
        public override bool CanWrite => _stream.CanWrite;

        /// <inheritdoc />
        public override long Length => _stream.Length;

        /// <inheritdoc />
        public override long Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _stream.Dispose();
                _hashAlgorithm.Dispose();
            }
        }

        #endregion
    }
}
