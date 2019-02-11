using System;
using System.IO;

// https://www.spoj.com/problems/SCUBADIV/ #memoization #recursion
// Finds the minimum cylinder weight for a scuba diver to survive.
public sealed class SCUBADIV
{
    private readonly int _cylinderCount;
    private readonly int[,] _cylinders;

    public SCUBADIV(int cylinderCount, int[,] cylinders)
    {
        _cylinderCount = cylinderCount;
        _cylinders = cylinders;
    }

    public int Solve(int oxygenReq, int nitrogenReq)
        => Solve(
            oxygenReq,
            nitrogenReq,
            cylinderIndex: 0,
            memoizer: new int?[oxygenReq + 1, nitrogenReq + 1, _cylinderCount],
            memoizerHasSolution: new bool[oxygenReq + 1, nitrogenReq + 1, _cylinderCount]).Value;

    // Thinking recursively, we can either use the ith cylinder or not. The lightest
    // way to meet the oxygen and nitrogen requirement is the min of 1) don't use the
    // ith cylinder, and fill up the requirements using the later cylinders, or
    // 2) use the ith cylinder, taking on its weight but lowering the oxygen and
    // nitrogen requirements that the later cylinders need to fill.
    private int? Solve(
        int oxygenReq,
        int nitrogenReq,
        int cylinderIndex,
        int?[,,] memoizer,
        bool[,,] memoizerHasSolution)
    {
        if (oxygenReq == 0 && nitrogenReq == 0)
            return 0;

        if (cylinderIndex == _cylinderCount)
            return null;

        if (memoizerHasSolution[oxygenReq, nitrogenReq, cylinderIndex])
            return memoizer[oxygenReq, nitrogenReq, cylinderIndex];

        int cylinderOxygen = _cylinders[cylinderIndex, 0];
        int cylinderNitrogen = _cylinders[cylinderIndex, 1];
        int cylinderWeight = _cylinders[cylinderIndex, 2];

        int? solutionWithoutCylinder = Solve(
            oxygenReq,
            nitrogenReq,
            cylinderIndex + 1,
            memoizer,
            memoizerHasSolution);
        int? solutionWithCylinder = Solve(
            Math.Max(oxygenReq - cylinderOxygen, 0),
            Math.Max(nitrogenReq - cylinderNitrogen, 0),
            cylinderIndex + 1,
            memoizer,
            memoizerHasSolution)
            + cylinderWeight;

        int? solution = solutionWithoutCylinder.HasValue || solutionWithCylinder.HasValue
            ? Math.Min(solutionWithoutCylinder ?? int.MaxValue, solutionWithCylinder ?? int.MaxValue)
            : (int?)null;
        memoizer[oxygenReq, nitrogenReq, cylinderIndex] = solution;
        memoizerHasSolution[oxygenReq, nitrogenReq, cylinderIndex] = true;

        return solution;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int oxygenReq = FastIO.ReadNonNegativeInt();
            int nitrogenReq = FastIO.ReadNonNegativeInt();

            int cylinderCount = FastIO.ReadNonNegativeInt();;
            int[,] cylinders = new int[cylinderCount, 3];
            for (int c = 0; c < cylinderCount; ++c)
            {
                cylinders[c, 0] = FastIO.ReadNonNegativeInt(); // oxygen
                cylinders[c, 1] = FastIO.ReadNonNegativeInt(); // nitrogen
                cylinders[c, 2] = FastIO.ReadNonNegativeInt(); // weight
            }

            var solver = new SCUBADIV(cylinderCount, cylinders);

            Console.WriteLine(
                solver.Solve(oxygenReq, nitrogenReq));
        }
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: Working around malformed input, not here because we need speed.
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
