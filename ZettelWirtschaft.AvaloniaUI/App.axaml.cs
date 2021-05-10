using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MediatR;
using Splat;
using ZettelWirtschaft.AvaloniaUI.ViewModels;
using ZettelWirtschaft.AvaloniaUI.Views;

namespace ZettelWirtschaft.AvaloniaUI
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(Locator.Current.GetService<IMediator>()!),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}