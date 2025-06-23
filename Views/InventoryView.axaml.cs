using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views
{
    public partial class InventoryView : UserControl
    {
        public InventoryView()
        {
            InitializeComponent();
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            if (this.VisualRoot is MainWindow mainWindow)
            {
                mainWindow.SetContent(new MainGameMenuView());
            }
        }
    }
}