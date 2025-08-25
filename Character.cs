using System;
using System.Text.Json.Serialization;

namespace AVGameRPG
{
    public class Character
    {
        public string Name { get; set; } = "Hero";

        // --- HP ---
        public int MaxHP { get; set; } = 100;
        public int CurrentHP { get; set; } = 100;

        // --- Mana ---
        public int MaxMana { get; set; } = 50;
        public int CurrentMana { get; set; } = 50;

        // --- Statystyki walki ---
        public int MinAttack { get; set; } = 5;
        public int MaxAttack { get; set; } = 10;
        public int Defense { get; set; } = 0;
        public int Agility { get; set; } = 0;
        public int Intelligence { get; set; } = 0;

        // --- Ekonomia ---
        public int Gold { get; set; } = 0;

        // --- Level / EXP ---
        // System.Text.Json nie zapisze/nie wczyta prywatnych setterów bez [JsonInclude].
        [JsonInclude]
        public int Level { get; private set; } = 1;

        [JsonInclude]
        public int Experience { get; private set; } = 0;

        public int ExpToNextLevel => 100 + (Level - 1) * 50;
        public int ExpRemaining => Math.Max(0, ExpToNextLevel - Experience);

        public void AddExperience(int amount)
        {
            if (amount <= 0) return;
            Experience += amount;
            while (Experience >= ExpToNextLevel)
            {
                Experience -= ExpToNextLevel;
                Level++;
            }
        }

        // --- Helpery HP/Mana ---
        public bool IsInjured => CurrentHP < MaxHP;
        public void HealToFull() => CurrentHP = MaxHP;

        public bool IsManaMissing => CurrentMana < MaxMana;
        public void RestoreManaToFull() => CurrentMana = MaxMana;

        public bool TrySpendMana(int cost)
        {
            if (cost <= 0) return true;
            if (CurrentMana < cost) return false;
            CurrentMana -= cost;
            return true;
        }

        // --- Walka fizyczna ---
        public int GetRandomAttack()
            => Random.Shared.Next(MinAttack, MaxAttack + 1);

        /// <summary>Oblicza otrzymane obrażenia z redukcją pancerza (min. 1, jeśli incoming > 0).</summary>
        public int ComputeDamageTaken(int incoming)
        {
            if (incoming <= 0) return 0;
            var def = Math.Max(0, Defense);
            var reduced = (int)Math.Round(incoming * (100.0 / (100.0 + def)));
            return Math.Max(1, reduced);
        }

        public void TakeDamage(int dmg)
        {
            var taken = ComputeDamageTaken(dmg);
            CurrentHP = Math.Max(0, CurrentHP - taken);
        }

        // --- Magia: skalowanie INT ---
        public int ComputeSpellPower(int basePower) =>
            basePower + (int)Math.Round(Intelligence * 0.6);

        // --- Kopiowanie stanu ---
        public void CopyFrom(Character other)
        {
            if (other == null) return;

            Name = other.Name;

            MaxHP = other.MaxHP;
            CurrentHP = Math.Min(other.CurrentHP, MaxHP);

            MaxMana = other.MaxMana == 0 ? 50 : other.MaxMana;
            CurrentMana = Math.Min(other.CurrentMana == 0 ? MaxMana : other.CurrentMana, MaxMana);

            MinAttack = other.MinAttack;
            MaxAttack = other.MaxAttack;
            Defense = other.Defense;
            Agility = other.Agility;
            Intelligence = other.Intelligence;

            Gold = other.Gold;

            // Te pola wczytają się poprawnie dzięki [JsonInclude],
            // a CopyFrom tylko je sanity-checkuje.
            Level = Math.Max(1, other.Level);
            Experience = Math.Max(0, other.Experience);
            while (Experience >= ExpToNextLevel)
            {
                Experience -= ExpToNextLevel;
                Level++;
            }
        }
    }
}




