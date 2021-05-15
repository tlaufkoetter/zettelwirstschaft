using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using ZettelWirtschaft.AvaloniaUI.ViewModels;

namespace ZettelWirtschaft.AvaloniaUI.Views
{
    public partial class ZettelDetailView : ReactiveUserControl<ZettelDetailViewModel>
    {
        public ZettelDetailView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}