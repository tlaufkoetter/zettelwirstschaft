using Xunit;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Test.ValueObject
{
    public class TitleTests
    {
        [Theory]
        [InlineData("  .  mit führenden leerezeichen", ".  mit führenden leerezeichen")]
        [InlineData("mit folgenden leerezeichen  ", "mit folgenden leerezeichen")]
        [InlineData("  mit beidem ", "mit beidem")]
        [InlineData("\n kompliziert", "kompliziert")]
        [InlineData("passiert\n", "passiert")]
        public void TestTrimming(string title, string expected)
        {
            Assert.Equal(expected, new Title(title));
        }

        [Fact]
        public void TestNormalAssignment()
        {
            var text = "textuell";
            Assert.Equal(text, new Title(text));
        }

        [Fact]
        public void UsesValidation()
        {
            Assert.Equal(typeof(ValueObject<string, TitleValidator>), typeof(Title).BaseType);
        }
    }
}