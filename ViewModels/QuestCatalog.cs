using System;
using System.Collections.Generic;
using System.Linq;

namespace AVGameRPG.Models
{
    public static class QuestCatalog
    {
        private record Entry(
            string Id,
            int MinLevel,
            bool RequiresBattle,
            string? EnemyName,
            int RewardGold,
            int RewardExp,
            TimeSpan Duration,
            string[] Titles,
            string[] Descriptions
        );

        // warianty nazw/treści dla questów logistycznych
        private static readonly string[] GatherTitles = { "Gather herbs", "Collect wheat", "Find mushrooms", "Collect wood" };
        private static readonly string[] GatherDescs = { "Collect 10 herbs.", "Harvest some wheat.", "Pick mushrooms in the forest.", "Gather firewood for the town." };

        private static readonly string[] EscortTitles = { "Escort the merchant", "Protect the caravan", "Guard a traveler" };
        private static readonly string[] EscortDescs = { "Protect the merchant on the way to the next town.", "Guard the caravan from raiders.", "Escort a lone traveler through the woods." };

        // BALANS
        private static readonly List<Entry> _entries = new()
        {
            new("q_escort",  1, false, null,   200,  300, TimeSpan.FromMinutes(1),
                EscortTitles, EscortDescs),
            new("q_gather",  1, false, null,    80,  300, TimeSpan.FromMinutes(2),
                GatherTitles, GatherDescs),
            new("q_rats",    3, false, null,   200,  300, TimeSpan.FromMinutes(2),
                new[]{"Clear the cellar"}, new[]{"Get rid of the rats in the town cellar."}),

            new("q_patrol",  6, false, null,   180, 350, TimeSpan.FromMinutes(2),
                new[]{"Town patrol"}, new[]{"Assist the guards during evening patrol."}),
            new("q_bandits", 7, true,  "Bandit", 180, 350, TimeSpan.FromMinutes(0.5),
                new[]{"Bandit ambush"}, new[]{"Bandits attack travelers on the road. Defeat them!"}),
            new("q_supplies",9, false, null,   220, 350, TimeSpan.FromMinutes(3),
                new[]{"Secure supplies"}, new[]{"Protect a convoy with supplies."}),

            new("q_bridge", 11, false, null,   260, 400, TimeSpan.FromMinutes(3),
                new[]{"Fix the bridge"}, new[]{"Help carpenters and defend the site occasionally."}),
            new("q_banditlead",13, true, "Bandit", 250, 400, TimeSpan.FromMinutes(0.6),
                new[]{"Bandit hunt"}, new[]{"Track and defeat the bandit leader."}),
            new("q_messengers",14,false, null,  300, 400, TimeSpan.FromMinutes(3),
                new[]{"Safe route"}, new[]{"Secure the messengers' route between towns."}),

            new("q_outpost",16,false, null,    360, 450, TimeSpan.FromMinutes(3),
                new[]{"Hold the outpost"}, new[]{"Defend the outpost from scouts."}),
            new("q_scouts", 17,true,  "Horde Scout", 340, 450, TimeSpan.FromMinutes(0.8),
                new[]{"Repel scouts"}, new[]{"Defeat horde scouts near the pass."}),
            new("q_fortify",18,false, null,    420, 450, TimeSpan.FromMinutes(3),
                new[]{"Fortify walls"}, new[]{"Help reinforce the town walls before the siege."})
        };

        private static readonly Entry _final = new(
            "q_warlord", 20, true, "Horde Warlord",
            1000, 0, TimeSpan.FromSeconds(30),
            new[] { "Slay the Horde Warlord" },
            new[] { "Face the mighty warlord of the horde!" }
        );

        public static List<Quest> GetAvailable(int playerLevel, IEnumerable<string>? completedIds = null)
        {
            var done = new HashSet<string>(completedIds ?? Enumerable.Empty<string>());
            var list = _entries
                .Where(e => playerLevel >= e.MinLevel && !done.Contains(e.Id))
                .Select(ToQuest)
                .ToList();

            if (playerLevel >= _final.MinLevel && !done.Contains(_final.Id))
                list.Add(ToQuest(_final));

            return list.OrderBy(q => GetMinLevelForId(GetId(q)))
                       .ThenBy(q => q.Title)
                       .ToList();
        }

        private static Quest ToQuest(Entry e)
        {
            var rand = new Random();
            string title = e.Titles[rand.Next(e.Titles.Length)];
            string desc = e.Descriptions[rand.Next(e.Descriptions.Length)];

            return new Quest
            {
                Title = title,
                Description = desc,
                Deadline = DateTime.Now.Add(e.Duration),
                RewardGold = e.RewardGold,
                RewardExp = e.RewardExp,
                RequiresBattle = e.RequiresBattle,
                EnemyName = e.EnemyName
            };
        }

        private static string GetId(Quest q)
        {
            // prosty map ID po tytule
            return q.Title switch
            {
                "Slay the Horde Warlord" => "q_warlord",
                "Escort the merchant" => "q_escort",
                "Protect the caravan" => "q_escort",
                "Guard a traveler" => "q_escort",
                "Gather herbs" => "q_gather",
                "Collect wheat" => "q_gather",
                "Find mushrooms" => "q_gather",
                "Collect wood" => "q_gather",
                _ => q.Title
            };
        }

        private static int GetMinLevelForId(string id)
        {
            if (id == _final.Id) return _final.MinLevel;
            var e = _entries.FirstOrDefault(x => x.Id == id);
            return e?.MinLevel ?? 1;
        }
    }
}


