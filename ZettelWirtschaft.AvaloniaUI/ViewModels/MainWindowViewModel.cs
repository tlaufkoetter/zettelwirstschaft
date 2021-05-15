using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ReactiveUI;
using ZettelWirtschaft.Engine.Zettel;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

namespace ZettelWirtschaft.AvaloniaUI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        private ObservableAsPropertyHelper<IEnumerable<ZettelListEntryViewModel>> zettelListEntries;
        public IEnumerable<ZettelListEntryViewModel> ZettelListEntries => zettelListEntries.Value;

        private readonly IMediator mediator;

        public ReactiveCommand<System.Reactive.Unit, IRoutableViewModel> CreateNewZettel { get; }
        public ReactiveCommand<System.Reactive.Unit, IEnumerable<ZettelListEntryViewModel>> Refresh { get; }

        public MainWindowViewModel(IMediator mediator)
        {
            Console.WriteLine("happening");
            this.mediator = mediator;

            CreateNewZettel = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new ZettelDetailViewModel(mediator, this))
            );

            Refresh = ReactiveCommand.CreateFromTask(RefreshAsync);

            zettelListEntries = Refresh.ToProperty(this, x => x.ZettelListEntries);

            this.WhenAnyValue(x => x.SelectedZettel).WhereNotNull().Subscribe(x => x.Open.Execute());
        }

        public async Task<IEnumerable<ZettelListEntryViewModel>> RefreshAsync()
        {
            return (await mediator.Send(new GetMultipleZettelQuery())).Select(z => new ZettelListEntryViewModel(z, this));
        }

        [Reactive]
        public ZettelListEntryViewModel? SelectedZettel { get; set; } = null;

        public RoutingState Router { get; } = new RoutingState();
    }
}
