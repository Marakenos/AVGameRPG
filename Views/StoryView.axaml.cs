using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG.Views;

namespace AVGameRPG.Views;

public partial class StoryView : UserControl
{
    public StoryView(string playerNick)
    {
        InitializeComponent();

        // Wstawiamy histori� z nickiem
        StoryText.Text = $@"
        By�o lato, rok 1000. Noc by�a bezchmurna i pe�na gwiazd.

        Gdy si� obudzi�e�, drogi {playerNick}, zd��y�e� tylko wy�cibi� g�ow� przez okno
        i ujrza�e� hord� zwierzoludzi naje�d�aj�cych Twoj� wiosk�.

        Razem z rodzin� uciekli�cie w pop�ochu do najbli�ej po�o�onego zamku 
        � Ch�teau de Couches.Teraz tam jeste�cie bezpieczni.
                                

        Lecz serce Twoje wci�� kipi ��dz� zemsty.
        Postanowi�e� zosta� najemnikiem, by kt�rego� dnia dopa�� naje�d�c�w
        i pom�ci� wszystkich, kt�rzy zgin�li podczas napa�ci.
        ";


    }

    private void OnContinueClick(object? sender, RoutedEventArgs e)
    {
        var mainWindow = this.VisualRoot as MainWindow;
        mainWindow?.SetContent(new MainGameMenuView());
    }
}
