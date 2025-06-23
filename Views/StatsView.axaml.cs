using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views
{
    public partial class StatsView : UserControl
    {
        public StatsView()
        {
            InitializeComponent();
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            var mainWindow = this.VisualRoot as MainWindow;
            mainWindow?.SetContent(new MainGameMenuView());
        }
    }
}