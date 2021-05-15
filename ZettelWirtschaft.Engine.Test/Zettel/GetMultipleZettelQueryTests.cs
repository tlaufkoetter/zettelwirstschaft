using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ZettelWirtschaft.Engine.ValueObject;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Engine.Test.Zettel
{
    public class GetMultipleZettelQueryTests
    {
        private static Guid id = Guid.NewGuid();
        public static IEnumerable<object[]> getZettelTestData = new object[][]{
            new[] {
                new List<ZettelEntity>{ new ZettelEntity(new ZettelId(id), new Title("Title"), new ZettelContent("mein Content")) },
                new List<ZettelEntity>{ new ZettelEntity(new ZettelId(id), new Title("Title"), new ZettelContent("mein Content")) }},
            new[] { new List<ZettelEntity>(), new List<ZettelEntity>() }
        };

        [Theory]
        [MemberData(nameof(getZettelTestData))]
        public void GetZettelNonNull(IEnumerable<ZettelEntity> input, IEnumerable<ZettelEntity> expected)
        {
            GetZettel(input, expected);
        }

        [Fact]
        public void GetZettelNull()
        {
            GetZettel(null, new ZettelEntity[] { });
        }

        private static void GetZettel(IEnumerable<ZettelEntity> input, IEnumerable<ZettelEntity> expected)
        {
            var repoMock = new Mock<IGetMultipleZettelRepository>();
            Expression<Func<IGetMultipleZettelRepository, Task<IEnumerable<ZettelEntity>>>> getMultipleZettel = r => r.GetMultipleZettel(It.IsAny<CancellationToken>());
            repoMock.Setup(getMultipleZettel).ReturnsAsync(input);
            var handler = new GetMultipleeZettelQueryHandler(repoMock.Object);
            var result = handler.Handle(new GetMultipleZettelQuery(), CancellationToken.None).Result;

            repoMock.Verify(getMultipleZettel);
            Assert.Equal(expected, result);
            Assert.All(expected.Zip(result), t =>
            {
                Assert.All(typeof(ZettelEntity).GetProperties(), p => Assert.Equal(p.GetValue(t.First), p.GetValue(t.Second)));
            });
        }
    }
}