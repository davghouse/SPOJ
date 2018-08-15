using Spoj.Library.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spoj.Library.PerformanceTests.TestSuites
{
    public class IOTestSuite : ITestSuite
    {
        #region FastIO

        private const string _fastInputExeName = "FastInputTest";
        private const string _fastInputSource =
@"using Spoj.Library.IO;

public static class Program
{
    private static void Main()
    {
        int n = FastIO.ReadPositiveInt();
        for (int i = 0; i < n; ++i)
        {
            FastIO.ReadPositiveInt();
        }
    }
}";

        private const string _fastInputAndOutputExeName = "FastInputAndOutputTest";
        private const string _fastInputAndOutputSource =
@"using Spoj.Library.IO;

public static class Program
{
    private static void Main()
    {
        int n = FastIO.ReadPositiveInt();
        for (int i = 0; i < n; ++i)
        {
            FastIO.WriteLine(FastIO.ReadPositiveInt());
        }

        FastIO.Flush();
    }
}";

        #endregion FastIO

        #region SlowIO

        private const string _slowInputExeName = "SlowInputTest";
        private const string _slowInputSource =
@"using Spoj.Library.IO;

public static class Program
{
    private static void Main()
    {
        int n = int.Parse(SlowIO.ReadLine());
        for (int i = 0; i < n; ++i)
        {
            int.Parse(SlowIO.ReadLine());
        }
    }
}";

        private const string _slowInputAndOutputExeName = "SlowInputAndOutputTest";
        private const string _slowInputAndOutputSource =
@"using Spoj.Library.IO;

public static class Program
{
    private static void Main()
    {
        int n = int.Parse(SlowIO.ReadLine());
        for (int i = 0; i < n; ++i)
        {
            SlowIO.WriteLine(int.Parse(SlowIO.ReadLine()));
        }

        SlowIO.Flush();
    }
}";

        #endregion SlowIO

        #region SlowestIO

        private const string _slowestInputExeName = "SlowestInputTest";
        private const string _slowestInputSource =
@"using Spoj.Library.IO;

public static class Program
{
    private static void Main()
    {
        int n = int.Parse(SlowestIO.ReadLine());
        for (int i = 0; i < n; ++i)
        {
            int.Parse(SlowestIO.ReadLine());
        }
    }
}";

        private const string _slowestInputAndOutputExeName = "SlowestInputAndOutputTest";
        private const string _slowestInputAndOutputSource =
@"using Spoj.Library.IO;

public static class Program
{
    private static void Main()
    {
        int n = int.Parse(SlowestIO.ReadLine());
        for (int i = 0; i < n; ++i)
        {
            SlowestIO.WriteLine(int.Parse(SlowestIO.ReadLine()));
        }
    }
}";

        #endregion SlowestIO

        private const int _inputSize = 10_000_000;
        private readonly string _input;

        public IOTestSuite()
        {
            var (success, diagnostics) = RuntimeCompiler.CompileToExe(_fastInputExeName, _fastInputSource);
            if (!success) throw new Exception();
            (success, diagnostics) = RuntimeCompiler.CompileToExe(_fastInputAndOutputExeName, _fastInputAndOutputSource);
            if (!success) throw new Exception();

            (success, diagnostics) = RuntimeCompiler.CompileToExe(_slowInputExeName, _slowInputSource);
            if (!success) throw new Exception();
            (success, diagnostics) = RuntimeCompiler.CompileToExe(_slowInputAndOutputExeName, _slowInputAndOutputSource);
            if (!success) throw new Exception();

            (success, diagnostics) = RuntimeCompiler.CompileToExe(_slowestInputExeName, _slowestInputSource);
            if (!success) throw new Exception();
            (success, diagnostics) = RuntimeCompiler.CompileToExe(_slowestInputAndOutputExeName, _slowestInputAndOutputSource);
            if (!success) throw new Exception();

            var inputBuilder = new StringBuilder();
            inputBuilder.AppendLine($"{_inputSize}");
            for (int i = 0; i < _inputSize; ++i)
            {
                inputBuilder.Append(InputGenerator.GenerateRandomInt(0, 100000));
                inputBuilder.AppendLine();
            }
            _input = inputBuilder.ToString();
        }

        public IEnumerable<TestScenario> TestScenarios => new TestScenario[]
        {
            new TestScenario($"{_inputSize} lines of input, no output", new TestCase[]
                {
                    new TestCase("FastIO", () => ExecuteExe(_fastInputExeName)),
                    new TestCase("SlowIO", () => ExecuteExe(_slowInputExeName)),
                    new TestCase("SlowestIO", () => ExecuteExe(_slowestInputExeName))
                }),
            new TestScenario($"{_inputSize} lines of input and output", new TestCase[]
                {
                    new TestCase("FastIO", () => ExecuteExe(_fastInputAndOutputExeName)),
                    new TestCase("SlowIO", () => ExecuteExe(_slowInputAndOutputExeName)),
                    new TestCase("SlowestIO", () => ExecuteExe(_slowestInputAndOutputExeName))
                }),
        };

        private void ExecuteExe(string exeName)
        {
            var (exitCode, output) = RuntimeCompiler.ExecuteExe(exeName, _input);
            if (exitCode != 0) throw new Exception();
        }
    }
}
