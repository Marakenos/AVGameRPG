using System;

namespace AVGameRPG
{
    public class Quest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }

        public bool RequiresBattle { get; set; } = false;
        public string? EnemyName { get; set; }


        public bool IsActive => !IsCompleted && DateTime.Now < Deadline;
        public TimeSpan TimeRemaining => Deadline - DateTime.Now;
    }
}