using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Daves.SpojSpace.Solver.UnitTests
{
    [TestClass]
    public class TagsTests
    {
        [TestMethod]
        public void Tags()
        {
            string[] tags = Solver.Solutions.Tags.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);
            string[] orderedTags = Solver.Solutions.Tags.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries)
                .OrderBy(t => t)
                .ToArray();
            CollectionAssert.AreEqual(tags, orderedTags,
                $"The tags need to be in alphabetical order, like: ${string.Join(Environment.NewLine, orderedTags)}");
        }
    }
}
