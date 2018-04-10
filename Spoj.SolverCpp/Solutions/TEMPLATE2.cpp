#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
using namespace std;

// See TEMPLATE.cs for details--this solution was submitted using C++ because C# I/O is too slow.
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
  scanf("%d", &remainingTestCases);

  while (remainingTestCases-- > 0)
  {
    int a, b;
    scanf("%d %d", &a, &b);

    printf("%d\n", solver.Solve(a, b));
  }

  return 0;
}
