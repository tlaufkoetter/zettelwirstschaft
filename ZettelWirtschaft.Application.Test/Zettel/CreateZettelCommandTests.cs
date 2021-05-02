using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ZettelWirtschaft.Application.ValueObject;
using ZettelWirtschaft.Application.Zettel;

namespace ZettelWirtschaft.Application.Test.Zettel
{
    public class CreateZettelCommandTests
    {
        [Theory]
        [InlineData("TestId", "Test Title", "Test Content")]
        [InlineData("TestId1", "Test Title2", "Test Content3")]
        [InlineData("TestId2", "Test Title3", "Test Content4")]
        [InlineData("TestId3", "Test Title4", "Test Content5")]
        [InlineData("TestId4", "Test Title5", "Test Content6")]
        public void CreatesZettel(string expectedId, string expectedTitle, string expectedContent)
        {
            var repoMock = new Mock<IZettelCreationRepository>();
            Expression<Func<IZettelCreationRepository, Task<ZettelId>>> getNewZettelId = repo => repo.GetNewZettelId(It.IsAny<CancellationToken>());
            repoMock.Setup(getNewZettelId).ReturnsAsync(new ZettelId(expectedId));

            Expression<Func<IZettelCreationRepository, Task<ZettelEntity>>> createNewZettel = repo => repo.CreateNewZettel(It.IsAny<ZettelEntity>(), It.IsAny<CancellationToken>());
            repoMock.Setup(createNewZettel).ReturnsAsync((ZettelEntity z, CancellationToken t) => z);

            var handler = new CreateZettelCommandHandler(repoMock.Object);
            var zettel = handler.Handle(new CreateZettelCommand(new Title(expectedTitle), new ZettelContent(expectedContent)), CancellationToken.None).Result;

            Assert.Equal(new ZettelId(expectedId), zettel.Id);
            Assert.Equal(new Title(expectedTitle), zettel.Title);
            Assert.Equal(new ZettelContent(expectedContent), zettel.Content);

            repoMock.Verify(getNewZettelId);
            repoMock.Verify(createNewZettel);
        }
    }
}