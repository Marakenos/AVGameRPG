using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using AVGameRPG;
using AVGameRPG.Models;

namespace AVGameRPG.Views
{
    public partial class TownHallView : UserControl
    {
        private readonly TimeSpan MaxMissionDuration = TimeSpan.FromSeconds(45);
        private List<Quest> _quests = new();
        private Quest? _selectedQuest;
        private DispatcherTimer? _timer;
        private DateTime? _missionEndsAt; // lokalny licznik

        public TownHallView()
        {
            InitializeComponent();

            _quests = QuestCatalog.GetAvailable(GameSession.Player.Level, GameSession.CompletedQuestIds);
            RefreshQuestList();

            MissionsList.SelectionChanged += (_, __) =>
            {
                _selectedQuest = MissionsList.SelectedItem as Quest;
            };

            TimeRemainingText.IsVisible = false;
        }

        private void OnAcceptMissionClick(object? sender, RoutedEventArgs e)
        {
            if (_selectedQuest is null)
                return;

            if (_selectedQuest.RequiresBattle)
            {
                (this.VisualRoot as MainWindow)?.SetContent(new BattleView(_selectedQuest));
            }
            else
            {
                //min(pozosta³y czas questa, 45s)
                var remainingNow = _selectedQuest.TimeRemaining;
                var capped = remainingNow <= TimeSpan.Zero
                    ? TimeSpan.Zero
                    : (remainingNow < MaxMissionDuration ? remainingNow : MaxMissionDuration);

                _missionEndsAt = DateTime.UtcNow + capped;

                TimeRemainingText.IsVisible = true;

                _timer?.Stop();
                _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
                _timer.Tick += (_, __) =>
                {
                    if (_selectedQuest is null || _missionEndsAt is null)
                        return;

                    var remaining = _missionEndsAt.Value - DateTime.UtcNow;
                    if (remaining <= TimeSpan.Zero)
                    {
                        GameSession.Player.Gold += _selectedQuest.RewardGold;
                        GameSession.Player.AddExperience(_selectedQuest.RewardExp);

                        _selectedQuest.IsCompleted = true;
                        GameSession.CompleteQuest(_selectedQuest.Title);

                        TimeRemainingText.Text = "Mission completed!";
                        _timer?.Stop();

                        _quests = QuestCatalog.GetAvailable(GameSession.Player.Level, GameSession.CompletedQuestIds);
                        RefreshQuestList();
                    }
                    else
                    {
                        TimeRemainingText.Text = $"{remaining:mm\\:ss}";
                    }
                };
                _timer.Start();
            }
        }

        private void RefreshQuestList()
        {
            var list = _quests
                .Where(q => !(q.EnemyName == "Horde Warlord" && GameSession.Player.Level < 20) && !q.IsCompleted)
                .ToList();

            MissionsList.ItemsSource = null;
            MissionsList.ItemsSource = list;

            if (list.Count == 0)
                TimeRemainingText.IsVisible = false;
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
        }
    }
}






