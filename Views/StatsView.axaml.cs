using Avalonia.Controls;
using Avalonia.Interactivity;


namespace AVGameRPG.Views
{
    public partial class StatsView : UserControl
    {
        // Prywatne pola (inne nazwy ni¿ w XAML)
        private TextBlock _levelText = null!;
        private TextBlock _hpText = null!;
        private TextBlock _atkText = null!;
        private TextBlock _defText = null!;
        private TextBlock _agiText = null!;
        private TextBlock _intText = null!;
        private TextBlock _goldText = null!;
        private TextBlock _expText = null!;


        public StatsView()
        {
            InitializeComponent();


            // Pobranie referencji po x:Name
            _levelText = this.FindControl<TextBlock>("PlayerLevelText");
            _hpText = this.FindControl<TextBlock>("PlayerHpText");
            _atkText = this.FindControl<TextBlock>("PlayerAtkText");
            _defText = this.FindControl<TextBlock>("PlayerDefText");
            _agiText = this.FindControl<TextBlock>("PlayerAgiText");
            _intText = this.FindControl<TextBlock>("PlayerIntText");
            _goldText = this.FindControl<TextBlock>("PlayerGoldText");
            _expText = this.FindControl<TextBlock>("PlayerExpText");


            UpdateUI();
        }


        private void UpdateUI()
        {
            var p = GameSession.Player;

            PlayerNameText.Text = $"Name: {GameSession.Player.Name}";
            _levelText.Text = $"Poziom: {p.Level}";
            _hpText.Text = $"HP: {p.CurrentHP}/{p.MaxHP}";
            _atkText.Text = $"Atak: {p.MinAttack}-{p.MaxAttack}";


            _defText.Text = $"Obrona: {p.Defense}";
            _agiText.Text = $"Zwinnoœæ: {p.Agility}";
            _intText.Text = $"Inteligencja: {p.Intelligence}";


            _goldText.Text = $"Z³oto: {p.Gold}";
            _expText.Text = $"EXP: {p.Experience}/{p.ExpToNextLevel}";
        }


        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
        }
    }
}



