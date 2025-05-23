using Avalonia.Controls;
using Avalonia.Interactivity;

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

        var dialog = new Window
        {
            Width = 300,
            Height = 100,
            Content = new TextBlock
            {
                Text = $"Witaj, {nick}!",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
            }
        };
        dialog.ShowDialog((Window)this.VisualRoot!);
    }
}