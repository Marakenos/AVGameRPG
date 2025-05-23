using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG.Views;

namespace AVGameRPG.Views;

public partial class MainMenuView : UserControl
{
    public MainMenuView()
    {
        InitializeComponent();
    }

    private void OnExitClick(object? sender, RoutedEventArgs e)
    {
        (this.VisualRoot as Window)?.Close();
    }

    private void OnPlayClick(object? sender, RoutedEventArgs e)
    {
        var mainWindow = this.VisualRoot as MainWindow;
        mainWindow?.SetContent(new GameView());
    }
}
