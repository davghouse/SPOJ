using System;
using System.IO;

// https://www.spoj.com/problems/CMPLS/ #math #sequence
// Finds the next terms in the polynomial of lowest degree matching a sequence.
public static class CMPLS
{
    public static void Solve(int?[] sequence, int knownLength, int unknownLength)
    {
        int?[,] differences = new int?[sequence.Length, sequence.Length];
        for (int c = 0; c < knownLength; ++c)
        {
            differences[0, c] = sequence[c].Value;
        }

        for (int r = 1; r < knownLength; ++r)
        {
            for (int c = 0; c < knownLength - r; ++c)
            {
                differences[r, c] = differences[r - 1, c + 1].Value - differences[r - 1, c].Value;
            }
        }

        int constantRow = 0;
        for (int r = 0; r < knownLength; ++r)
        {
            bool isConstantRow = true;
            for (int c = 1; c < knownLength - r; ++c)
            {
                if (differences[r, c].Value != differences[r, c - 1].Value)
                {
                    isConstantRow = false;
                    break;
                }
            }

            if (isConstantRow)
            {
                constantRow = r;
                break;
            }
        }

        for (int c = 1; c < sequence.Length; ++c)
        {
            differences[constantRow, c] = differences[constantRow, c - 1].Value;
        }

        for (int r = constantRow - 1; r >= 0; --r)
        {
            for (int c = knownLength - r; c < sequence.Length; ++c)
            {
                differences[r, c] = differences[r, c - 1].Value + differences[r + 1, c - 1].Value;
            }
        }

        for (int c = knownLength; c < sequence.Length; ++c)
        {
            sequence[c] = differences[0, c].Value;
        }
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int knownLength = FastIO.ReadNonNegativeInt();
            int unknownLength = FastIO.ReadNonNegativeInt();

            int?[] sequence = new int?[knownLength + unknownLength];
            for (int i = 0; i < knownLength; ++i)
            {
                sequence[i] = FastIO.ReadInt();
            }

            CMPLS.Solve(sequence, knownLength, unknownLength);

            for (int i = knownLength; i < sequence.Length; ++i)
            {
                FastIO.WriteNonNegativeInt(sequence[i].Value);
                FastIO.WriteSpace();
            }

            FastIO.WriteLine();
        }

        FastIO.Flush();
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: FastIO might not be necessary, but the problem as a large I/O warning.
public static class FastIO
{
    private const byte _null = (byte)'\0';
    private const byte _space = (byte)' ';
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

    private static byte ReadByte()
    {
        if (_inputBufferIndex == _inputBufferSize)
        {
            _inputBufferIndex = 0;
            _inputBufferSize = _inputStream.Read(_inputBuffer, 0, _inputBufferLimit);
            if (_inputBufferSize == 0)
                return _null; // All input has been read.
        }

        return _inputBuffer[_inputBufferIndex++];
    }

    public static int ReadNonNegativeInt()
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
        int result = isNegative ? -(digit - _zero) : (digit - _zero);
        while (true)
        {
            digit = ReadByte();
            if (digit < _zero) break;
            result = result * 10 + (isNegative ? -(digit - _zero) : (digit - _zero));
        }

        return result;
    }

    public static void WriteNonNegativeInt(int value)
    {
        int digitCount = 0;
        do
        {
            int digit = value % 10;
            _digitsBuffer[digitCount++] = (byte)(digit + _zero);
            value /= 10;
        } while (value > 0);

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

    private static void WriteByte(byte @byte)
    {
        if (_outputBufferSize == _outputBufferLimit) // else _outputBufferSize < _outputBufferLimit.
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        _outputBuffer[_outputBufferSize++] = @byte;
    }

    public static void WriteSpace()
        => WriteByte(_space);

    public static void WriteLine()
        => WriteByte(_newLine);

    public static void Flush()
    {
        _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
        _outputBufferSize = 0;
        _outputStream.Flush();
    }
}
