using System;
using System.IO;

namespace Spoj.Library.IO
{
    // This is based in part on submissions from https://www.codechef.com/status/INTEST.
    // It's assumed the input is well-formed, so if you try to read an integer when no
    // integers remain in the input, there's undefined behavior (infinite loop).
    public static class FastIO
    {
        private const byte _null = (byte)'\0';
        private const byte _newLine = (byte)'\n';
        private const byte _minusSign = (byte)'-';
        private const byte _zero = (byte)'0';
        private const int _inputBufferLimit = 8192;
        private const int _outputBufferLimit = 8192;

        private static readonly Stream _inputStream = Console.OpenStandardInput();
        private static readonly byte[] _inputBuffer = new byte[_inputBufferLimit];
        private static int _inputBufferSize = 0;
        private static int _inputBufferIndex = 0;

        private static readonly Stream _outputStream = Console.OpenStandardOutput();
        private static readonly byte[] _outputBuffer = new byte[_outputBufferLimit];
        private static readonly byte[] _digitsBuffer = new byte[11];
        private static int _outputBufferSize = 0;

        private static byte ReadByte() {
            if (_inputBufferIndex == _inputBufferSize) {
                _inputBufferIndex = 0;
                _inputBufferSize = _inputStream.Read(_inputBuffer, 0, _inputBufferLimit);
                if (_inputBufferSize == 0)
                    return _null; // All input has been read.
            }

            return _inputBuffer[_inputBufferIndex++];
        }

        public static int ReadPositiveInt()
        {
            byte digit;

            // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
            do
            {
                digit = ReadByte();
            }
            while (digit < _minusSign);

            // Build up the integer from its digits, until we run into whitespace or the null byte.
            int result = digit - _zero;
            while (true)
            {
                digit = ReadByte();
                if (digit < _zero) break;
                result = result * 10 + (digit - _zero);
            }

            return result;
        }

        public static int ReadInt()
        {
            // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
            byte digit;
            do
            {
                digit = ReadByte();
            }
            while (digit < _minusSign);

            bool isNegative = digit == _minusSign;
            if (isNegative)
            {
                digit = ReadByte();
            }

            // Build up the integer from its digits, until we run into whitespace or the null byte.
            int result = digit - _zero;
            while (true)
            {
                digit = ReadByte();
                if (digit < _zero) break;
                result = result * 10 + (digit - _zero);
            }

            return isNegative ? -result : result;
        }

        public static void Write(int value)
        {
            bool isNegative = value < 0;
            if (isNegative)
            {
                value = -value;
            }

            int digitCount = 0;
            do
            {
                int digit = value % 10;
                _digitsBuffer[digitCount++] = (byte)(digit + _zero);
                value /= 10;
            } while (value > 0);

            if (isNegative)
            {
                _digitsBuffer[digitCount++] = _minusSign;
            }

            if (_outputBufferSize + digitCount > _outputBufferLimit)
            {
                _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
                _outputBufferSize = 0;
            }

            while (digitCount > 0)
            {
                _outputBuffer[_outputBufferSize++] = _digitsBuffer[--digitCount];
            }
        }

        public static void WriteLine(int value)
        {
            Write(value);

            if (_outputBufferSize + 1 > _outputBufferLimit)
            {
                _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
                _outputBufferSize = 0;
            }

            _outputBuffer[_outputBufferSize++] = _newLine;
        }

        public static void Flush()
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
            _outputStream.Flush();
        }
    }
}
