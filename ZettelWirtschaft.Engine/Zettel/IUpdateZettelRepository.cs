using System.Threading;
using System.Threading.Tasks;

namespace ZettelWirtschaft.Engine.Zettel
{
    public interface IUpdateZettelRepository
    {
        Task<ZettelEntity> UpdateZettel(ZettelEntity zettel, CancellationToken cancellation);
        Task<bool> DoesZettelExist(ZettelId zettelId1, CancellationToken cancellation);
    }
}