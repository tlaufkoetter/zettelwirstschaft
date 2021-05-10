using System.Threading;
using System.Threading.Tasks;

namespace ZettelWirtschaft.Engine.Zettel
{
    public interface IZettelCreationRepository
    {
        Task<ZettelId> GetNewZettelId(CancellationToken cancellation);
        Task<ZettelEntity> CreateNewZettel(ZettelEntity zettel, CancellationToken cancellation);
    }
}