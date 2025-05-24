using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views;

public partial class TownHallView : UserControl
{
    public TownHallView()
    {
        InitializeComponent();
    }

    private void OnExitClick(object? sender, RoutedEventArgs e)
    {
        (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
    }
}
