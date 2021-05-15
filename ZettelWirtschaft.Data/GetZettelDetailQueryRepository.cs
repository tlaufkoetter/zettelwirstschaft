using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Data
{
    public class GetZettelDetailQueryRepository : IGetZettelDetailQueryRepository
    {
        private readonly ZettelDbContext dbContext;
        private readonly IMapper mapper;

        public GetZettelDetailQueryRepository(ZettelDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ZettelEntity> GetZettelDetail(ZettelId id, CancellationToken cancellation)
        {
            var zettel = await dbContext.Zettels.FindAsync(id.Value);
            if (zettel == null)
            {
                return null;
            }

            return mapper.Map<ZettelEntity>(zettel);
        }
    }
}