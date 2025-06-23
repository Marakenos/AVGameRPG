using Avalonia.Controls;
using Avalonia.Interactivity;
<<<<<<< HEAD
using AVGameRPG.Models;
=======
>>>>>>> 029769fbb2aa6b27cc0cd482ab7ffaaf6a148d20

namespace AVGameRPG.Views
{
    public partial class StatsView : UserControl
    {
        public StatsView()
        {
            InitializeComponent();
<<<<<<< HEAD

            var player = GameSession.Player;

            PlayerHpText.Text = $"HP: {player.CurrentHP}/{player.MaxHP}";
            PlayerAtkText.Text = $"Atak: {player.MinAttack}-{player.MaxAttack}";
            PlayerGoldText.Text = $"Z³oto: {player.Gold}";
            PlayerExpText.Text = $"EXP: {player.Experience}";
=======
>>>>>>> 029769fbb2aa6b27cc0cd482ab7ffaaf6a148d20
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
        }
    }
}
=======
            var mainWindow = this.VisualRoot as MainWindow;
            mainWindow?.SetContent(new MainGameMenuView());
        }
    }
}
>>>>>>> 029769fbb2aa6b27cc0cd482ab7ffaaf6a148d20
