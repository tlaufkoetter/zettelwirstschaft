using System;
using Xunit;
using ZettelWirtschaft.Application.Tag;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Test.Tag
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