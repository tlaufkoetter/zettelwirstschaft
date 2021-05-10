using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

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
            => AppBuilder.Configure<App>()
               .UsePlatformDetect()
               .LogToTrace()
               .UseReactiveUI();
    }
}
