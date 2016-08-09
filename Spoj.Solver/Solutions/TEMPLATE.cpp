#include <iostream>
using namespace std;

// See TEMPLATE.cs--this solution was submitted using C++ because C# was unavailable.
int Solve(int a, int b)
{
  return a * b;
}

int main()
{
  int remainingTestCases;
  cin >> remainingTestCases;

  while (remainingTestCases-- > 0)
  {
    int a, b;
    cin >> a >> b;

    cout << Solve(a, b) << endl;
  }

  return 0;
}