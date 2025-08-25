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
                    MinAttack = 50,
                    MaxAttack = 87,
                    Defense = 50
                },
                "Bandit" => new Character
                {
                    Name = "Bandit",
                    MaxHP = 175,
                    CurrentHP = 175,
                    MinAttack = 10,
                    MaxAttack = 30,
                    Defense = 20
                },
                "Horde Scout" => new Character
                {
                    Name = "Horde Scout",
                    MaxHP = 350,
                    CurrentHP = 350,
                    MinAttack = 40,
                    MaxAttack = 74,
                    Defense = 40
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
