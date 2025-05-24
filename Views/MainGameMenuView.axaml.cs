using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views;

public partial class MainGameMenuView : UserControl
{
    public MainGameMenuView()
    {
        InitializeComponent();
    }

    private void OnExitClick(object? sender, RoutedEventArgs e)
    {
        (this.VisualRoot as Window)?.Close();
    }

    private void OnTownHallClick(object? sender, RoutedEventArgs e)
    {
        var mainWindow = this.VisualRoot as MainWindow;
        mainWindow?.SetContent(new TownHallView());
    }

}