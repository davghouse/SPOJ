#include <iostream>
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
  cin >> remainingTestCases;

  while (remainingTestCases-- > 0)
  {
    int a, b;
    cin >> a >> b;

    cout << TEMPLATE::Solve(a, b) << endl;
  }

  return 0;
}
