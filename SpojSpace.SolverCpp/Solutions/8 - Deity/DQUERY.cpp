#define _CRT_SECURE_NO_WARNINGS
#include <algorithm>
#include <stdio.h>
#include <vector>
using namespace std;

// See DQUERY.cs for details--this solution was submitted using C++ because C# is too slow.

struct DistinctCountQuery
{
  int QueryStartIndex;
  int QueryEndIndex;
  int ResultIndex;

  bool operator<(const DistinctCountQuery &rhs) const { return QueryEndIndex < rhs.QueryEndIndex; }
};

class PURQBinaryIndexedTree
{
public:
    PURQBinaryIndexedTree(int arrayLength) : _tree(arrayLength + 1)
    { }

    void PointUpdate(int updateIndex, int delta)
    {
        for (++updateIndex;
            updateIndex < _tree.size();
            updateIndex += updateIndex & -updateIndex)
        {
            _tree[updateIndex] += delta;
        }
    }

    int SumQuery(int queryEndIndex)
    {
        int sum = 0;
        for (++queryEndIndex;
            queryEndIndex > 0;
            queryEndIndex -= queryEndIndex & -queryEndIndex)
        {
            sum += _tree[queryEndIndex];
        }

        return sum;
    }

    int SumQuery(int queryStartIndex, int queryEndIndex)
    {
      return SumQuery(queryEndIndex) - SumQuery(queryStartIndex - 1);
    }

private:
  vector<int> _tree;
};

class DQUERY
{
public:
  DQUERY()
  { }

  vector<int> SolveOffline(int n, vector<int> sourceArray, int queryCount, vector<DistinctCountQuery> queries)
  {
    vector<int> queryResults(queryCount);

    sort(queries.begin(), queries.end());
    PURQBinaryIndexedTree purqBinaryIndexedTree(n);

    vector<int> valuesLatestOccurrenceIndices(_valueLimit, -1);
    int queryIndex = 0;

    for (int phaseEndIndex = 0;
        phaseEndIndex < n && queryIndex < queryCount;
        ++phaseEndIndex)
    {
        int endValue = sourceArray[phaseEndIndex];
        int endValuesPreviousLatestOccurrenceIndex = valuesLatestOccurrenceIndices[endValue];
        if (endValuesPreviousLatestOccurrenceIndex != -1)
        {
            purqBinaryIndexedTree.PointUpdate(endValuesPreviousLatestOccurrenceIndex, -1);
        }
        purqBinaryIndexedTree.PointUpdate(phaseEndIndex, 1);
        valuesLatestOccurrenceIndices[endValue] = phaseEndIndex;

        DistinctCountQuery query;
        while (queryIndex < queryCount
            && (query = queries[queryIndex]).QueryEndIndex == phaseEndIndex)
        {
            queryResults[query.ResultIndex] = purqBinaryIndexedTree.SumQuery(query.QueryStartIndex, phaseEndIndex);
            ++queryIndex;
        }
    }

    return queryResults;
  }

private:
  static const int _valueLimit = 1000000;
};


int main()
{
  DQUERY solver;

  int n;
  scanf("%d", &n);

  vector<int> sourceArray(n);
  for (int i = 0; i < n; ++i)
  {
    scanf("%d", &sourceArray[i]);
  }

  int queryCount;
  scanf("%d", &queryCount);

  vector<DistinctCountQuery> queries(queryCount);
  for (int q = 0; q < queryCount; ++q)
  {
    int queryStartIndex, queryEndIndex;
    scanf("%d %d", &queryStartIndex, &queryEndIndex);

    queries[q].QueryStartIndex = queryStartIndex - 1;
    queries[q].QueryEndIndex = queryEndIndex - 1;
    queries[q].ResultIndex = q;
  }

  vector<int> queryResults = solver.SolveOffline(n, sourceArray, queryCount, queries);
  for (int q = 0; q < queryCount; ++q)
  {
    printf("%d\n", queryResults[q]);
  }

  return 0;
}
