using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.AvaloniaUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public class ZettelListEntry
        {
            public ZettelListEntry(string title, string content)
            {
                this.Title = title;
                this.Content = content;

            }
            public string Title { get; set; }
            public string Content { get; set; }

        }
        public IEnumerable<ZettelListEntry> Zettel { get; set; } = new List<ZettelListEntry>();

        private readonly IMediator mediator;

        public MainWindowViewModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Update()
        {
            Zettel = (await mediator.Send(new GetMultipleZettelQuery())).Select(z => new ZettelListEntry(z.Title, z.Content));
        }

        public string Greeting => "Welcome to Avalonia!";
    }
}
