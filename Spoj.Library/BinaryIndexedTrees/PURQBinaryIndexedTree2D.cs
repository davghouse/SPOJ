namespace Spoj.Library.BinaryIndexedTrees
{
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
}
