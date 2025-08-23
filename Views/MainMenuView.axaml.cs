using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views;

public partial class MainMenuView : UserControl
{
    public MainMenuView() => InitializeComponent();

    private void OnExitClick(object? sender, RoutedEventArgs e)
        => (this.VisualRoot as Window)?.Close();

    private void OnPlayClick(object? sender, RoutedEventArgs e)
        => (this.VisualRoot as MainWindow)?.SetContent(new GameView());

    private async void OnLoadClick(object? sender, RoutedEventArgs e)
    {
        var owner = this.VisualRoot as Window;
        var files = Services.SaveLoadService.GetSaveFiles();

        if (files.Count == 0)
        {
            await new MessageBoxWindow("Brak zapisów!").ShowDialog(owner);
            return;
        }

        var chooser = new SaveChooserWindow(files);
        var chosenPath = await chooser.ShowDialog<string?>(owner);
        if (string.IsNullOrEmpty(chosenPath)) return;

        var data = Services.SaveLoadService.LoadGame(chosenPath);
        if (data == null)
        {
            await new MessageBoxWindow("Nie uda³o siê wczytaæ zapisu.").ShowDialog(owner);
            return;
        }

        Services.SaveLoadService.ApplyLoadedData(data);
        (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
    }
}



