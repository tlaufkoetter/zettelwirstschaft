using System;
using Xunit;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Test.ValueObject
{
    public class StringIdValidatorTests
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
        public void DoesTheTest(string id)
        {
            Assert.True(new StringIdValidator().Validate(id).IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        [InlineData("\n")]
        [InlineData("asdhf jhkjsde")]
        [InlineData("asdhf%#$!*@()jhkjsde")]
        [InlineData("ASD8hhasdf3--aw4t5_hjjasdf")]
        [InlineData("ASD8hhasdf3_-aw4t5_hjjasdf")]
        [InlineData("asdf\nhfdsjk")]
        public void FailToCreateInvalidStringId(string id)
        {
            Assert.False(new StringIdValidator().Validate(id).IsValid);
        }

        [Fact]
        public void FailToCreateNullStringId()
        {
            Assert.ThrowsAny<Exception>(() => new StringIdValidator().Validate((string)null));
        }

        [Fact]
        public void StringIdUsesValidator()
        {
            Assert.Equal(typeof(ValueObject<string, StringIdValidator>).FullName, typeof(StringId).BaseType.FullName);
        }

        [Fact]
        public void SetsValue()
        {
            Assert.Equal("test", new StringId("test"));
        }
    }
}