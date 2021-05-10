using Xunit;
using ZettelWirtschaft.Engine.ValueObject;

namespace ZettelWirtschaft.Engine.Test.ValueObject
{
    public class TitleValidatorTests
    {
        [Theory]
        [InlineData("test")]
        [InlineData("Dies ist ein schöner Test.")]
        [InlineData("1. Ein weitere valider Titel!")]
        [InlineData("2. #1 ist ichi ban")]
        [InlineData("3. ein Test: funktioniert das?")]
        [InlineData("@#$%^()&*_(")]
        public void TestValidTitleStrings(string text)
        {
            Assert.True(new TitleValidator().Validate(text).IsValid);
        }

        [Theory]
        [InlineData("Dies ist ein Test.\nmit mehreren\nZeilen")]
        [InlineData("asd")]
        [InlineData("\n1. Ein weitere invalider Titel.")]
        [InlineData("\t2. #2 ist falsch")]
        [InlineData("mit einem Zeilenumbruch zu enden ist nicht erlaubt\n")]
        [InlineData("2. #2 ist falsch weil trailing Whitespaces nicht erlaubt sind ")]
        [InlineData("Eigentlich sieht der Titel ganz gut aus aber 101 zeichen sind zu viele und irgendwann ist mal gut!!!!")]
        [InlineData("")]
        [InlineData("      ")]
        [InlineData("\n     ")]
        [InlineData("\t  \t\t\t")]
        public void TestInvalidTitleStrings(string text)
        {
            Assert.False(new TitleValidator().Validate(text).IsValid);
        }
    }
}