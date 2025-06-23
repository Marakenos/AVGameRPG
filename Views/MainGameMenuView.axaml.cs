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

    private void OnHealerClick(object? sender, RoutedEventArgs e)
    {
        var mainWindow = this.VisualRoot as MainWindow;
        mainWindow?.SetContent(new HealerView());
    }

    private void OnShopClick(object? sender, RoutedEventArgs e)
    {
        if (this.VisualRoot is MainWindow main)
        {
            main.SetContent(new MerchantView());
        }
    }
    private void OnInventoryClick(object? sender, RoutedEventArgs e)
    {
        if (this.VisualRoot is MainWindow mainWindow)
        {
            mainWindow.SetContent(new InventoryView());
        }
    }
    private void OnTrainingClick(object? sender, RoutedEventArgs e)
    {
        var mainWindow = this.VisualRoot as MainWindow;
        mainWindow?.SetContent(new ArenaView());
    }

    private void OnStatsClick(object? sender, RoutedEventArgs e)
    {
        var mainWindow = this.VisualRoot as MainWindow;
        mainWindow?.SetContent(new StatsView());
    }
}