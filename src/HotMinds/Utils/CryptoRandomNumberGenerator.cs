using System;
using System.Security.Cryptography;

namespace HotMinds.Utils
{
    /// <summary>
    ///     Сrypto-strength random number generator (based on <see cref="RNGCryptoServiceProvider"/>).
    /// </summary>
    public class CryptoRandomNumberGenerator : IDisposable
    {
        /// <summary>
        ///     The default buffer size for storing generated random bytes.
        /// </summary>
        public const int DefaultBufferSize = 0x3FFF;

        private bool _disposed;
        private readonly RNGCryptoServiceProvider _rng;
        private readonly byte[] _buffer;
        private int _position;

        /// <summary>
        ///     Create the random number generator instance.
        /// </summary>
        /// <param name="bufferSize">The internal buffer size.</param>
        public CryptoRandomNumberGenerator(int bufferSize = DefaultBufferSize)
        {
            _rng = new RNGCryptoServiceProvider();
            _buffer = new byte[bufferSize];
            _position = _buffer.Length;
        }

        /// <summary>
        ///     Get next random byte.
        /// </summary>
        public byte NextByte()
        {
            this.ThrowsIfDisposed();
            if (_position >= _buffer.Length)
            {
                _rng.GetBytes(_buffer);
                _position = 0;
            }
            return _buffer[_position++];
        }

        /// <summary>
        ///     Get next random integer.
        /// </summary>
        public int NextInt32()
        {
            return (int)(this.NextUInt32() & 0x7FFFFFFFu);
        }

        /// <summary>
        ///     Get next random unsigned integer.
        /// </summary>
        public uint NextUInt32()
        {
            this.ThrowsIfDisposed();
            if (_position >= _buffer.Length - 4)
            {
                _rng.GetBytes(_buffer);
                _position = 0;
            }
            var value = BitConverter.ToUInt32(_buffer, _position);
            _position += 4;
            return value;
        }

        /// <summary>
        ///     Get next random unsigned long integer.
        /// </summary>
        public ulong NextUInt64()
        {
            this.ThrowsIfDisposed();
            if (_position >= _buffer.Length - 8)
            {
                _rng.GetBytes(_buffer);
                _position = 0;
            }
            var value = BitConverter.ToUInt64(_buffer, _position);
            _position += 8;
            return value;
        }

        private void ThrowsIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(this.GetType().FullName);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (!_disposed)
            {
                _rng.Dispose();
                _disposed = true;
            }
        }
    }
}
