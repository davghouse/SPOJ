#include <iostream>
using namespace std;

// See SAMER08F.cs for details--this solution was submitted using C++ because C# was unavailable.
class SAMER08F
{
public:
  SAMER08F()
  {
    _squareCounts[0] = 0;
    _squareCounts[1] = 1;
    _squareCounts[2] = 5;

    for (int n = 3; n <= _limit; ++n)
    {
      _squareCounts[n] = 1 + 4 * SumFromOneUntil(n - 1) + _squareCounts[n - 2];
    }
  }

  int Solve(int n)
  {
    return _squareCounts[n];
  }

private:
  static const int _limit = 100;
  int _squareCounts[_limit + 1];

  static int SumFromOneUntil(int n)
  {
    return n * (n + 1) / 2;
  }
};

int main()
{
  SAMER08F solver;

  int n;
  while (cin >> n && n != 0)
    cout << solver.Solve(n) << endl;

  return 0;
}
