using System;
using System.IO;

// https://www.spoj.com/problems/JNEXT/ #ad-hoc #greedy #sorting
// Finds the smallest number larger than the given number, using the same digits.
public static class JNEXT
{
    // In order to minimize the number, we want to change digits starting as far right
    // as possible. Starting from the right, we check to see if we've seen a bigger
    // digit than the current one. If so, we swap the smallest such digit into that
    // position. The number is now larger, but we might be able to make it a bit
    // smaller (but still larger) but sorting the digits we've already seen in
    // ascending order. I was curious to see how competitive my time could get for
    // this one, so I used FastIO and tried to sort the tail quickly (using digit buckets).
    // But code could be simpler by using built-in sorts, and I didn't compare performance.
    public static bool Solve(int digitCount, int[] digits)
    {
        if (digitCount == 1)
            return false;

        int[] digitBuckets = new int[10];
        ++digitBuckets[digits[digitCount - 1]];
        int swapIndex = digitCount - 2;

        while (swapIndex >= 0)
        {
            int currentDigit = digits[swapIndex];
            int newDigit = 0;
            for (int d = currentDigit + 1; d <= 9; ++d)
            {
                if (digitBuckets[d] != 0)
                {
                    newDigit = d;
                    break;
                }
            }

            if (newDigit != 0)
            {
                --digitBuckets[newDigit];
                ++digitBuckets[currentDigit];
                digits[swapIndex] = newDigit;

                // Sort tail by looping through the buckets in ascending order.
                int tailIndex = swapIndex + 1;
                for (int d = 0; d <= 9; ++d)
                {
                    int thisDigitCount = digitBuckets[d];
                    for (int c = 1; c <= thisDigitCount; ++c)
                    {
                        digits[tailIndex++] = d;
                    }
                }

                break;
            }
            else
            {
                ++digitBuckets[currentDigit];
                --swapIndex;
            }
        }

        return swapIndex >= 0;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int digitCount = FastIO.ReadNonNegativeInt();
            int[] digits = new int[digitCount];

            for (int d = 0; d < digitCount; ++d)
            {
                digits[d] = FastIO.ReadNonNegativeInt();
            }

            if (JNEXT.Solve(digitCount, digits))
            {
                for (int d = 0; d < digitCount; ++d)
                {
                    FastIO.WriteDigit(digits[d]);
                }
            }
            else
            {
                FastIO.WriteNegativeOne();
            }

            FastIO.WriteLine();
        }

        FastIO.Flush();
    }
}

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

    public static void WriteDigit(int digit)
        => WriteByte((byte)(digit + _zero));

    public static void WriteNegativeOne()
    {
        WriteByte((byte)'-');
        WriteByte((byte)'1');
    }

    public static void WriteLine()
        => WriteByte(_newLine);

    private static void WriteByte(byte @byte)
    {
        if (_outputBufferSize == _outputBufferLimit) // else _outputBufferSize < _outputBufferLimit.
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        _outputBuffer[_outputBufferSize++] = @byte;
    }

    public static void Flush()
    {
        _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
        _outputBufferSize = 0;
        _outputStream.Flush();
    }
}
