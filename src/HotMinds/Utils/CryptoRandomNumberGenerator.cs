using System;
using System.Security.Cryptography;

namespace HotMinds.Utils
{
    public class CryptoRandomNumberGenerator : IDisposable
    {
        public const int DefaultBufferSize = 0x3FFF;

        private bool _disposed;
        private readonly RNGCryptoServiceProvider _rng;
        private readonly byte[] _buffer;
        private int _position;

        public CryptoRandomNumberGenerator(int bufferSize = DefaultBufferSize)
        {
            _rng = new RNGCryptoServiceProvider();
            _buffer = new byte[bufferSize];
            _position = _buffer.Length;
        }

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
