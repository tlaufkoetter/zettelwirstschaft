using Xunit;
using ZettelWirtschaft.Application.Zettel;

namespace ZettelWirtschaft.Application.Test.Zettel
{
    public class ZettelContentTests
    {
        [Theory]
        [InlineData("no trailing    ", "no trailing\n")]
        [InlineData("kein trailing \nauch nicht bei mehreren Zeilen", "kein trailing\nauch nicht bei mehreren Zeilen\n")]
        [InlineData("auch keine\n   \nwhitezpace Zeilen!", "auch keine\n\nwhitezpace Zeilen!\n")]
        [InlineData("test\n", "test\n")]
        [InlineData("test", "test\n")]
        public void TestAssignment(string content, string expected)
        {
            Assert.Equal(expected, new ZettelContent(content));
        }
    }
}