using System;
using Xunit;
using ZettelWirtschaft.Engine.ValueObject;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Engine.Test.Zettel
{
    public class ZettelIdTests
    {
        [Fact]
        public void IsGuidId()
        {
            Assert.Equal(typeof(GuidId).FullName, typeof(ZettelId).BaseType.FullName);
        }

        [Fact]
        public void SetsValue()
        {
            var id = Guid.NewGuid();
            Assert.Equal(id, new GuidId(id));
        }
    }
}