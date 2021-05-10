using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ZettelWirtschaft.Engine.ValueObject;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.Data
{
    public class UpdateZettelRepository : IUpdateZettelRepository
    {
        private readonly ZettelDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateZettelRepository(ZettelDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<bool> DoesZettelExist(ZettelId zettelId1, CancellationToken cancellation)
        {
            return (await dbContext.Zettels.FindAsync(Guid.Parse(zettelId1))) != null;
        }

        public async Task<ZettelEntity> UpdateZettel(ZettelEntity zettel, CancellationToken cancellation)
        {
            var zettelData = await dbContext.Zettels.FindAsync(Guid.Parse(zettel.Id));
            if (zettelData == null)
            {
                throw new ArgumentException($"zettel with id {zettel.Id} does not exist.");
            }

            mapper.Map(zettel, zettelData);
            dbContext.Update(zettelData);
            await dbContext.SaveChangesAsync();

            return mapper.Map<ZettelEntity>(zettelData);
        }
    }
}