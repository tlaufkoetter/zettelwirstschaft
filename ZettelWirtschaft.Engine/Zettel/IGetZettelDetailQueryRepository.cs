using System.Threading;
using System.Threading.Tasks;

namespace ZettelWirtschaft.Engine.Zettel
{
    public interface IGetZettelDetailQueryRepository
    {
        Task<ZettelEntity> GetZettelDetail(ZettelId id, CancellationToken cancellation);
    }
}