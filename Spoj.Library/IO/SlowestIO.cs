using System;

namespace Spoj.Library.IO
{
    public static class SlowestIO
    {
        public static string ReadLine()
            => Console.ReadLine();

        public static void Write(int value)
            => Console.Write(value);

        public static void WriteLine(int value)
            => Console.WriteLine(value);
    }
}
