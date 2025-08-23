using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG;
using AVGameRPG.Models;

namespace AVGameRPG.Views
{
    public partial class BattleView : UserControl
    {
        private Character _player;
        private Character _enemy;
        private Quest _selectedQuest;
        private bool _battleEnded = false;

        private const int HealManaCost = 8; // koszt many za Heal

        public BattleView(Quest quest)
        {
            InitializeComponent();

            _selectedQuest = quest;
            _player = GameSession.Player;

            _enemy = EnemyCatalog.Create(quest.EnemyName);

            UpdateStats();
            Log($"Battle started! {_enemy.Name} stands before you.");
        }


        private void OnAttackClick(object? sender, RoutedEventArgs e)
        {
            if (_battleEnded || _player.CurrentHP <= 0 || _enemy.CurrentHP <= 0) return;

            int dmg = _player.GetRandomAttack();
            _enemy.TakeDamage(dmg);
            Log($"Player attacks for {dmg} damage.");

            if (_enemy.CurrentHP > 0)
            {
                int counter = _enemy.GetRandomAttack();
                _player.TakeDamage(counter);
                Log($"{_enemy.Name} counterattacks for {counter} damage.");
                if (_player.CurrentHP <= 0) EndBattle(false);
            }
            else EndBattle(true);

            UpdateStats();
        }

        private void OnDefendClick(object? sender, RoutedEventArgs e)
        {
            if (_battleEnded || _enemy.CurrentHP <= 0 || _player.CurrentHP <= 0) return;

            int reduced = _enemy.GetRandomAttack() / 2;
            _player.TakeDamage(reduced);
            Log($"Player defends! Takes only {reduced} damage.");

            if (_player.CurrentHP <= 0) EndBattle(false);
            UpdateStats();
        }

        // HEAL – teraz zu¿ywa manê
        private void OnHealClick(object? sender, RoutedEventArgs e)
        {
            if (_battleEnded || _enemy.CurrentHP <= 0 || _player.CurrentHP <= 0) return;

            if (!_player.TrySpendMana(HealManaCost))
            {
                Log("Not enough mana!");
                UpdateStats();
                return;
            }

            int healed = Random.Shared.Next(5, 15) + (int)Math.Round(_player.Intelligence * 0.3);
            int before = _player.CurrentHP;
            _player.CurrentHP = Math.Min(_player.MaxHP, _player.CurrentHP + healed);
            Log($"Player heals for {_player.CurrentHP - before} HP (-{HealManaCost} MP).");

            if (_enemy.CurrentHP > 0)
            {
                int counter = _enemy.GetRandomAttack();
                _player.TakeDamage(counter);
                Log($"{_enemy.Name} attacks for {counter} damage.");
                if (_player.CurrentHP <= 0) EndBattle(false);
            }

            UpdateStats();
        }

        private void OnDodgeClick(object? sender, RoutedEventArgs e)
        {
            if (_battleEnded || _enemy.CurrentHP <= 0 || _player.CurrentHP <= 0) return;

            bool success = Random.Shared.NextDouble() < 0.5;
            if (success) Log("Player dodges the attack!");
            else
            {
                int counter = _enemy.GetRandomAttack();
                _player.TakeDamage(counter);
                Log($"Dodge failed! {_enemy.Name} hits for {counter} damage.");
                if (_player.CurrentHP <= 0) EndBattle(false);
            }

            UpdateStats();
        }

        private void OnCastClick(object? sender, RoutedEventArgs e)
        {
            if (_battleEnded || _player.CurrentHP <= 0 || _enemy.CurrentHP <= 0) return;

            var spells = SpellBook.GetAvailableSpells(_player);
            if (spells.Count == 0) { Log("No spells available."); return; }

            var spell = spells[0];

            if (!_player.TrySpendMana(spell.ManaCost))
            {
                Log("Not enough mana!");
                UpdateStats();
                return;
            }

            int power = _player.ComputeSpellPower(spell.Power);

            if (spell.Type == SpellType.Damage)
            {
                _enemy.TakeDamage(power);
                Log($"Player casts {spell.Name} for {power} damage (-{spell.ManaCost} MP).");

                if (_enemy.CurrentHP <= 0) EndBattle(true);
                else
                {
                    int counter = _enemy.GetRandomAttack();
                    _player.TakeDamage(counter);
                    Log($"{_enemy.Name} counterattacks for {counter} damage.");
                    if (_player.CurrentHP <= 0) EndBattle(false);
                }
            }
            else // Heal
            {
                int before = _player.CurrentHP;
                _player.CurrentHP = Math.Min(_player.MaxHP, _player.CurrentHP + power);
                Log($"Player casts {spell.Name} and heals {_player.CurrentHP - before} HP (-{spell.ManaCost} MP).");

                if (_enemy.CurrentHP > 0)
                {
                    int counter = _enemy.GetRandomAttack();
                    _player.TakeDamage(counter);
                    Log($"{_enemy.Name} attacks for {counter} damage.");
                    if (_player.CurrentHP <= 0) EndBattle(false);
                }
            }

            UpdateStats();
        }

        private void EndBattle(bool playerWon)
        {
            _battleEnded = true;

            if (playerWon)
            {
                Log($"{_enemy.Name} has been defeated!");
                Log($"+{_selectedQuest.RewardGold} gold, +{_selectedQuest.RewardExp} EXP!");
                _selectedQuest.IsCompleted = true;

                _player.Gold += _selectedQuest.RewardGold;
                _player.AddExperience(_selectedQuest.RewardExp);
                GameSession.CompleteQuest(_selectedQuest.Title);
            }
            else Log($"Player has fallen... {_enemy.Name} wins the battle.");

            ExitButton.IsVisible = true;
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new TownHallView());
        }

        private void UpdateStats()
        {
            // Gracz
            PlayerHpText.Text = $"HP: {_player.CurrentHP}/{_player.MaxHP}";
            PlayerAtkText.Text = $"Attack: {_player.MinAttack}-{_player.MaxAttack}";
            PlayerGoldText.Text = $"Gold: {_player.Gold}";
            PlayerExpText.Text = $"EXP: {_player.Experience}/{_player.ExpToNextLevel}";
            // jeœli masz:
            // PlayerManaText.Text = $"Mana: {_player.CurrentMana}/{_player.MaxMana}";

            // Wróg (dynamicznie)
            EnemyNameText.Text = _enemy.Name;
            EnemyHpText.Text = $"HP: {_enemy.CurrentHP}/{_enemy.MaxHP}";
            EnemyAtkText.Text = $"Attack: {_enemy.MinAttack}-{_enemy.MaxAttack}";
        }


        private void Log(string message) => BattleLogText.Text += $"\n{message}";
    }
}




