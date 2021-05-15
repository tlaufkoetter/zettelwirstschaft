using System;
using MediatR;
using ReactiveUI;
using Splat;
using ZettelWirtschaft.Engine.Zettel;

namespace ZettelWirtschaft.AvaloniaUI.ViewModels
{
    public class ZettelListEntryViewModel : ReactiveObject, IRoutableViewModel
    {
        private ZettelEntity zettel;
        public string Title { get => zettel.Title; }

        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; }

        public ReactiveCommand<System.Reactive.Unit, IRoutableViewModel> Open { get; }

        public ZettelListEntryViewModel(ZettelEntity zettel, IScreen screen)
        {
            this.zettel = zettel;
            HostScreen = screen;

            Open = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new ZettelDetailViewModel(Locator.Current.GetService<IMediator>()!, HostScreen, zettel)));
        }
    }
}
