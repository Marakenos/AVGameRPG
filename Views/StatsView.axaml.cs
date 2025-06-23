using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG.Models;

namespace AVGameRPG.Views
{
    public partial class StatsView : UserControl
    {
        public StatsView()
        {
            InitializeComponent();

            var player = GameSession.Player;

            PlayerHpText.Text = $"HP: {player.CurrentHP}/{player.MaxHP}";
            PlayerAtkText.Text = $"Atak: {player.MinAttack}-{player.MaxAttack}";
            PlayerGoldText.Text = $"Z³oto: {player.Gold}";
            PlayerExpText.Text = $"EXP: {player.Experience}";
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
        }
    }
}
