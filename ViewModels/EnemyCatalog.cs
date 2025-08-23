using System;

namespace AVGameRPG.Models
{
    public static class EnemyCatalog
    {
        public static Character Create(string? enemyName)
        {
            return enemyName switch
            {
                "Horde Warlord" => new Character
                {
                    Name = "Horde Warlord",
                    MaxHP = 500,
                    CurrentHP = 500,
                    MinAttack = 25,
                    MaxAttack = 40,
                    Defense = 8
                },
                "Bandit" => new Character
                {
                    Name = "Bandit",
                    MaxHP = 120,
                    CurrentHP = 120,
                    MinAttack = 10,
                    MaxAttack = 18,
                    Defense = 2
                },
                "Horde Scout" => new Character
                {
                    Name = "Horde Scout",
                    MaxHP = 160,
                    CurrentHP = 160,
                    MinAttack = 12,
                    MaxAttack = 20,
                    Defense = 3
                },
                _ => CreateGoblin(enemyName)
            };
        }

        private static Character CreateGoblin(string? name)
        {
            var rnd = Random.Shared;
            var hp = rnd.Next(40, 80);

            return new Character
            {
                Name = string.IsNullOrWhiteSpace(name) ? "Goblin" : name,
                MaxHP = hp,
                CurrentHP = hp,
                MinAttack = rnd.Next(2, 6),
                MaxAttack = rnd.Next(7, 12),
                Defense = 0
            };
        }
    }
}
