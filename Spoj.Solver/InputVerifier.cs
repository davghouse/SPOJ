using System;
using System.IO;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/QTREE/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int vertexCount = FastIO.ReadNonNegativeInt();
            if (vertexCount < 1 || vertexCount > 10000)
                throw new ArgumentException();

            for (int i = 0; i < vertexCount - 1; ++i)
            {
                int firstVertexID = FastIO.ReadNonNegativeInt();
                int secondVertexID = FastIO.ReadNonNegativeInt();
                int edgeWeight = FastIO.ReadNonNegativeInt();

                if (firstVertexID < 1 || firstVertexID > vertexCount)
                    throw new ArgumentException();
                if (secondVertexID < 1 || secondVertexID > vertexCount)
                    throw new ArgumentException();
                if (edgeWeight < 0 || edgeWeight > 1000000)
                    throw new ArgumentException();
                // Edges aren't ordered in any way, so this would throw.
                //if (firstVertexID != i + 1)
                //    throw new ArgumentException();
            }

            char instruction;
            while ((instruction = FastIO.ReadInstruction()) != 'D')
            {
                if (instruction == 'C')
                {
                    int edgeNumber = FastIO.ReadNonNegativeInt();
                    int edgeWeight = FastIO.ReadNonNegativeInt();

                    if (edgeNumber < 1 || edgeNumber >= vertexCount)
                        throw new ArgumentException();
                    if (edgeWeight < 0 || edgeWeight > 1000000)
                        throw new ArgumentException();
                }
                else if (instruction == 'Q')
                {
                    int firstVertexID = FastIO.ReadNonNegativeInt();
                    int secondVertexID = FastIO.ReadNonNegativeInt();

                    if (firstVertexID < 1 || firstVertexID > vertexCount)
                        throw new ArgumentException();
                    if (secondVertexID < 1 || secondVertexID > vertexCount)
                        throw new ArgumentException();
                    if (firstVertexID == secondVertexID)
                        throw new ArgumentException();
                }
                else throw new ArgumentException();
            }
        }
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
public static class FastIO
{
    private const byte _null = (byte)'\0';
    private const byte _minusSign = (byte)'-';
    private const byte _zero = (byte)'0';
    private const byte _A = (byte)'A';
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

    public static char ReadInstruction()
    {
        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        byte firstInstructionChar;
        do
        {
            firstInstructionChar = ReadByte();
        } while (firstInstructionChar < _minusSign);

        // Consume and discard instruction characters (their ASCII codes are all uppercase).
        byte throwawayInstructionChar;
        do
        {
            throwawayInstructionChar = ReadByte();
        } while (throwawayInstructionChar >= _A);

        return (char)firstInstructionChar;
    }
}
