using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views
{
    public partial class MessageBoxWindow : Window
    {
        public MessageBoxWindow(string message)
        {
            InitializeComponent();
            MessageText.Text = message;
        }

        private void OnOkClick(object? sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

