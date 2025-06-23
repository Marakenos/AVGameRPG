using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views
{
    public partial class ArenaView : UserControl
    {
        public ArenaView()
        {
            InitializeComponent();
        }

        private void OnTrainAttackClick(object? sender, RoutedEventArgs e)
        {
            // logika treningu ataku
        }

        private void OnTrainDefenseClick(object? sender, RoutedEventArgs e)
        {
            // logika treningu obrony
        }

        private void OnTrainAgilityClick(object? sender, RoutedEventArgs e)
        {
            // logika treningu zwinnoœci
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            var mainWindow = this.VisualRoot as MainWindow;
            mainWindow?.SetContent(new MainGameMenuView());
        }
    }
}