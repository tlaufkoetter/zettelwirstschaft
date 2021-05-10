using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Data
{
    public class GetMultipleZettelRepository : IGetMultipleZettelRepository
    {
        private readonly ZettelDbContext dbContext;
        private readonly IMapper mapper;

        public GetMultipleZettelRepository(ZettelDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public Task<IEnumerable<ZettelEntity>> GetMultipleZettel(CancellationToken cancellation)
        {
            return Task.FromResult(dbContext.Zettels.ProjectTo<ZettelEntity>(mapper.ConfigurationProvider).AsEnumerable());
        }
    }
}