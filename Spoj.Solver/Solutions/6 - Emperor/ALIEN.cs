using System;
using System.IO;

// https://www.spoj.com/problems/ALIEN/ #sliding-window
// Finds the maximum number of train stations an alien can tolerate.
public sealed class ALIEN
{
    private ALIEN(int humanCount, int stationCount)
    {
        HumansSeen = humanCount;
        StationsPassed = stationCount;
    }

    public int HumansSeen { get; }
    public int StationsPassed { get; }

    public static ALIEN Solve(int stationCount, int humanLimit, int[] stationHumanCounts)
    {
        int optimalHumansSeen = 0,
            optimalStationsPassed = 0;
        int humansSeen = stationHumanCounts[0],
            stationsPassed = 1;
        int startStation = 0,
            endStation = 0;

        // We are looking at a sliding window defined by the start station and the end station.
        // Each loop starts with considering the current window. Then, the end station is
        // advanced by 1, and the start station is advanced only as far as necessary. We can
        // stop once we've considered all the end stations, or once the start station has
        // gotten so high that it won't be possible to find a longer chain of stations.
        while (true)
        {
            if (humansSeen <= humanLimit && stationsPassed > optimalStationsPassed)
            {
                optimalHumansSeen = humansSeen;
                optimalStationsPassed = stationsPassed;
            }
            else if (humansSeen < optimalHumansSeen && stationsPassed == optimalStationsPassed)
            {
                optimalHumansSeen = humansSeen;
            }

            if (endStation == stationCount - 1
                || stationCount - startStation < optimalStationsPassed)
                return new ALIEN(optimalHumansSeen, optimalStationsPassed);

            humansSeen += stationHumanCounts[++endStation];
            ++stationsPassed;

            while (humansSeen > humanLimit && startStation < endStation)
            {
                humansSeen -= stationHumanCounts[startStation++];
                --stationsPassed;
            }
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
            int stationCount = FastIO.ReadNonNegativeInt();
            int humanLimit = FastIO.ReadNonNegativeInt();

            int[] stationHumanCounts = new int[stationCount];
            for (int s = 0; s < stationCount; ++s)
            {
                stationHumanCounts[s] = FastIO.ReadNonNegativeInt();
            }

            var solution = ALIEN.Solve(stationCount, humanLimit, stationHumanCounts);

            FastIO.WriteNonNegativeInt(solution.HumansSeen);
            FastIO.WriteSpace();
            FastIO.WriteNonNegativeInt(solution.StationsPassed);
            FastIO.WriteLine();
        }

        FastIO.Flush();
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: FastIO might not be necessary, but I saw some IO warnings in the comments.
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
