using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace HotMinds.Utils
{
    public class CryptoPasswordGenerator : IDisposable
    {
        public const int LengthLimit = 255;

        public const string SafeLowerCaseLetterSymbols = "abcdefghjkmnpqrstuvwxyz";
        public const string SafeUpperCaseLetterSymbols = "ABCDEFGHJKMNPQRSTUVWXYZ";
        public const string SafeDigitSymbols = "23456789";
        public const string SafeSpecialSymbols = "-_#$@~";

        private bool _disposed;
        private int _minLength = 1;
        private int _maxLength = LengthLimit;
        private CharsetGroup[] _charsetGroups;
        private bool _charsetGroupsValid;

        private readonly CryptoRandomNumberGenerator _randomNumber;

        public CryptoPasswordGenerator(
            int minLength = 6,
            int maxLength = 8,
            int lowerCaseLetterMinHit = 1,
            int lowerCaseLetterMaxHit = -1,
            int upperCaseLetterMinHit = 1,
            int upperCaseLetterMaxHit = -1,
            int digitMinHit = 1,
            int digitMaxHit = -1,
            int specialMinHit = 0,
            int specialMaxHit = 1)
        {
            _randomNumber = new CryptoRandomNumberGenerator();
            this.MinLength = minLength;
            this.MaxLength = maxLength;
            this.CharsetGroups = new[]
            {
                new CharsetGroup(SafeLowerCaseLetterSymbols, lowerCaseLetterMinHit, lowerCaseLetterMaxHit),
                new CharsetGroup(SafeUpperCaseLetterSymbols, upperCaseLetterMinHit, upperCaseLetterMaxHit),
                new CharsetGroup(SafeDigitSymbols, digitMinHit, digitMaxHit),
                new CharsetGroup(SafeSpecialSymbols, specialMinHit, specialMaxHit)
            };
        }

        public int MinLength
        {
            get { return _minLength; }
            set
            {
                if (value < 1 || value > _maxLength)
                    throw new ArgumentException(
                        $"{nameof(MinLength)} cannot be less than 1 or greater than the {nameof(MaxLength)}.",
                        nameof(value));
                _minLength = value;
                _charsetGroupsValid = false;
            }
        }

        public int MaxLength
        {
            get { return _maxLength; }
            set
            {
                if (value < _minLength || value > LengthLimit)
                    throw new ArgumentException(
                        $"{nameof(MaxLength)} cannot be less than the {nameof(MinLength)} "
                        + $"or greater than {LengthLimit}.",
                        nameof(value));
                _maxLength = value;
                _charsetGroupsValid = false;
            }
        }

        public CharsetGroup[] CharsetGroups
        {
            get { return _charsetGroups; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (value.Length == 0)
                    throw new ArgumentException("Array must not be empty.", nameof(value));
                _charsetGroups = value;
                _charsetGroupsValid = false;
            }
        }

        public string Generate()
        {
            this.ThrowsIfDisposed();
            this.ValidateCharsetGroups();

            var length = this.GenerateLength();
            var result = new StringBuilder();

            var groupTuples = _charsetGroups
                .Select(p => new CharsetGroupTuple(p))
                .ToList();

            // generate min hits
            foreach (var tuple in groupTuples)
            {
                while (tuple.Count < tuple.Group.MinHits)
                {
                    result.Append(this.GenerateChar(tuple.Group.Charset));
                    tuple.Count += 1;
                }
            }

            var charset = this.GenerateCharset(groupTuples);
            while (result.Length < length)
            {
                var ch = this.GenerateChar(charset);
                var tuple = groupTuples.Find(p => p.Group.Charset.Contains(ch));
                tuple.Count += 1;
                if (tuple.Count >= tuple.Group.MaxHits)
                {
                    charset = this.GenerateCharset(groupTuples);
                }
                result.Append(ch);
            }

            var charArray =
                result.ToString(0, length)
                    .OrderBy(p => _randomNumber.NextByte())
                    .ToArray();

            return new string(charArray);
        }

        private void ValidateCharsetGroups()
        {
            if (!_charsetGroupsValid)
            {
                var sumMin = _charsetGroups.Sum(p => p.MinHits);
                if (sumMin > _minLength)
                {
                    throw new InvalidOperationException("The total of minimum hits greater than the minimum password length.");
                }

                var sumMax = _charsetGroups.Sum(p => (long)p.MaxHits);
                if (sumMax < _minLength)
                {
                    throw new InvalidOperationException("The total of maximum hits less than the minimum password length.");
                }

                _charsetGroupsValid = true;
            }
        }

        private char GenerateChar(string charset)
        {
            var num = _randomNumber.NextUInt32();
            var index = num % (uint)charset.Length;
            var ch = charset[(int)index];
            return ch;
        }

        private string GenerateCharset(IEnumerable<CharsetGroupTuple> tuples)
        {
            return string.Concat(tuples.Select(p => p.Count >= p.Group.MaxHits ? "" : p.Group.Charset));
        }

        private int GenerateLength()
        {
            if (_minLength == _maxLength)
                return _minLength;
            var number = _randomNumber.NextUInt32();
            var delta = number % (_maxLength - _minLength + 1);
            return _minLength + (int)delta;
        }

        private void ThrowsIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(this.GetType().FullName);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _randomNumber.Dispose();
                _disposed = true;
            }
        }

        private class CharsetGroupTuple
        {
            public CharsetGroup Group { get; }

            public int Count { get; set; }

            public CharsetGroupTuple(CharsetGroup charsetGroup)
            {
                this.Group = charsetGroup;
                this.Count = 0;
            }
        }

        public class CharsetGroup
        {
            public string Charset { get; }

            public int MinHits { get; }

            public int MaxHits { get; }

            public CharsetGroup([NotNull] string charset, int minHits = 0, int maxHits = -1)
            {
                if (charset == null)
                    throw new ArgumentNullException(nameof(charset));
                if (string.IsNullOrEmpty(charset))
                    throw new ArgumentException("Charset must not be empty.", nameof(charset));
                if (minHits < 0)
                    throw new ArgumentException("Minimal hits must not be less than zero.", nameof(minHits));
                if (maxHits < 0)
                {
                    maxHits = Int32.MaxValue;
                }
                if (minHits > maxHits)
                    throw new ArgumentException("Maximal hits must not be less than minimal hits.", nameof(maxHits));
                this.Charset = charset;
                this.MinHits = minHits;
                this.MaxHits = maxHits;
            }
        }
    }
}

