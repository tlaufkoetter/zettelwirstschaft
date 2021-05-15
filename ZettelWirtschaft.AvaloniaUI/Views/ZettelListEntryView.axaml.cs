using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ZettelWirtschaft.AvaloniaUI.ViewModels;

namespace ZettelWirtschaft.AvaloniaUI.Views
{
    public partial class ZettelListEntryView : ReactiveUserControl<ZettelListEntryViewModel>
    {
        public ZettelListEntryView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}