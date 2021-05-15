using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ZettelWirtschaft.Engine.ValueObject;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Engine.Test.Zettel
{
    public class CreateZettelCommandTests
    {
        [Theory]
        [InlineData("Test Title", "Test Content")]
        [InlineData("Test Title2", "Test Content3")]
        [InlineData("Test Title3", "Test Content4")]
        [InlineData("Test Title4", "Test Content5")]
        [InlineData("Test Title5", "Test Content6")]
        public void CreatesZettel(string expectedTitle, string expectedContent)
        {
            var repoMock = new Mock<IZettelCreationRepository>();

            Expression<Func<IZettelCreationRepository, Task<ZettelEntity>>> createNewZettel = repo => repo.CreateNewZettel(It.IsAny<ZettelEntity>(), It.IsAny<CancellationToken>());
            repoMock.Setup(createNewZettel).ReturnsAsync((ZettelEntity z, CancellationToken t) => z);

            var handler = new CreateZettelCommandHandler(repoMock.Object);
            var zettel = handler.Handle(new CreateZettelCommand(new Title(expectedTitle), new ZettelContent(expectedContent)), CancellationToken.None).Result;

            Assert.NotNull(zettel.Id);
            Assert.Equal(new Title(expectedTitle), zettel.Title);
            Assert.Equal(new ZettelContent(expectedContent), zettel.Content);

            repoMock.Verify(createNewZettel);
        }
    }
}