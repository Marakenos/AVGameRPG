using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AVGameRPG.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void SetContent(UserControl control)
    {
        MainContent.Content = control;
    }
}
