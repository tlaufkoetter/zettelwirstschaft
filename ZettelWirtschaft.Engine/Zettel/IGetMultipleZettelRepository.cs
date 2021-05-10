using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZettelWirtschaft.Engine.Zettel
{
    public interface IGetMultipleZettelRepository
    {
        Task<IEnumerable<ZettelEntity>> GetMultipleZettel(CancellationToken cancellation);
    }
}