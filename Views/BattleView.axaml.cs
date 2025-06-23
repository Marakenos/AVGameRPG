using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using AVGameRPG.Models;
using System;


namespace AVGameRPG.Views
{
    public partial class BattleView : UserControl
    {
        private Character _player;
        private Character _enemy;
        private Quest _selectedQuest;
        private bool _battleEnded = false;

        public BattleView(Quest quest)
        {
            InitializeComponent();

            _selectedQuest = quest;
            _player = GameSession.Player;

            _enemy = new Character
            {
                Name = "Goblin",
                MaxHP = new Random().Next(40, 80),
                CurrentHP = 0,
                MinAttack = new Random().Next(2, 6),
                MaxAttack = new Random().Next(7, 12)
            };
            _enemy.CurrentHP = _enemy.MaxHP;

            UpdateStats();
            Log("Walka rozpoczêta! Goblin stoi przed tob¹.");
        }

        private void OnAttackClick(object? sender, RoutedEventArgs e)
        {
            if (_battleEnded || _player.CurrentHP <= 0 || _enemy.CurrentHP <= 0)
                return;

            int dmg = _player.GetRandomAttack();
            _enemy.TakeDamage(dmg);
            Log($"Gracz atakuje za {dmg} obra¿eñ.");

            if (_enemy.CurrentHP > 0)
            {
                int counter = _enemy.GetRandomAttack();
                _player.TakeDamage(counter);
                Log($"Goblin kontratakuje za {counter} obra¿eñ.");
            }
            else
            {
                EndBattle(true);
            }

            if (_player.CurrentHP <= 0)
            {
                EndBattle(false);
            }

            UpdateStats();
        }

        private void OnDefendClick(object? sender, RoutedEventArgs e)
        {
            if (_battleEnded) return;

            int reduced = _enemy.GetRandomAttack() / 2;
            _player.TakeDamage(reduced);
            Log($"Gracz siê broni! Otrzymuje tylko {reduced} obra¿eñ.");
            UpdateStats();
        }

        private void OnHealClick(object? sender, RoutedEventArgs e)
        {
            if (_battleEnded) return;

            int healed = new Random().Next(5, 15);
            _player.CurrentHP = Math.Min(_player.MaxHP, _player.CurrentHP + healed);
            Log($"Gracz leczy siê o {healed} HP.");
            UpdateStats();
        }

        private void OnDodgeClick(object? sender, RoutedEventArgs e)
        {
            if (_battleEnded) return;

            var rand = new Random();
            bool success = rand.NextDouble() < 0.5;
            if (success)
            {
                Log("Gracz unika ataku goblina!");
            }
            else
            {
                int counter = _enemy.GetRandomAttack();
                _player.TakeDamage(counter);
                Log($"Unik nieudany! Goblin trafia za {counter} obra¿eñ.");
            }
            UpdateStats();
        }

        private void EndBattle(bool playerWon)
        {
            _battleEnded = true;
            if (playerWon)
            {
                Log("Goblin zosta³ pokonany!");
                Log($"+{_selectedQuest.RewardGold} z³ota, +{_selectedQuest.RewardExp} EXP!");
                _selectedQuest.IsCompleted = true;

                _player.Gold += _selectedQuest.RewardGold;
                _player.Experience += _selectedQuest.RewardExp;
            }
            else
            {
                Log("Gracz poleg³! Goblin wygrywa walkê.");
            }

            ExitButton.IsVisible = true;
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new TownHallView());
        }

        private void UpdateStats()
        {
            PlayerHpText.Text = $"HP: {_player.CurrentHP}";
            PlayerAtkText.Text = $"Atak: {_player.MinAttack}-{_player.MaxAttack}";
            PlayerGoldText.Text = $"Z³oto: {_player.Gold}";
            PlayerExpText.Text = $"EXP: {_player.Experience}";

            EnemyHpText.Text = $"HP: {_enemy.CurrentHP}";
            EnemyAtkText.Text = $"Atak: {_enemy.MinAttack}-{_enemy.MaxAttack}";
        }

        private void Log(string message)
        {
            BattleLogText.Text += $"\n{message}";
        }
    }
}