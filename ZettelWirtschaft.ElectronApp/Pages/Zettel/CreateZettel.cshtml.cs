using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class CreateZettelModel : PageModel
    {
        public class CreateZettelViewModel
        {
            [Required]
            public string Title { get; set; }
            [Required]
            public string Content { get; set; }
        }

        [BindProperty]
        public CreateZettelViewModel Zettel { get; set; }

        private readonly IMediator mediator;

        public CreateZettelModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyZettel = new CreateZettelViewModel();
            if (await TryUpdateModelAsync(emptyZettel, "zettel", zettel => zettel.Title, s => s.Content))
            {
                var createCommand = new CreateZettelCommand(new Title(emptyZettel.Title), new ZettelContent(emptyZettel.Content));
                var newZettel = await mediator.Send(createCommand);
                return RedirectToPage("./ListZettel");
            }
            return Page();
        }

    }
    public class CreateZettelRepository : IZettelCreationRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CreateZettelRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ZettelEntity> CreateNewZettel(ZettelEntity zettel, CancellationToken cancellation)
        {
            var result = (await dbContext.Zettel.AddAsync(new ZettelData()
            {
                Id = Guid.Parse(zettel.Id),
                Title = zettel.Title,
                Content = zettel.Content
            })).Entity;
            await dbContext.SaveChangesAsync();
            return new ZettelEntity(new ZettelId(result.Id.ToString()), new Title(result.Title), new ZettelContent(result.Content));
        }

        public Task<ZettelId> GetNewZettelId(CancellationToken cancellation)
        {
            return Task.FromResult(new ZettelId(Guid.NewGuid().ToString()));
        }
    }
}
