using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.Helpers;
using System.Linq;

namespace Spoj.Library.UnitTests.Helpers
{
    [TestClass]
    public class StringHelperTests
    {
        private static char[] _delimiters = new[] { '+', '-', '*', '/', '=' };

        private string _s;
        private string[] _res;

        private string[] SplitAndKeep(string s)
            => s.SplitAndKeep(_delimiters).ToArray();

        [TestMethod]
        public void SplitAndKeep_WhenStringContainsNoDelimiters()
        {
            _s = "1234";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(1, _res.Length);
            Assert.AreEqual("1234", _res[0]);

            _s = "";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(0, _res.Length);

            _s = "   ";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(1, _res.Length);
            Assert.AreEqual("   ", _res[0]);
        }

        [TestMethod]
        public void SplitAndKeep_WhenStringContainsOneDelimiter()
        {
            _s = "12+34";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(3, _res.Length);
            Assert.AreEqual("12", _res[0]);
            Assert.AreEqual("+", _res[1]);
            Assert.AreEqual("34", _res[2]);

            _s = "1*4";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(3, _res.Length);
            Assert.AreEqual("1", _res[0]);
            Assert.AreEqual("*", _res[1]);
            Assert.AreEqual("4", _res[2]);

            _s = "-3";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(2, _res.Length);
            Assert.AreEqual("-", _res[0]);
            Assert.AreEqual("3", _res[1]);

            _s = "3-";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(2, _res.Length);
            Assert.AreEqual("3", _res[0]);
            Assert.AreEqual("-", _res[1]);

            _s = "/";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(1, _res.Length);
            Assert.AreEqual("/", _res[0]);
        }

        [TestMethod]
        public void SplitAndKeep_WhenStringContainsTwoDelimiters()
        {
            _s = "1+23+4";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(5, _res.Length);
            Assert.AreEqual("1", _res[0]);
            Assert.AreEqual("+", _res[1]);
            Assert.AreEqual("23", _res[2]);
            Assert.AreEqual("+", _res[3]);
            Assert.AreEqual("4", _res[4]);

            _s = "1++4";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(4, _res.Length);
            Assert.AreEqual("1", _res[0]);
            Assert.AreEqual("+", _res[1]);
            Assert.AreEqual("+", _res[2]);
            Assert.AreEqual("4", _res[3]);

            _s = "*1*";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(3, _res.Length);
            Assert.AreEqual("*", _res[0]);
            Assert.AreEqual("1", _res[1]);
            Assert.AreEqual("*", _res[2]);

            _s = "+12+13";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(4, _res.Length);
            Assert.AreEqual("+", _res[0]);
            Assert.AreEqual("12", _res[1]);
            Assert.AreEqual("+", _res[2]);
            Assert.AreEqual("13", _res[3]);

            _s = "+-33";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(3, _res.Length);
            Assert.AreEqual("+", _res[0]);
            Assert.AreEqual("-", _res[1]);
            Assert.AreEqual("33", _res[2]);

            _s = "33+=";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(3, _res.Length);
            Assert.AreEqual("33", _res[0]);
            Assert.AreEqual("+", _res[1]);
            Assert.AreEqual("=", _res[2]);

            _s = "3+3+3";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(5, _res.Length);
            Assert.AreEqual("3", _res[0]);
            Assert.AreEqual("+", _res[1]);
            Assert.AreEqual("3", _res[2]);
            Assert.AreEqual("+", _res[3]);
            Assert.AreEqual("3", _res[4]);

            _s = "*/";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(2, _res.Length);
            Assert.AreEqual("*", _res[0]);
            Assert.AreEqual("/", _res[1]);
        }

        [TestMethod]
        public void SplitAndKeep_WhenStringContainsAllDelimiters()
        {
            _s = "*=+-/";
            _res = SplitAndKeep(_s);
            Assert.AreEqual(5, _res.Length);
            Assert.AreEqual("*", _res[0]);
            Assert.AreEqual("=", _res[1]);
            Assert.AreEqual("+", _res[2]);
            Assert.AreEqual("-", _res[3]);
            Assert.AreEqual("/", _res[4]);
        }
    }
}
