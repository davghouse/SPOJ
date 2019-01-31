using System;
using System.IO;
using System.Text;

// https://www.spoj.com/problems/CEQU/ #formula #gcd #math
// Given ax + by = c, determines if there are integers x, y satisfying it.
public static class CEQU
{
    // https://en.wikipedia.org/wiki/B%C3%A9zout%27s_identity.
    // That identity shows that ax + by can produce any integer that's a multiple of
    // GCD(a, b). Note the proof of it also shows that the GCD(a, b) is the smallest
    // attainable positive integer for ax + by, so only multiples of the GCD can be
    // produced. So the equation is satisfiable iff c is a multiple of the GCD.
    public static bool Solve(int a, int b, int c)
    {
        int gcd = GreatestCommonDivisor(a, b);

        return c % gcd == 0;
    }

    // This is a good article (first section): http://www.cut-the-knot.org/blue/Euclid.shtml.
    // One point to note, for a = bt + r, the gcd(a, b) divides a so it divides bt + r.
    // And it divides b, so it divides bt, which means for bt + r to be divisible by it,
    // r also needs to be divisible by it. So it divides both b and r. And the article
    // notes the importance of showing not only does it divide b and r, it's also their gcd.
    private static int GreatestCommonDivisor(int a, int b)
    {
        int temp;
        while (b != 0)
        {
            temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int testCount = FastIO.ReadNonNegativeInt();
        for (int t = 1; t <= testCount; ++t)
        {
            int a = FastIO.ReadNonNegativeInt();
            int b = FastIO.ReadNonNegativeInt();
            int c = FastIO.ReadNonNegativeInt();

            output.AppendLine(
                $"Case {t}: {(CEQU.Solve(a, b, c) ? "Yes" : "No")}");
        }

        Console.Write(output);
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: FastIO might not be necessary, but seems like large input.
public static class FastIO
{
    private const byte _null = (byte)'\0';
    private const byte _newLine = (byte)'\n';
    private const byte _minusSign = (byte)'-';
    private const byte _zero = (byte)'0';
    private const int _inputBufferLimit = 8192;

    private static readonly Stream _inputStream = Console.OpenStandardInput();
    private static readonly byte[] _inputBuffer = new byte[_inputBufferLimit];
    private static int _inputBufferSize = 0;
    private static int _inputBufferIndex = 0;

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
}
