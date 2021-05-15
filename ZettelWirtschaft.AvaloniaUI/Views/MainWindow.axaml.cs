using System.Globalization;
using System;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ZettelWirtschaft.AvaloniaUI.ViewModels;
using ReactiveUI.Validation.Extensions;
using Avalonia.Controls.Notifications;
using Splat;

namespace ZettelWirtschaft.AvaloniaUI.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>, INamed
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            var notificationManager = new WindowNotificationManager(this)
            {
                Position = NotificationPosition.TopRight
            };
            Locator.CurrentMutable.RegisterConstant<INotificationManager>(notificationManager);
        }

        private void InitializeComponent()
        {

            this.WhenActivated(disposables =>
            {
                ViewModel!.Refresh.Execute().Subscribe().DisposeWith(disposables);
            });
            AvaloniaXamlLoader.Load(this);
        }

    }
}