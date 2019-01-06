using System;
using System.Collections.Generic;
using System.IO;

// https://www.spoj.com/problems/QTREE/ #divide-and-conquer #graph-theory #hld #lca #segment-tree #tree
// Finds the edge of maximum weight between two vertices in a tree w/ updatable weights.
public static class QTREE // v2, trading code quality/readability for better performance
{
    private static int _vertexCount, _chainIndex, _baseArrayIndex;
    private static readonly List<int>[]
        _verticesNeighbors = new List<int>[10000],
        _verticesEdgeWeights = new List<int>[10000],
        _verticesEdges = new List<int>[10000];
    private static readonly int[]
        _verticesDepths = new int[10000],
        _verticesSubtreeSizes = new int[10000],
        _verticesParents = new int[10000],
        // An edge an ID, a parent, and child vertex--this stores an edge's child vertex ID.
        _edgesChildVertices = new int[9999],
        _chainsHeadVertices = new int[10000],
        _verticesChainIndices = new int[10000],
        _verticesBaseArrayIndices = new int[10000],
        _baseArrayWeights = new int[10000],
        _segmentTree = new int[2 * 16384 - 1];
    private static readonly int[,] _ancestorTable = new int[14, 10000];

    private static void BuildRootedStructure(int parentVertexID, int vertexID, int depth)
    {
        _verticesParents[vertexID] = parentVertexID;
        _verticesDepths[vertexID] = depth;
        _verticesSubtreeSizes[vertexID] = 1;

        int neighborCount = _verticesNeighbors[vertexID].Count;
        for (int i = 0; i < neighborCount; ++i)
        {
            int childVertexID = _verticesNeighbors[vertexID][i];
            if (childVertexID == parentVertexID) continue;

            int edgeID = _verticesEdges[vertexID][i];
            _edgesChildVertices[edgeID] = childVertexID;

            BuildRootedStructure(
                parentVertexID: vertexID,
                vertexID: childVertexID,
                depth: depth + 1);

            _verticesSubtreeSizes[vertexID] += _verticesSubtreeSizes[childVertexID];
        }
    }

    private static void RunHLD(int parentVertexID, int vertexID, int edgeWeight)
    {
        if (_chainsHeadVertices[_chainIndex] == -1)
        {
            _chainsHeadVertices[_chainIndex] = vertexID;
        }

        _verticesChainIndices[vertexID] = _chainIndex;
        _verticesBaseArrayIndices[vertexID] = _baseArrayIndex;
        _baseArrayWeights[_baseArrayIndex++] = edgeWeight;

        var neighbors = _verticesNeighbors[vertexID];
        int heaviestChildVertexID = -1;
        int heaviestChildSubtreeSize = 0;
        int heaviestChildEdgeWeight = 0;

        for (int i = 0; i < neighbors.Count; ++i)
        {
            int childVertexID = neighbors[i];
            if (childVertexID == parentVertexID) continue;

            int childSubtreeSize = _verticesSubtreeSizes[childVertexID];
            if (childSubtreeSize > heaviestChildSubtreeSize)
            {
                heaviestChildVertexID = childVertexID;
                heaviestChildSubtreeSize = childSubtreeSize;
                heaviestChildEdgeWeight = _verticesEdgeWeights[vertexID][i];
            }
        }

        if (heaviestChildVertexID != -1)
        {
            RunHLD(
                parentVertexID: vertexID,
                vertexID: heaviestChildVertexID,
                edgeWeight: heaviestChildEdgeWeight);
        }

        for (int i = 0; i < neighbors.Count; ++i)
        {
            int childVertexID = neighbors[i];
            if (childVertexID == parentVertexID || childVertexID == heaviestChildVertexID) continue;

            ++_chainIndex;
            RunHLD(
                parentVertexID: vertexID,
                vertexID: childVertexID,
                edgeWeight: _verticesEdgeWeights[vertexID][i]);
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

    // Most guides online cover this approach, but here's one good one:
    // https://kartikkukreja.wordpress.com/2014/11/09/a-simple-approach-to-segment-trees/
    private static void BuildSegmentTree(int segmentTreeIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _segmentTree[segmentTreeIndex] = _baseArrayWeights[segmentStartIndex];
            return;
        }

        int leftChildSegmentTreeIndex = 2 * segmentTreeIndex + 1;
        int rightChildSegmentTreeIndex = leftChildSegmentTreeIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        BuildSegmentTree(leftChildSegmentTreeIndex, segmentStartIndex, leftChildSegmentEndIndex);
        BuildSegmentTree(rightChildSegmentTreeIndex, leftChildSegmentEndIndex + 1, segmentEndIndex);

        _segmentTree[segmentTreeIndex] = Math.Max(
            _segmentTree[leftChildSegmentTreeIndex],
            _segmentTree[rightChildSegmentTreeIndex]);
    }

    // https://blog.anudeep2011.com/heavy-light-decomposition/
    // https://www.geeksforgeeks.org/heavy-light-decomposition-set-2-implementation/
    private static int Query(int firstVertexID, int secondVertexID)
    {
        int lcaVertexID = GetLCA(firstVertexID, secondVertexID);

        return Math.Max(
            QueryUp(firstVertexID, lcaVertexID),
            QueryUp(secondVertexID, lcaVertexID));
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

    // Finds the edge of maximum weight on the path up the tree from the descendant vertex
    // to the ancestor vertex. The HLD segment tree allows for log(n) querying when vertices
    // are within the same HLD chain. Some chain hopping may be necessary, but as the HLD
    // links above mention, there are no more than log(n) chains.
    private static int QueryUp(int descendantVertexID, int ancestorVertexID)
    {
        int pathMaximumEdgeWeight = 0;
        int ancestorChainIndex = _verticesChainIndices[ancestorVertexID];

        while (true)
        {
            int descendantChainIndex = _verticesChainIndices[descendantVertexID];

            if (descendantChainIndex == ancestorChainIndex)
            {
                if (descendantVertexID == ancestorVertexID)
                    return pathMaximumEdgeWeight; // Could still be zero if initial vertices were equal.

                // Consider the following tree, rooted at V0: V0 --- V1 -- V2 -- V3.
                // Say we're querying from V3 (descendant) to V1 (ancestor). Along that path we need
                // to consider V3's edge to V2, and V2's edge to V1. In the HLD segment tree, vertices
                // correspond with the edge to their parent. So we need to query the range between the
                // descendant and one before the ancestor, V3 to V2. (The base array for the segment tree
                // has ancestors appearing before descendants, explaining the start & end indices below.)
                int chainMaximumEdgeWeight = QuerySegmentTree(
                    _verticesBaseArrayIndices[ancestorVertexID] + 1,
                    _verticesBaseArrayIndices[descendantVertexID]);

                return Math.Max(pathMaximumEdgeWeight, chainMaximumEdgeWeight);
            }
            else
            {
                int descendantChainHeadVertexID = _chainsHeadVertices[descendantChainIndex];

                // Query through the descendant's chain head, which considers all the edges from the
                // descendant to the chain head, plus the edge from the chain head to the next chain.
                int descendantChainMaximumEdgeWeight = QuerySegmentTree(
                    _verticesBaseArrayIndices[descendantChainHeadVertexID],
                    _verticesBaseArrayIndices[descendantVertexID]);
                pathMaximumEdgeWeight = Math.Max(pathMaximumEdgeWeight, descendantChainMaximumEdgeWeight);

                // Advance to the bottom of the next chain.
                descendantVertexID = _verticesParents[descendantChainHeadVertexID];
            }
        }
    }

    private static int QuerySegmentTree(int queryStartIndex, int queryEndIndex)
        => QuerySegmentTree(
            segmentTreeIndex: 0,
            segmentStartIndex: 0,
            segmentEndIndex: _vertexCount - 1,
            queryStartIndex: queryStartIndex,
            queryEndIndex: queryEndIndex);

    private static int QuerySegmentTree(
        int segmentTreeIndex, int segmentStartIndex, int segmentEndIndex,
        int queryStartIndex, int queryEndIndex)
    {
        if (queryStartIndex <= segmentStartIndex && queryEndIndex >= segmentEndIndex)
            return _segmentTree[segmentTreeIndex];

        int leftChildSegmentTreeIndex = 2 * segmentTreeIndex + 1;
        int rightChildSegmentTreeIndex = leftChildSegmentTreeIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        if (queryStartIndex <= leftChildSegmentEndIndex && queryEndIndex > leftChildSegmentEndIndex)
            return Math.Max(
                QuerySegmentTree(leftChildSegmentTreeIndex, segmentStartIndex, leftChildSegmentEndIndex, queryStartIndex, queryEndIndex),
                QuerySegmentTree(rightChildSegmentTreeIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, queryStartIndex, queryEndIndex));
        else if (queryStartIndex <= leftChildSegmentEndIndex)
            return QuerySegmentTree(leftChildSegmentTreeIndex, segmentStartIndex, leftChildSegmentEndIndex, queryStartIndex, queryEndIndex);
        else
            return QuerySegmentTree(rightChildSegmentTreeIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, queryStartIndex, queryEndIndex);
    }

    private static void Change(int edgeID, int weight)
        => UpdateSegmentTree(_verticesBaseArrayIndices[_edgesChildVertices[edgeID]], weight);

    private static void UpdateSegmentTree(int updateIndex, int weight)
        => UpdateSegmentTree(
            segmentTreeIndex: 0,
            segmentStartIndex: 0,
            segmentEndIndex: _vertexCount - 1,
            updateIndex: updateIndex,
            weight: weight);

    private static void UpdateSegmentTree(
        int segmentTreeIndex, int segmentStartIndex, int segmentEndIndex,
        int updateIndex, int weight)
    {
        if (segmentStartIndex == segmentEndIndex /* and also == updateIndex */)
        {
            _segmentTree[segmentTreeIndex] = weight;
            return;
        }

        int leftChildSegmentTreeIndex = 2 * segmentTreeIndex + 1;
        int rightChildSegmentTreeIndex = leftChildSegmentTreeIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        if (updateIndex <= leftChildSegmentEndIndex)
        {
            UpdateSegmentTree(leftChildSegmentTreeIndex, segmentStartIndex, leftChildSegmentEndIndex, updateIndex, weight);
        }
        else
        {
            UpdateSegmentTree(rightChildSegmentTreeIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, updateIndex, weight);
        }

        _segmentTree[segmentTreeIndex] = Math.Max(
            _segmentTree[leftChildSegmentTreeIndex],
            _segmentTree[rightChildSegmentTreeIndex]);
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
                // Can't be more chain heads than vertices, so this definitely resets enough.
                _chainsHeadVertices[vertexID] = -1; 
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
                depth: 0);

            _chainIndex = 0;
            _baseArrayIndex = 0;
            RunHLD(
                parentVertexID: -1,
                vertexID: 0,
                edgeWeight: 0);

            BuildAncestorTable();

            BuildSegmentTree(
                segmentTreeIndex: 0,
                segmentStartIndex: 0,
                segmentEndIndex: _vertexCount - 1);

            char instruction;
            while ((instruction = FastIO.ReadInstruction()) != 'D')
            {
                if (instruction == 'Q')
                {
                    FastIO.WriteNonNegativeInt(Query(
                        firstVertexID: FastIO.ReadNonNegativeInt() - 1,
                        secondVertexID: FastIO.ReadNonNegativeInt() - 1));
                    FastIO.WriteLine();
                }
                else
                {
                    Change(
                        edgeID: FastIO.ReadNonNegativeInt() - 1,
                        weight: FastIO.ReadNonNegativeInt());
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

        // Consume and discard instruction characters (their ASCII codes are all uppercase).
        byte throwawayInstructionChar;
        do
        {
            throwawayInstructionChar = ReadByte();
        } while (throwawayInstructionChar >= _A);

        return (char)firstInstructionChar;
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
