using System;
using System.Text.Json.Serialization;

namespace AVGameRPG.Models
{
    public class Equipment
    {
        [JsonInclude] public Item? Head { get; private set; }
        [JsonInclude] public Item? Armor { get; private set; }
        [JsonInclude] public Item? Ring { get; private set; }
        [JsonInclude] public Item? Necklace { get; private set; }
        [JsonInclude] public Item? Gloves { get; private set; }
        [JsonInclude] public Item? Weapon { get; private set; }
        [JsonInclude] public Item? Shield { get; private set; }
        [JsonInclude] public Item? Boots { get; private set; }
        [JsonInclude] public Item? Misc { get; private set; }

        public Item? GetSlot(ItemCategory cat) => cat switch
        {
            ItemCategory.Head => Head,
            ItemCategory.Armor => Armor,
            ItemCategory.Ring => Ring,
            ItemCategory.Necklace => Necklace,
            ItemCategory.Gloves => Gloves,
            ItemCategory.Weapon => Weapon,
            ItemCategory.Shield => Shield,
            ItemCategory.Boots => Boots,
            ItemCategory.Misc => Misc,
            _ => null
        };

        private void SetSlot(ItemCategory cat, Item? item)
        {
            switch (cat)
            {
                case ItemCategory.Head: Head = item; break;
                case ItemCategory.Armor: Armor = item; break;
                case ItemCategory.Ring: Ring = item; break;
                case ItemCategory.Necklace: Necklace = item; break;
                case ItemCategory.Gloves: Gloves = item; break;
                case ItemCategory.Weapon: Weapon = item; break;
                case ItemCategory.Shield: Shield = item; break;
                case ItemCategory.Boots: Boots = item; break;
                case ItemCategory.Misc: Misc = item; break;
            }
        }

        /// <summary>Zakłada przedmiot i automatycznie zdejmuje poprzedni – modyfikuje staty gracza.</summary>
        public void Equip(AVGameRPG.Character player, Item item)
        {
            if (item is null) return;

            var prev = GetSlot(item.Category);
            if (prev != null) RemoveBonuses(player, prev);

            SetSlot(item.Category, item);
            ApplyBonuses(player, item);
        }

        /// <summary>Ściąga item ze slotu (bez zakładania innego).</summary>
        public void Unequip(AVGameRPG.Character player, ItemCategory cat)
        {
            var prev = GetSlot(cat);
            if (prev == null) return;

            RemoveBonuses(player, prev);
            SetSlot(cat, null);
        }

        private static void ApplyBonuses(AVGameRPG.Character p, Item i)
        {
            p.MinAttack += i.Attack;
            p.MaxAttack += i.Attack;

            p.Defense += i.Defense;
            p.Agility += i.Agility;
            p.Intelligence += i.Intelligence;

            p.MaxHP += i.Hp;
            p.CurrentHP = Math.Min(p.CurrentHP + i.Hp, p.MaxHP);
        }

        private static void RemoveBonuses(AVGameRPG.Character p, Item i)
        {
            p.MinAttack -= i.Attack;
            p.MaxAttack -= i.Attack;

            p.Defense -= i.Defense;
            p.Agility -= i.Agility;
            p.Intelligence -= i.Intelligence;

            p.MaxHP -= i.Hp;
            p.CurrentHP = Math.Min(p.CurrentHP, p.MaxHP);
        }
    }
}

