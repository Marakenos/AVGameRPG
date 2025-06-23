using Avalonia.Controls;
using Avalonia.Interactivity;
<<<<<<< HEAD
using Avalonia.Threading;
using System;
using System.Collections.Generic;

namespace AVGameRPG.Views
{
    public partial class TownHallView : UserControl
    {
        private List<Quest> _quests;
        private Quest? _selectedQuest;
        private DispatcherTimer? _timer;

        public TownHallView()
        {
            InitializeComponent();

            _quests = new List<Quest>
            {
                new Quest
                {
                    Title = "Escort the merchant",
                    Description = "Protect the merchant on the way to the next town.",
                    Deadline = DateTime.Now.AddMinutes(1),
                    IsCompleted = false,
                    RewardGold = 100,
                    RewardExp = 50
                }
            };

            RefreshQuestList();

            MissionsList.SelectionChanged += (s, e) =>
            {
                _selectedQuest = MissionsList.SelectedItem as Quest;
            };

            TimeRemainingText.IsVisible = false; // Zegar ukryty na starcie
        }

        private void OnAcceptMissionClick(object? sender, RoutedEventArgs e)
        {
            _selectedQuest = MissionsList.SelectedItem as Quest;

            if (_selectedQuest == null)
                return;

            TimeRemainingText.IsVisible = true;

            // Jeœli timer ju¿ by³ uruchomiony wczeœniej, zatrzymaj go
            _timer?.Stop();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };

            _timer.Tick += (_, _) =>
            {
                if (_selectedQuest != null)
                {
                    if (_selectedQuest.IsActive)
                    {
                        TimeRemainingText.Text = _selectedQuest.TimeRemaining.ToString(@"mm\:ss");
                    }
                    else
                    {
                        TimeRemainingText.Text = "Zakoñczono";
                        _timer.Stop();

                        // Po zakoñczeniu odliczania przejœcie do widoku walki Z QUESTEM
                        (this.VisualRoot as MainWindow)?.SetContent(new BattleView(_selectedQuest));
                    }
                }
            };

            _timer.Start();
        }
        private void RefreshQuestList()
        {
            MissionsList.ItemsSource = null;
            MissionsList.ItemsSource = _quests.FindAll(q => !q.IsCompleted);
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
        }
    }
}




=======

namespace AVGameRPG.Views;

public partial class TownHallView : UserControl
{
    public TownHallView()
    {
        InitializeComponent();
    }

    private void OnExitClick(object? sender, RoutedEventArgs e)
    {
        (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
    }
}
>>>>>>> 029769fbb2aa6b27cc0cd482ab7ffaaf6a148d20
