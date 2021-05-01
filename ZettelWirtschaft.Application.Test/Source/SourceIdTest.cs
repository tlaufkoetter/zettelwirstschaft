using Xunit;
using ZettelWirtschaft.Application.Source;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Test.Source
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