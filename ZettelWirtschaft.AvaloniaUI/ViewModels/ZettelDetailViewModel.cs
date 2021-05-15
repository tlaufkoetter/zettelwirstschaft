using System.Security.Cryptography.X509Certificates;
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
using Splat;
using Avalonia.Controls.Notifications;

namespace ZettelWirtschaft.AvaloniaUI.ViewModels
{
    public class ZettelDetailViewModel : ReactiveValidationObject, IRoutableViewModel
    {
        public ReactiveCommand<System.Reactive.Unit, ZettelEntity> Save { get; }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Close { get; }

        private readonly ObservableAsPropertyHelper<bool> _isSaving;
        public bool IsSaving => _isSaving.Value;

        private readonly ObservableAsPropertyHelper<bool> _canSave;
        public bool CanSave => _canSave.Value;

        public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

        public IScreen HostScreen { get; }

        private readonly IMediator mediator;

        private ZettelId? zettelId { get; set; } = null;

        [Reactive]
        public string ZettelTitle { get; set; } = "";

        [Reactive]
        public string ZettelContent { get; set; } = "";

        public ZettelDetailViewModel(IMediator mediator, IScreen screen, ZettelEntity? zettel = null)
        {
            if (zettel != null)
            {
                zettelId = zettel.Id;
                ZettelTitle = zettel.Title;
                ZettelContent = zettel.Content;
            }

            this.mediator = mediator;
            this.HostScreen = screen;

            this.ValidationRule(
                vm => vm.ZettelTitle,
                title => !string.IsNullOrWhiteSpace(title),
                "Der Titel muss gefüllt sein."
            );

            this.ValidationRule(
                vm => vm.ZettelContent,
                content => !string.IsNullOrWhiteSpace(content),
                "Der Inhalt muss gefüllt sein."
            );

            Close = ReactiveCommand.CreateFromTask(async () =>
            {
                await HostScreen.Router.NavigateBack.Execute();
            });

            _canSave = this.IsValid().ToProperty(this, x => x.CanSave);

            Save = ReactiveCommand
                .CreateFromTask(SaveZettel, this.WhenAnyValue(x => x.CanSave));

            Save.Select(x => x.Id).ToProperty(this, x => x.zettelId);
            Save.Select(x => x.Title.Value).ToProperty(this, x => x.ZettelTitle);
            Save.Select(x => x.Content.Value).ToProperty(this, x => x.ZettelContent);
            Save.Select(x => x).Subscribe(_ => Locator.Current.GetService<INotificationManager>()?.Show(new Notification("Gespeichert", "", type: NotificationType.Success)));

            Save.ThrownExceptions
                .Subscribe(ex => Locator.Current.GetService<INotificationManager>()?.Show(new Notification("Fehler", ex.ToString(), type: NotificationType.Error)));

            _isSaving = Save
                .IsExecuting
                .ToProperty(this, x => x.IsSaving);
        }

        private async Task<ZettelEntity> SaveZettel()
        {
            if (zettelId != null)
            {
                return await mediator.Send(new UpdateZettelCommand(new ZettelEntity(
                    zettelId!, new Engine.ValueObject.Title(ZettelTitle), new ZettelContent(ZettelContent)
                )));
            }
            else
            {
                return await mediator.Send(new CreateZettelCommand(new Engine.ValueObject.Title(ZettelTitle), new Engine.Zettel.ZettelContent(ZettelContent)));
            }
        }
    }
}
