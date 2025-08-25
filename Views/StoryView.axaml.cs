using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG.Views;

namespace AVGameRPG.Views;

public partial class StoryView : UserControl
{
    public StoryView(string playerNick)
    {
        InitializeComponent();

        // ustaw nick w sesji (fallback: "Hero")
        GameSession.Player.Name = string.IsNullOrWhiteSpace(playerNick) ? "Hero" : playerNick.Trim();

        // u¿yj nicku z sesji w historii
        var name = GameSession.Player.Name;
        StoryText.Text = $@"
        Summer, in the year 1000. The night was clear, the heavens ablaze with stars.
        When you awoke, {name}, you cast your gaze through the window — only to witness a horde of beastmen descending upon your village in fury.
        With your family, you fled in desperation, seeking refuge within the nearest stronghold:
        the Château de Couches. Behind its stone walls you found safety… for now.
        But your heart knows no rest. It burns with a thirst for vengeance.
        You have sworn to take up the path of the mercenary, so that one day you may track down the raiders — and avenge all who perished in the flames of that night.";
    }

    private void OnContinueClick(object? sender, RoutedEventArgs e)
        => (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
}

