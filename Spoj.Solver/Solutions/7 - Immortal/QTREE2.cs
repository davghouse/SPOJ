using System;
using System.Collections.Generic;
using System.IO;

// https://www.spoj.com/problems/QTREE2/ #divide-and-conquer #graph-theory #lca #segment-tree #tree
// Finds the distance and kth edge between vertices in a tree.
public static class QTREE2 // Much of the code has been copied from QTREE_v2 to ensure no TLEs.
{
    private static int _vertexCount;
    private static readonly List<int>[]
        _verticesNeighbors = new List<int>[10000],
        _verticesEdgeWeights = new List<int>[10000],
        _verticesEdges = new List<int>[10000];
    private static readonly int[]
        _verticesParents = new int[10000],
        _verticesDepths = new int[10000],
        _verticesCostToRoot = new int[10000],
        // An edge an ID, a parent, and child vertex--this stores an edge's child vertex ID.
        _edgesChildVertices = new int[9999];
    private static readonly int[,] _ancestorTable = new int[14, 10000];

    private static void BuildRootedStructure(int parentVertexID, int vertexID, int depth, int costToRoot)
    {
        _verticesParents[vertexID] = parentVertexID;
        _verticesDepths[vertexID] = depth;
        _verticesCostToRoot[vertexID] = costToRoot;

        int neighborCount = _verticesNeighbors[vertexID].Count;
        for (int i = 0; i < neighborCount; ++i)
        {
            int childVertexID = _verticesNeighbors[vertexID][i];
            if (childVertexID == parentVertexID) continue;

            int edgeID = _verticesEdges[vertexID][i];
            _edgesChildVertices[edgeID] = childVertexID;
            int edgeWeight = _verticesEdgeWeights[vertexID][i];

            BuildRootedStructure(
                parentVertexID: vertexID,
                vertexID: childVertexID,
                depth: depth + 1,
                costToRoot: costToRoot + edgeWeight);
        }
    }

    // See Topcoder notes on LCA: https://goo.gl/aDqvPG.
    private static void BuildAncestorTable()
    {
        for (int j = 0; 1 << j < _vertexCount; ++j)
        {
            for (int i = 0; i < _vertexCount; ++i)
            {
                _ancestorTable[j, i] = j == 0 ? _verticesParents[i] : -1;
            }
        }

        for (int j = 1; 1 << j < _vertexCount; ++j)
        {
            for (int i = 0; i < _vertexCount; ++i)
            {
                if (_ancestorTable[j - 1, i] != -1)
                {
                    _ancestorTable[j, i] = _ancestorTable[j - 1, _ancestorTable[j - 1, i]];
                }
            }
        }
    }

    private static int GetDistanceBetween(int firstVertexID, int secondVertexID)
    {
        int lcaVertexID = GetLCA(firstVertexID, secondVertexID);

        // The distance is the same as the sum of the costs from each vertex to their LCA.
        // We only know cost of vertices to root, but it's easy to get the cost we want from that.
        return _verticesCostToRoot[firstVertexID]
            + _verticesCostToRoot[secondVertexID]
            // Each of the previous terms includes unwanted cost from LCA to root--remove.
            - 2 * _verticesCostToRoot[lcaVertexID];
    }

    private static int GetKthVertexBetween(int firstVertexID, int secondVertexID, int k)
    {
        int lcaVertexID = GetLCA(firstVertexID, secondVertexID);

        // The path's kth vertex might be between the first vertex and the LCA...
        int vertexCountFromFirstToLCA = _verticesDepths[firstVertexID] - _verticesDepths[lcaVertexID] + 1;
        if (vertexCountFromFirstToLCA >= k)
            return GetKthParent(firstVertexID, k - 1);

        // Or we might have to go from the first vertex, to the LCA, and then down towards the second.
        int vertexCountFromSecondToLCA = _verticesDepths[secondVertexID] - _verticesDepths[lcaVertexID] + 1;
        int totalPathLength = vertexCountFromFirstToLCA + vertexCountFromSecondToLCA - 1;
        return GetKthParent(secondVertexID, totalPathLength - k);
    }

    private static int GetKthParent(int vertexID, int k)
    {
        int parentVertexID = vertexID;
        while (k-- > 0)
        {
            parentVertexID = _verticesParents[parentVertexID];
        }

        return parentVertexID;
    }

    // See Topcoder notes on LCA: https://goo.gl/aDqvPG.
    private static int GetLCA(int firstVertexID, int secondVertexID)
    {
        int tmp, log, i;
        if (_verticesDepths[firstVertexID] < _verticesDepths[secondVertexID])
        {
            tmp = firstVertexID;
            firstVertexID = secondVertexID;
            secondVertexID = tmp;
        }

        for (log = 1; 1 << log <= _verticesDepths[firstVertexID]; log++);
        log--;

        for (i = log; i >= 0; i--)
        {
            if (_verticesDepths[firstVertexID] - (1 << i) >= _verticesDepths[secondVertexID])
            {
                firstVertexID = _ancestorTable[i, firstVertexID];
            }
        }

        if (firstVertexID == secondVertexID)
            return firstVertexID;

        for (i = log; i >= 0; i--)
        {
            if (_ancestorTable[i, firstVertexID] != -1
                && _ancestorTable[i, firstVertexID] != _ancestorTable[i, secondVertexID])
            {
                firstVertexID = _ancestorTable[i, firstVertexID];
                secondVertexID = _ancestorTable[i, secondVertexID];
            }
        }

        return _verticesParents[firstVertexID];
    }

    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            _vertexCount = FastIO.ReadNonNegativeInt();

            for (int vertexID = 0; vertexID < _vertexCount; ++vertexID)
            {
                _verticesNeighbors[vertexID] = new List<int>();
                _verticesEdgeWeights[vertexID] = new List<int>();
                _verticesEdges[vertexID] = new List<int>();
            }

            for (int edgeID = 0; edgeID < _vertexCount - 1; ++edgeID)
            {
                int firstVertexID = FastIO.ReadNonNegativeInt() - 1;
                int secondVertexID = FastIO.ReadNonNegativeInt() - 1;
                int edgeWeight = FastIO.ReadNonNegativeInt();

                _verticesNeighbors[firstVertexID].Add(secondVertexID);
                _verticesNeighbors[secondVertexID].Add(firstVertexID);

                _verticesEdgeWeights[firstVertexID].Add(edgeWeight);
                _verticesEdgeWeights[secondVertexID].Add(edgeWeight);

                _verticesEdges[firstVertexID].Add(edgeID);
                _verticesEdges[secondVertexID].Add(edgeID);
            }

            BuildRootedStructure(
                parentVertexID: -1,
                vertexID: 0,
                depth: 0,
                costToRoot: 0);

            BuildAncestorTable();

            char instruction;
            while ((instruction = FastIO.ReadInstruction()) != 'S')
            {
                if (instruction == 'D')
                {
                    FastIO.WriteNonNegativeInt(GetDistanceBetween(
                        firstVertexID: FastIO.ReadNonNegativeInt() - 1,
                        secondVertexID: FastIO.ReadNonNegativeInt() - 1));
                    FastIO.WriteLine();
                }
                else
                {
                    FastIO.WriteNonNegativeInt(GetKthVertexBetween(
                        firstVertexID: FastIO.ReadNonNegativeInt() - 1,
                        secondVertexID: FastIO.ReadNonNegativeInt() - 1,
                        k: FastIO.ReadNonNegativeInt()) + 1);
                    FastIO.WriteLine();
                }
            }
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

    public static char ReadInstruction()
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

        return secondInstructionChar == 'O' ? 'S' // S for DONE.
            : (char)firstInstructionChar; // D for DIST, K for KTH.
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
