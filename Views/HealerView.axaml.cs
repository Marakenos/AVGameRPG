using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views
{
    public partial class HealerView : UserControl
    {
        public HealerView()
        {
            InitializeComponent();
        }

        private void OnHealClick(object? sender, RoutedEventArgs e)
        {
            var dialog = new Window
            {
                Width = 300,
                Height = 100,
                Content = new TextBlock
                {
                    Text = "You have been healed!",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                }
            };
            dialog.ShowDialog((Window)this.VisualRoot!);
        }

        private void OnManaClick(object? sender, RoutedEventArgs e)
        {
            var dialog = new Window
            {
                Width = 300,
                Height = 100,
                Content = new TextBlock
                {
                    Text = "Your mana has been restored!",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                }
            };
            dialog.ShowDialog((Window)this.VisualRoot!);
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            var mainWindow = this.VisualRoot as MainWindow;
            mainWindow?.SetContent(new MainGameMenuView());
        }
    }
}
