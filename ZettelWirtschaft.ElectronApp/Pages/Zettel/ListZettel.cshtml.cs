using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZettelWirtschaft.Application.Zettel;
using System.Threading;
using ZettelWirtschaft.ElectronApp.Data;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtscahft.ElectronApp.Pages.Zettel
{
    public class ListZettelModel : PageModel
    {
        private readonly IMediator mediator;

        public IEnumerable<ZettelListEntry> Zettel { get; set; }

        public ListZettelModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task OnGet()
        {
            Zettel = (await mediator.Send(new GetMultipleZettelQuery())).Select(z => new ZettelListEntry() { Id = z.Id, Title = z.Title });
        }

        public class ZettelListEntry
        {
            public string Id { get; set; }
            public string Title { get; set; }
        }
    }
    public class GetMultipleZettelRepository : IGetMultipleZettelRepository
    {

        private readonly ApplicationDbContext dbContext;

        public GetMultipleZettelRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<IEnumerable<ZettelEntity>> GetMultipleZettel(CancellationToken cancellation)
        {
            return Task.FromResult(dbContext.Zettel.Select(z => new ZettelEntity(new ZettelId(z.Id.ToString()), new Title(z.Title), new ZettelContent(z.Content))).AsEnumerable());
        }
    }
}
