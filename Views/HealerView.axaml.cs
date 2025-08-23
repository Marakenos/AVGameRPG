using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views
{
    public partial class HealerView : UserControl
    {
        private Character Player => GameSession.Player;

        public HealerView()
        {
            InitializeComponent();
        }

        // Leczenie HP do pe³na
        private async void OnHealClick(object? sender, RoutedEventArgs e)
        {
            string message;

            if (Player.CurrentHP < Player.MaxHP)
            {
                Player.CurrentHP = Player.MaxHP;
                message = "You have been healed!";
            }
            else
            {
                message = "You are in full strength!";
            }

            await ShowInfo(message);
        }

        // Odbudowa many – dzia³a jeœli Character ma CurrentMana/MaxMana; inaczej poka¿e info
        private async void OnManaClick(object? sender, RoutedEventArgs e)
        {
            string message;

            var t = Player.GetType();
            var maxManaProp = t.GetProperty("MaxMana");
            var currentManaProp = t.GetProperty("CurrentMana");

            if (maxManaProp != null && currentManaProp != null)
            {
                int maxMana = (int)(maxManaProp.GetValue(Player) ?? 0);
                int currentMana = (int)(currentManaProp.GetValue(Player) ?? 0);

                if (currentMana < maxMana)
                {
                    currentManaProp.SetValue(Player, maxMana);
                    message = "Your mana has been restored!";
                }
                else
                {
                    message = "Your mana is already full!";
                }
            }
            else
            {
                message = "Mana is not available for this character.";
            }

            await ShowInfo(message);
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
        }

        private async Task ShowInfo(string message)
        {
            if (this.VisualRoot is not Window owner) return;

            var dialog = new Window
            {
                Width = 300,
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


