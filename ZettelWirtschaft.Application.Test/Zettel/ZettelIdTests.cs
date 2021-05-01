using System;
using Xunit;
using ZettelWirtschaft.Application.ValueObject;
using ZettelWirtschaft.Application.Zettel;

namespace ZettelWirtschaft.Application.Test.Zettel
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