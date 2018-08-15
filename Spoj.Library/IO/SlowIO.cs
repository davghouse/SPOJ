using System;
using System.Text;

namespace Spoj.Library.IO
{
    public static class SlowIO
    {
        private static readonly StringBuilder output = new StringBuilder(8192);

        public static string ReadLine()
            => Console.ReadLine();

        public static void Write(int value)
            => output.Append(value);

        public static void WriteLine(int value)
            => output.AppendLine(value.ToString());

        public static void Flush()
        {
            Console.Write(output);
            Console.Out.Flush();
            output.Clear();
        }
    }
}
