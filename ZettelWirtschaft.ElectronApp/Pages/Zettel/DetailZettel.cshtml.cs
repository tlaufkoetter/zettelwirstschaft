using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZettelWirtschaft.Application.ValueObject;
using ZettelWirtschaft.Application.Zettel;
using ZettelWirtschaft.ElectronApp.Data;

namespace ZettelWirtscahft.ElectronApp.Pages.Zettel
{
    public class DetailZettelModel : PageModel
    {
        public class DetailZettelData
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public DetailZettelData Zettel { get; set; }
        private readonly IMediator mediator;

        public DetailZettelModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task OnGet(string zettelId)
        {
            var zettelEntity = await mediator.Send(new GetZettelDetailQuery(new ZettelId(zettelId)));
            Zettel = new DetailZettelData()
            {
                Id = zettelEntity.Id,
                Title = zettelEntity.Title,
                Content = zettelEntity.Content
            };
        }
    }

    public class GetZettelDetailQueryRepository : IGetZettelDetailQueryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public GetZettelDetailQueryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ZettelEntity> GetZettelDetail(ZettelId id, CancellationToken cancellation)
        {
            var data = await dbContext.FindAsync<ZettelData>(Guid.Parse(id));
            return new ZettelEntity(new ZettelId(data.Id.ToString()), new Title(data.Title), new ZettelContent(data.Content));
        }
    }
}
