using Xunit;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Engine.Test.Zettel
{
    public class ZettelContentValidatorTests
    {
        [Theory]
        [InlineData("         ")]
        [InlineData("no trailing    ")]
        [InlineData("kein trailing \nauch nicht bei mehreren Zeilen")]
        [InlineData("auch keine\n   \nwhitezpace Zeilen!")]
        public void TestInvalidContents(string content)
        {
            Assert.False(new ZettelContentValidator().Validate(content).IsValid);
        }

        [Theory]
        [InlineData("ack")]
        [InlineData("")]
        [InlineData("Ein solider, wenn auch kurzer, Text")]
        [InlineData("Mehrzeiler sind in Ordnung\nund auch durchaus erwünscht")]
        [InlineData("   leading ist bei einer Zeile zwar seltsam, aber erlaubt")]
        [InlineData(" * eine liste\n * mit mehreren Einträgen")]
        [InlineData("einrücken\n\tverleicht nachdruck\n\n!")]
        [InlineData("Auf einem Zeilenumbruch zu Enden ist hier voll ok\n")]
        [InlineData("auch\nach mehreren Zeilen\n")]
        public void TestValidContents(string content)
        {
            Assert.True(new ZettelContentValidator().Validate(content).IsValid);
        }
    }
}