using Xunit;
using ZettelWirtschaft.Engine.Source;
using ZettelWirtschaft.Engine.ValueObject;

namespace ZettelWirtschaft.Engine.Test.Source
{
    public class SourceIdTest
    {
        [Fact]
        public void IsStringId()
        {
            Assert.Equal(typeof(StringId).FullName, typeof(SourceId).BaseType.FullName);
        }

        [Fact]
        public void SetsValue()
        {
            Assert.Equal("test", new SourceId("test"));
        }
    }
}