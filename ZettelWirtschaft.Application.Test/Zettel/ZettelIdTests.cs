using System;
using Xunit;
using ZettelWirtschaft.Application.Zettel;

namespace ZettelWirtschaft.Application.Test.Zettel
{
    public class ZettelIdTests
    {
        [Theory]
        [InlineData("asdf")]
        [InlineData("a099901293745163p0sdf")]
        [InlineData("ASD8hhasdf3-aw4t5_hjjasdf")]
        [InlineData("1234760")]
        [InlineData("0001234760")]
        [InlineData("0")]
        [InlineData("a")]
        [InlineData("a")]
        [InlineData("1")]
        public void CreateValidZettelId(string id)
        {
            //When
            var zettelId = new ZettelId(id);

            //Then
            Assert.Equal(id, zettelId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("asdhf jhkjsde")]
        [InlineData("asdhf%#$!*@()jhkjsde")]
        [InlineData("ASD8hhasdf3--aw4t5_hjjasdf")]
        [InlineData("ASD8hhasdf3_-aw4t5_hjjasdf")]
        [InlineData("asdf\nhfdsjk")]
        public void CreateInvalidZettelId(string id)
        {
            Assert.ThrowsAny<Exception>(() => new ZettelId(id));
        }

    }
}