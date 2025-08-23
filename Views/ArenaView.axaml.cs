using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG;

namespace AVGameRPG.Views
{
    public partial class ArenaView : UserControl
    {
        public ArenaView()
        {
            InitializeComponent();
            RefreshUI();
        }

        // koszt = 20, 30, 40, 50, 60 (zale�nie od aktualnego licznika 0..4)
        private static int CostFor(int currentCount) => 20 + 10 * currentCount;

        private void RefreshUI()
        {
            var p = GameSession.Player;

            PlayerGoldText.Text = p.Gold.ToString();

            // ATK
            AttackProgressText.Text = $"Post�p: {GameSession.AttackTrainCount}/5 (+5 ATK za trening)";
            AttackCostText.Text = GameSession.AttackTrainCount >= 5
                ? "Limit osi�gni�ty"
                : $"Koszt kolejnego treningu: {CostFor(GameSession.AttackTrainCount)} z�";

            // DEF
            DefenseProgressText.Text = $"Post�p: {GameSession.DefenseTrainCount}/5 (+5 DEF za trening)";
            DefenseCostText.Text = GameSession.DefenseTrainCount >= 5
                ? "Limit osi�gni�ty"
                : $"Koszt kolejnego treningu: {CostFor(GameSession.DefenseTrainCount)} z�";

            // AGI
            AgilityProgressText.Text = $"Post�p: {GameSession.AgilityTrainCount}/5 (+5 AGI za trening)";
            AgilityCostText.Text = GameSession.AgilityTrainCount >= 5
                ? "Limit osi�gni�ty"
                : $"Koszt kolejnego treningu: {CostFor(GameSession.AgilityTrainCount)} z�";
        }

        private void OnTrainAttackClick(object? sender, RoutedEventArgs e) => TrainStat(StatType.Attack);
        private void OnTrainDefenseClick(object? sender, RoutedEventArgs e) => TrainStat(StatType.Defense);
        private void OnTrainAgilityClick(object? sender, RoutedEventArgs e) => TrainStat(StatType.Agility);

        private enum StatType { Attack, Defense, Agility }

        private static int GetCount(StatType type) => type switch
        {
            StatType.Attack => GameSession.AttackTrainCount,
            StatType.Defense => GameSession.DefenseTrainCount,
            StatType.Agility => GameSession.AgilityTrainCount,
            _ => 0
        };

        private static void SetCount(StatType type, int value)
        {
            switch (type)
            {
                case StatType.Attack: GameSession.AttackTrainCount = value; break;
                case StatType.Defense: GameSession.DefenseTrainCount = value; break;
                case StatType.Agility: GameSession.AgilityTrainCount = value; break;
            }
        }

        private void TrainStat(StatType type)
        {
            var p = GameSession.Player;

            int counter = GetCount(type);
            if (counter >= 5)
            {
                ShowMessage("Osi�gni�to maksymalny poziom treningu (5/5).");
                return;
            }

            int cost = CostFor(counter);
            if (p.Gold < cost)
            {
                ShowMessage($"Za ma�o z�ota. Potrzeba {cost} z�.");
                return;
            }

            // zap�a�
            p.Gold -= cost;

            // efekt treningu
            switch (type)
            {
                case StatType.Attack:
                    p.MinAttack += 5;
                    p.MaxAttack += 5;
                    break;
                case StatType.Defense:
                    p.Defense += 5;
                    break;
                case StatType.Agility:
                    p.Agility += 5;
                    break;
            }

            // zwi�ksz licznik (bez ref � zwyk�e przypisanie)
            SetCount(type, counter + 1);

            RefreshUI();
            ShowMessage("Trening zako�czony sukcesem!");
        }

        private async void ShowMessage(string msg)
        {
            var w = this.VisualRoot as Window;
            if (w is null) return;

            var dialog = new Window
            {
                Width = 360,
                Height = 140,
                CanResize = false,
                Title = "Arena",
                Content = new TextBlock
                {
                    Text = msg,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                }
            };

            await dialog.ShowDialog(w);
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
        }
    }
}



