using System;
using System.IO;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/HIGHWAYS/ (it has multi-edges)
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int cityCount = FastIO.ReadNonNegativeInt();
            if (cityCount < 2 || cityCount > 100000)
                throw new Exception();

            int highwayCount = FastIO.ReadNonNegativeInt();
            if (highwayCount < 1 || highwayCount > 100000)
                throw new Exception();

            int startCityIndex = FastIO.ReadNonNegativeInt() - 1;
            if (startCityIndex < 0 || startCityIndex >= cityCount)
                throw new Exception();

            int endCityIndex = FastIO.ReadNonNegativeInt() - 1;
            if (endCityIndex < 0 || endCityIndex >= cityCount)
                throw new Exception();

            for (int h = 0; h < highwayCount; ++h)
            {
                int firstCityID = FastIO.ReadNonNegativeInt() - 1;
                if (firstCityID < 0 || firstCityID >= cityCount)
                    throw new Exception();

                int secondCityID = FastIO.ReadNonNegativeInt() - 1;
                if (secondCityID < 0 || secondCityID >= cityCount)
                    throw new Exception();

                if (firstCityID == secondCityID)
                    throw new Exception();

                int minutes = FastIO.ReadNonNegativeInt();
                if (minutes < 1 || minutes > 1000)
                    throw new Exception();
            }
        }
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
    private const int _outputBufferLimit = 8192;

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
