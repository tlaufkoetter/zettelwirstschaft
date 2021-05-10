using System;
using Xunit;
using ZettelWirtschaft.Engine.ValueObject;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Engine.Test.Zettel
{
    public class ZettelIdTests
    {
        [Fact]
        public void IsStringId()
        {
            Assert.Equal(typeof(StringId).FullName, typeof(ZettelId).BaseType.FullName);
        }

        [Fact]
        public void SetsValue()
        {
            Assert.Equal("test", new StringId("test"));
        }
    }
}