using System.Threading;
using System;
using System.Linq.Expressions;
using Moq;
using ZettelWirtschaft.Engine.Zettel;
using System.Threading.Tasks;
using ZettelWirtschaft.Engine.ValueObject;
using Xunit;
using System.Collections.Generic;

namespace ZettelWirtschaft.Engine.Test.Zettel
{
    public class UpdateZettelCommandTests
    {
        public static IEnumerable<object[]> data = new object[][] {
            new object[] {new ZettelEntity(new ZettelId(Guid.NewGuid()), new Title("zettelTitle1"), new ZettelContent("ZettelContent1"))},
            new object[] {new ZettelEntity(new ZettelId(Guid.NewGuid()), new Title("zettelTitle2"), new ZettelContent("ZettelContent2"))},
        };

        [Theory]
        [MemberData(nameof(data))]
        public void UpdatesZettel(ZettelEntity expected)
        {
            var repoMock = new Mock<IUpdateZettelRepository>();

            Expression<Func<IUpdateZettelRepository, Task<bool>>> doesZettelExist = r => r.DoesZettelExist(It.IsAny<ZettelId>(), It.IsAny<CancellationToken>());
            repoMock.Setup(doesZettelExist).ReturnsAsync(true);

            Expression<Func<IUpdateZettelRepository, Task<ZettelEntity>>> updateZettel = r => r.UpdateZettel(It.IsAny<ZettelEntity>(), It.IsAny<CancellationToken>());
            repoMock.Setup(updateZettel).ReturnsAsync(expected);

            var handler = new UpdateZettelCommandHandler(repoMock.Object);

            var result = handler.Handle(new UpdateZettelCommand(new ZettelEntity(new ZettelId(Guid.NewGuid()), new Title("title"), new ZettelContent("content"))), CancellationToken.None).Result;

            Assert.Equal(expected, result);
            Assert.All(expected.GetType().GetProperties(), p => Assert.Equal(p.GetValue(expected), p.GetValue(result)));

            repoMock.Verify(doesZettelExist);
            repoMock.Verify(updateZettel);
        }

        [Fact]
        public void ZettelDoesNotExist()
        {
            var repoMock = new Mock<IUpdateZettelRepository>();

            Expression<Func<IUpdateZettelRepository, Task<bool>>> doesZettelExist = r => r.DoesZettelExist(It.IsAny<ZettelId>(), It.IsAny<CancellationToken>());
            repoMock.Setup(doesZettelExist).ReturnsAsync(false);

            Expression<Func<IUpdateZettelRepository, Task<ZettelEntity>>> updateZettel = r => r.UpdateZettel(It.IsAny<ZettelEntity>(), It.IsAny<CancellationToken>());
            repoMock.Setup(updateZettel);

            var handler = new UpdateZettelCommandHandler(repoMock.Object);

            Assert.ThrowsAny<Exception>(() => handler.Handle(new UpdateZettelCommand(new ZettelEntity(new ZettelId(Guid.NewGuid()), new Title("title"), new ZettelContent("content"))), CancellationToken.None).Result);

            repoMock.Verify(doesZettelExist);
            repoMock.VerifyNoOtherCalls();
        }
    }
}