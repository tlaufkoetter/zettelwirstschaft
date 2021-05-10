using System;
using Xunit;
using ZettelWirtschaft.Engine.Tag;
using ZettelWirtschaft.Engine.ValueObject;

namespace ZettelWirtschaft.Engine.Test.Tag
{
    public class TagIdTests
    {
        [Fact]
        public void IsStringId()
        {
            Assert.Equal(typeof(StringId).FullName, typeof(TagId).BaseType.FullName);
        }

        [Fact]
        public void SetsValue()
        {
            Assert.Equal("test", new TagId("test"));
        }
    }
}