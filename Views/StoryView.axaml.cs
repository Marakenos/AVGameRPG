using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG.Views;

namespace AVGameRPG.Views;

public partial class StoryView : UserControl
{
    public StoryView(string playerNick)
    {
        InitializeComponent();

        // Wstawiamy historiê z nickiem
        StoryText.Text = $@"
        By³o lato, rok 1000. Noc by³a bezchmurna i pe³na gwiazd.

        Gdy siê obudzi³eœ, drogi {playerNick}, zd¹¿y³eœ tylko wyœcibiæ g³owê przez okno
        i ujrza³eœ hordê zwierzoludzi naje¿d¿aj¹cych Twoj¹ wioskê.

        Razem z rodzin¹ uciekliœcie w pop³ochu do najbli¿ej po³o¿onego zamku 
        – Château de Couches.Teraz tam jesteœcie bezpieczni.
                                

        Lecz serce Twoje wci¹¿ kipi ¿¹dz¹ zemsty.
        Postanowi³eœ zostaæ najemnikiem, by któregoœ dnia dopaœæ najeŸdŸców
        i pomœciæ wszystkich, którzy zginêli podczas napaœci.
        ";


    }

    private void OnContinueClick(object? sender, RoutedEventArgs e)
    {
        var mainWindow = this.VisualRoot as MainWindow;
        mainWindow?.SetContent(new MainGameMenuView());
    }
}
