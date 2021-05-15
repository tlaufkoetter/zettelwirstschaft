using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
using ZettelWirtschaft.AvaloniaUI.ViewModels;
using ZettelWirtschaft.AvaloniaUI.Views;

namespace ZettelWirtschaft.AvaloniaUI
{
    public class Startup
    {
        public void ConfigureServices(ServiceCollection services)
        {
            services.UseMicrosoftDependencyResolver();
        }
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public void Start(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        private AppBuilder BuildAvaloniaApp()
        {
            Locator.CurrentMutable.Register(() => new ZettelDetailView(), typeof(IViewFor<ZettelDetailViewModel>));
            Locator.CurrentMutable.Register(() => new ZettelListEntryView(), typeof(IViewFor<ZettelListEntryViewModel>));
            return AppBuilder.Configure<App>()
               .UsePlatformDetect()
               .LogToTrace()
               .UseReactiveUI();
        }
    }
}
