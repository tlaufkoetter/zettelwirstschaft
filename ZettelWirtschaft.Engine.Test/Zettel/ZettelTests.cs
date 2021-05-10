using System;
using Xunit;
using ZettelWirtschaft.Engine.Source;
using ZettelWirtschaft.Engine.ValueObject;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Engine.Test.Zettel
{
    public class ZettelTests
    {

        [Fact]
        public void CreateZettel()
        {
            var zettel = new ZettelEntity(new ZettelId("zettel1"), new Title("Ein Zettel"), new ZettelContent("Eine kleine Reise durch die Tests"));
        }

        [Theory]
        [InlineData("zettel1", "Ein Zettel", null)]
        [InlineData("zettel1", null, "Eine kleine Reise durch die Tests")]
        [InlineData(null, "Ein Zettel", "Eine kleine Reise durch die Tests")]
        [InlineData(null, null, "Eine kleine Reise durch die Tests")]
        [InlineData("zettel1", null, null)]
        [InlineData(null, "Ein Zettel", null)]
        [InlineData(null, null, null)]
        public void CreateInvalidZettel(string zettelId, string zettelTitle, string zettelContent)
        {
            ZettelId id = null;
            if (zettelId != null)
            {
                id = new ZettelId(zettelId);
            }
            Title titel = null;
            if (zettelTitle != null)
            {
                titel = new Title(zettelTitle);
            }
            ZettelContent content = null;
            if (zettelContent != null)
            {
                content = new ZettelContent(zettelContent);
            }
            Assert.ThrowsAny<Exception>(() => new ZettelEntity(id, titel, content));
        }

    }
}