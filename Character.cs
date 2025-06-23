using System;

namespace AVGameRPG
{
    public class Character
    {
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int MinAttack { get; set; }
        public int MaxAttack { get; set; }
        public int Gold { get; set; }
        public int Experience { get; set; }

        private static readonly Random _rand = new Random();

        public int GetRandomAttack()
        {
            return _rand.Next(MinAttack, MaxAttack + 1);
        }

        public void TakeDamage(int damage)
        {
            CurrentHP = Math.Max(0, CurrentHP - damage);
        }

        public bool IsAlive => CurrentHP > 0;
    }
}
