#include <iostream>
using namespace std;

// See TEMPLATE.cs--this solution was submitted using C++ because C# I/O is too slow.
class TEMPLATE
{
public:
  TEMPLATE()
  { }

  int Solve(int a, int b)
  {
    return a * b;
  }
};

int main()
{
  TEMPLATE solver;

  int remainingTestCases;
  cin >> remainingTestCases;

  while (remainingTestCases-- > 0)
  {
    int a, b;
    cin >> a >> b;

    cout << solver.Solve(a, b) << endl;
  }

  return 0;
}
