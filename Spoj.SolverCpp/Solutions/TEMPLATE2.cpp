#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
using namespace std;

// Submitted using C++ because C# was unavailable--see TEMPLATE.cs for details.
class TEMPLATE
{
public:
  static int Solve(int a, int b)
  {
    return a * b;
  }
};

int main()
{
  int remainingTestCases;
  scanf("%d", &remainingTestCases);

  while (remainingTestCases-- > 0)
  {
    int a, b;
    scanf("%d %d", &a, &b);

    printf("%d\n", TEMPLATE::Solve(a, b));
  }

  return 0;
}
