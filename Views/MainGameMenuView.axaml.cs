using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG.Services;
using System.IO;

namespace AVGameRPG.Views
{
    public partial class MainGameMenuView : UserControl
    {
        public MainGameMenuView()
        {
            InitializeComponent();
        }

        // --- Nawigacja ---

        private void OnExitClick(object? s, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new MainMenuView());
        }

        private void OnTownHallClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new TownHallView());
        }

        private void OnHealerClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new HealerView());
        }

        private void OnShopClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new MerchantView());
        }

        private void OnInventoryClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new InventoryView());
        }

        private void OnTrainingClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new ArenaView());
        }

        private void OnStatsClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new StatsView());
        }

        // --- Save / Load ---

        private async void OnSaveClick(object? sender, RoutedEventArgs e)
        {
            var owner = this.VisualRoot as Window;
            var path = AVGameRPG.Services.SaveLoadService.SaveGameToSlot();
            await new MessageBoxWindow($"Zapisano:\n{Path.GetFileName(path)}").ShowDialog(owner);
        }

        private async void OnLoadClick(object? sender, RoutedEventArgs e)
        {
            if (SaveLoadService.LoadGame())
            {
                await ShowToast("Game loaded.");
                
            }
            else
            {
                await ShowToast("No save file found.");
            }
        }

        // --- Pomocnicze UI ---

        private async Task ShowToast(string message)
        {
            if (this.VisualRoot is not Window owner) return;

            var dialog = new Window
            {
                Width = 360,
                Height = 110,
                CanResize = false,
                Title = "Info",
                Content = new TextBlock
                {
                    Text = message,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                }
            };

            await dialog.ShowDialog(owner);
        }
    }
}

