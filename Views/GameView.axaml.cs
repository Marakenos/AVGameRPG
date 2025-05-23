using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG.Views;

namespace AVGameRPG.Views;

public partial class GameView : UserControl
{
    public GameView()
    {
        InitializeComponent();
    }

    private void OnContinueClick(object? sender, RoutedEventArgs e)
    {
        var nick = NickInput.Text;
        var mainWindow = this.VisualRoot as MainWindow;
        mainWindow?.SetContent(new StoryView(nick));
    }

}