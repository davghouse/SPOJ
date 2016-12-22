using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Helpers;
using System.Linq;

namespace Spoj.Library.UnitTests.Helpers
{
    [TestClass]
    public sealed class StringHelperTests
    {
        private static char[] _delimiters = new[] { '+', '-', '*', '/', '=' };

        private string s;
        private string[] res;

        private string[] SplitAndKeep(string s)
            => s.SplitAndKeep(_delimiters).ToArray();

        [TestMethod]
        public void SplitAndKeep_WhenStringContainsNoDelimiters()
        {
            s = "1234";
            res = SplitAndKeep(s);
            Assert.AreEqual(1, res.Length);
            Assert.AreEqual("1234", res[0]);

            s = "";
            res = SplitAndKeep(s);
            Assert.AreEqual(0, res.Length);

            s = "   ";
            res = SplitAndKeep(s);
            Assert.AreEqual(1, res.Length);
            Assert.AreEqual("   ", res[0]);
        }

        [TestMethod]
        public void SplitAndKeep_WhenStringContainsOneDelimiter()
        {
            s = "12+34";
            res = SplitAndKeep(s);
            Assert.AreEqual(3, res.Length);
            Assert.AreEqual("12", res[0]);
            Assert.AreEqual("+", res[1]);
            Assert.AreEqual("34", res[2]);

            s = "1*4";
            res = SplitAndKeep(s);
            Assert.AreEqual(3, res.Length);
            Assert.AreEqual("1", res[0]);
            Assert.AreEqual("*", res[1]);
            Assert.AreEqual("4", res[2]);

            s = "-3";
            res = SplitAndKeep(s);
            Assert.AreEqual(2, res.Length);
            Assert.AreEqual("-", res[0]);
            Assert.AreEqual("3", res[1]);

            s = "3-";
            res = SplitAndKeep(s);
            Assert.AreEqual(2, res.Length);
            Assert.AreEqual("3", res[0]);
            Assert.AreEqual("-", res[1]);

            s = "/";
            res = SplitAndKeep(s);
            Assert.AreEqual(1, res.Length);
            Assert.AreEqual("/", res[0]);
        }

        [TestMethod]
        public void SplitAndKeep_WhenStringContainsTwoDelimiters()
        {
            s = "1+23+4";
            res = SplitAndKeep(s);
            Assert.AreEqual(5, res.Length);
            Assert.AreEqual("1", res[0]);
            Assert.AreEqual("+", res[1]);
            Assert.AreEqual("23", res[2]);
            Assert.AreEqual("+", res[3]);
            Assert.AreEqual("4", res[4]);

            s = "1++4";
            res = SplitAndKeep(s);
            Assert.AreEqual(4, res.Length);
            Assert.AreEqual("1", res[0]);
            Assert.AreEqual("+", res[1]);
            Assert.AreEqual("+", res[2]);
            Assert.AreEqual("4", res[3]);

            s = "*1*";
            res = SplitAndKeep(s);
            Assert.AreEqual(3, res.Length);
            Assert.AreEqual("*", res[0]);
            Assert.AreEqual("1", res[1]);
            Assert.AreEqual("*", res[2]);

            s = "+12+13";
            res = SplitAndKeep(s);
            Assert.AreEqual(4, res.Length);
            Assert.AreEqual("+", res[0]);
            Assert.AreEqual("12", res[1]);
            Assert.AreEqual("+", res[2]);
            Assert.AreEqual("13", res[3]);

            s = "+-33";
            res = SplitAndKeep(s);
            Assert.AreEqual(3, res.Length);
            Assert.AreEqual("+", res[0]);
            Assert.AreEqual("-", res[1]);
            Assert.AreEqual("33", res[2]);

            s = "33+=";
            res = SplitAndKeep(s);
            Assert.AreEqual(3, res.Length);
            Assert.AreEqual("33", res[0]);
            Assert.AreEqual("+", res[1]);
            Assert.AreEqual("=", res[2]);

            s = "3+3+3";
            res = SplitAndKeep(s);
            Assert.AreEqual(5, res.Length);
            Assert.AreEqual("3", res[0]);
            Assert.AreEqual("+", res[1]);
            Assert.AreEqual("3", res[2]);
            Assert.AreEqual("+", res[3]);
            Assert.AreEqual("3", res[4]);

            s = "*/";
            res = SplitAndKeep(s);
            Assert.AreEqual(2, res.Length);
            Assert.AreEqual("*", res[0]);
            Assert.AreEqual("/", res[1]);
        }

        [TestMethod]
        public void SplitAndKeep_WhenStringContainsAllDelimiters()
        {
            s = "*=+-/";
            res = SplitAndKeep(s);
            Assert.AreEqual(5, res.Length);
            Assert.AreEqual("*", res[0]);
            Assert.AreEqual("=", res[1]);
            Assert.AreEqual("+", res[2]);
            Assert.AreEqual("-", res[3]);
            Assert.AreEqual("/", res[4]);
        }
    }
}
