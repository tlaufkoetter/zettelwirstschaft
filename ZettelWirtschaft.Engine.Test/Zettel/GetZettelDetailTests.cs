using System.Threading;
using System.Collections.Generic;
using Moq;
using Xunit;
using ZettelWirtschaft.Engine.Zettel;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;
using ZettelWirtschaft.Engine.ValueObject;

namespace ZettelWirtschaft.Engine.Test.Zettel
{
    public class GetZettelDetailTests
    {
        public static IEnumerable<object[]> data = new object[][] {
            new object[] {new ZettelId("theid"), new ZettelEntity(new ZettelId("theid"), new Title("the title"), new ZettelContent("the content"))},
            new object[] {new ZettelId("theid1"),new ZettelEntity(new ZettelId("theid1"), new Title("the title2"), new ZettelContent("the content3"))}
        };

        [Theory]
        [MemberData(nameof(data))]
        public void GetZettelDetail(ZettelId id, ZettelEntity expected)
        {
            var repoMock = new Mock<IGetZettelDetailQueryRepository>();
            Expression<Func<IGetZettelDetailQueryRepository, Task<ZettelEntity>>> getDetail = r => r.GetZettelDetail(id, It.IsAny<CancellationToken>());
            repoMock.Setup(getDetail).ReturnsAsync(expected);

            var handler = new GetZettelDetailQueryHandler(repoMock.Object);
            var zettel = handler.Handle(new GetZettelDetailQuery(id), CancellationToken.None).Result;

            repoMock.Verify(getDetail);
            Assert.Equal(expected, zettel);
            Assert.All(expected.GetType().GetProperties(), p => Assert.Equal(p.GetValue(expected), p.GetValue(zettel)));
        }

        [Fact]
        public void TryGetNonExistantZettel()
        {
            var repoMock = new Mock<IGetZettelDetailQueryRepository>();
            Expression<Func<IGetZettelDetailQueryRepository, Task<ZettelEntity>>> getDetail = r => r.GetZettelDetail(It.IsAny<ZettelId>(), It.IsAny<CancellationToken>());
            repoMock.Setup(getDetail).ReturnsAsync((ZettelEntity)null);

            var handler = new GetZettelDetailQueryHandler(repoMock.Object);
            var zettel = handler.Handle(new GetZettelDetailQuery(new ZettelId("doesNotExist")), CancellationToken.None).Result;

            repoMock.Verify(getDetail);
            Assert.Null(zettel);
        }
    }
}