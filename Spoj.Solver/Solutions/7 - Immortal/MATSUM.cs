using System;
using System.IO;

// https://www.spoj.com/problems/MATSUM/ #bit
// Updates a matrix and computes the sum of square ranges.
public sealed class MATSUM
{
    private int[,] _sourceMatrix;
    private readonly PURQBinaryIndexedTree2D _binaryIndexedTree;

    public MATSUM(int matrixSize)
    {
        _sourceMatrix = new int[matrixSize, matrixSize];
        _binaryIndexedTree = new PURQBinaryIndexedTree2D(matrixSize, matrixSize);
    }

    public void Set(int rowIndex, int columnIndex, int value)
    {
        int oldValue = _sourceMatrix[rowIndex, columnIndex];
        _binaryIndexedTree.PointUpdate(rowIndex, columnIndex, value - oldValue);
        _sourceMatrix[rowIndex, columnIndex] = value;
    }

    public int Query(int nearRowIndex, int nearColumnIndex, int farRowIndex, int farColumnIndex)
        => _binaryIndexedTree.SumQuery(nearRowIndex, nearColumnIndex, farRowIndex, farColumnIndex);
}

// See 1D PURQ before trying to understand this. And then this guide:
// https://www.geeksforgeeks.org/two-dimensional-binary-indexed-tree-or-fenwick-tree/.
public sealed class PURQBinaryIndexedTree2D
{
    private readonly int[,] _tree;
    private readonly int _rowCount;
    private readonly int _columnCount;

    public PURQBinaryIndexedTree2D(int rowCount, int columnCount)
    {
        _tree = new int[rowCount + 1, columnCount + 1];
        _rowCount = rowCount;
        _columnCount = columnCount;
    }

    // Updates to reflect an addition at an index of the original array (by traversing the update trees).
    public void PointUpdate(int rowIndex, int columnIndex, int delta)
    {
        for (int r = rowIndex + 1;
            r <= _rowCount;
            r += r & -r)
        {
            for (int c = columnIndex + 1;
                c <= _columnCount;
                c += c & -c)
            {
                _tree[r, c] += delta;
            }
        }
    }

    // Computes the sum from (0, 0) through (rowIndex, columnIndex) (by traversing the interrogation trees).
    private int SumQuery(int rowIndex, int columnIndex)
    {
        int sum = 0;
        for (int r = rowIndex + 1;
            r > 0;
            r -= r & -r)
        {
            for (int c = columnIndex + 1;
                c > 0;
                c -= c & -c)
            {
                sum += _tree[r, c];
            }
        }

        return sum;
    }

    // Computes the sum from a near point to a far point, by removing the parts we shouldn't
    // have counted. Fenwick describes a more efficient way to do this, but it's complicated.
    public int SumQuery(int nearRowIndex, int nearColumnIndex, int farRowIndex, int farColumnIndex)
        => SumQuery(farRowIndex, farColumnIndex)
        - SumQuery(nearRowIndex - 1, farColumnIndex)
        - SumQuery(farRowIndex, nearColumnIndex - 1)
        + SumQuery(nearRowIndex - 1, nearColumnIndex - 1);
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int matrixSize = FastIO.ReadNonNegativeInt();
            var solver = new MATSUM(matrixSize);

            char command;
            while ((command = FastIO.ReadCommand()) != 'E')
            {
                if (command == 'S')
                {
                    solver.Set(
                        rowIndex: FastIO.ReadNonNegativeInt(),
                        columnIndex: FastIO.ReadNonNegativeInt(),
                        value: FastIO.ReadInt());
                }
                else
                {
                    FastIO.WriteInt(solver.Query(
                        nearRowIndex: FastIO.ReadNonNegativeInt(),
                        nearColumnIndex: FastIO.ReadNonNegativeInt(),
                        farRowIndex: FastIO.ReadNonNegativeInt(),
                        farColumnIndex: FastIO.ReadNonNegativeInt()));
                    FastIO.WriteLine();
                }
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
    private const byte _A = (byte)'A';
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

    public static char ReadCommand()
    {
        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        byte firstInstructionChar;
        do
        {
            firstInstructionChar = ReadByte();
        } while (firstInstructionChar < _minusSign);
        byte secondInstructionChar = ReadByte();

        // Consume and discard instruction characters (their ASCII codes are all uppercase).
        byte throwawayInstructionChar;
        do
        {
            throwawayInstructionChar = ReadByte();
        } while (throwawayInstructionChar >= _A);

        return secondInstructionChar == 'U' ? 'Q' // Q for SUM.
            : (char)firstInstructionChar; // S for SET, E for END.
    }

    public static void WriteInt(int value)
    {
        bool isNegative = value < 0;

        int digitCount = 0;
        do
        {
            int digit = isNegative ? -(value % 10) : (value % 10);
            _digitsBuffer[digitCount++] = (byte)(digit + _zero);
            value /= 10;
        } while (value != 0);

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
