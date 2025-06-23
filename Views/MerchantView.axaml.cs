using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views;

public partial class MerchantView : UserControl
{
    public MerchantView()
    {
        InitializeComponent();
    }

    private void OnExitClick(object? sender, RoutedEventArgs e)
    {
        if (this.VisualRoot is MainWindow main)
        {
            main.SetContent(new MainGameMenuView());
        }
    }
}