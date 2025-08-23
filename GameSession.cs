using AVGameRPG.Models;
using System.Collections.Generic;

namespace AVGameRPG
{
    public static class GameSession
    {
        public static Character Player { get; } = new Character();
        public static List<Item> Inventory { get; } = new();
        public static Equipment Equipment { get; } = new();

        // Treningi
        public static int AttackTrainCount { get; set; } = 0;
        public static int DefenseTrainCount { get; set; } = 0;
        public static int AgilityTrainCount { get; set; } = 0;

        // Ukończone questy
        public static HashSet<string> CompletedQuestIds { get; } = new();

        public static void CompleteQuest(string id)
        {
            CompletedQuestIds.Add(id);
        }
    }
}


