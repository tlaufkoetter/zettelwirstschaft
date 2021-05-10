using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Data
{
    public class ZettelCreationRepository : IZettelCreationRepository
    {
        private readonly ZettelDbContext dbContext;
        private readonly IMapper mapper;

        public ZettelCreationRepository(ZettelDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ZettelEntity> CreateNewZettel(ZettelEntity zettel, CancellationToken cancellation)
        {
            var zettelData = await dbContext.Zettels.FindAsync(Guid.Parse(zettel.Id));
            cancellation.ThrowIfCancellationRequested();
            if (zettelData != null)
            {
                throw new ArgumentException($"zettel with id {zettel.Id} does already exist");
            }

            mapper.Map(zettel, zettelData);
            await dbContext.AddAsync(zettelData);

            cancellation.ThrowIfCancellationRequested();
            await dbContext.SaveChangesAsync();
            return mapper.Map<ZettelEntity>(zettelData);
        }

        public Task<ZettelId> GetNewZettelId(CancellationToken cancellation)
        {
            return Task.FromResult(new ZettelId(Guid.NewGuid().ToString()));
        }
    }
}