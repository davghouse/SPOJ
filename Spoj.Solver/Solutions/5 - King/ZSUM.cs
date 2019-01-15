using System;
using System.IO;

// https://www.spoj.com/problems/ZSUM/ #ad-hoc #math #mod-math #sequence
// Calculates (mod a number) a sequence term defined in terms of big exponentations.
public static class ZSUM
{
    private const int _modulus = 10000007;

    // Note that Z_n = Z_n-1 + n^k + n^n, so after a bit more work we see the
    // value we're looking for is n^k + n^n + 2((n - 1)^k + (n - 1)^(n - 1)).
    // Then it's just a matter of doing these huge exponentations efficiently,
    // and without overflowing. I went with FastIO just in case for this problem.
    public static int Solve(int n, int k)
        => (ModularPow(n, k, _modulus) + ModularPow(n, n, _modulus)
        + 2 * (ModularPow(n - 1, k, _modulus) + ModularPow(n - 1, n - 1, _modulus)))
        % _modulus;

    // https://en.wikipedia.org/wiki/Exponentiation_by_squaring
    // https://stackoverflow.com/a/383596
    // One we know how to exponentiate quickly, it's easy to throw some modulos in:
    // https://en.wikipedia.org/wiki/Modular_exponentiation#Right-to-left_binary_method
    public static int ModularPow(int @base, int exponent, int modulus)
    {
        if (modulus == 1)
            return 0;

        int result = 1;
        @base = @base % modulus;
        while (exponent != 0)
        {
            if ((exponent & 1) == 1)
            {
                result = (int)(result * (long)@base % modulus);
            }

            @base = (int)(@base * (long)@base % modulus);
            exponent >>= 1;
        }

        return result;
    }
}

public static class Program
{
    private static void Main()
    {
        int n, k;
        while ((n = FastIO.ReadNonNegativeInt()) != 0)
        {
            k = FastIO.ReadNonNegativeInt();

            FastIO.WriteNonNegativeInt(
                ZSUM.Solve(n, k));
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

    public static void WriteLine()
    {
        if (_outputBufferSize == _outputBufferLimit) // else _outputBufferSize < _outputBufferLimit.
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
