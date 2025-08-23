using System.Collections.Generic;
using System.Linq;

namespace AVGameRPG.Models
{
    public static class ShopData
    {
        private static int PriceForLevel(int lvl) => lvl switch
        {
            5 => 100,
            10 => 250,
            15 => 600,
            20 => 1500,
            _ => 100
        };

        private static Item Make(string name, ItemCategory cat, int lvl,
                                 int atk = 0, int def = 0, int agi = 0, int intel = 0, int hp = 0)
        {
            return new Item
            {
                Name = name,
                Category = cat,
                RequiredLevel = lvl,
                Price = PriceForLevel(lvl),
                Attack = atk,
                Defense = def,
                Agility = agi,
                Intelligence = intel,
                Hp = hp,
                Description = BuildDescription(atk, def, agi, intel, hp)
            };
        }

        private static string BuildDescription(int atk, int def, int agi, int intel, int hp)
        {
            var parts = new List<string>();
            if (atk != 0) parts.Add($"+{atk} ATK");
            if (def != 0) parts.Add($"+{def} DEF");
            if (agi != 0) parts.Add($"+{agi} AGI");
            if (intel != 0) parts.Add($"+{intel} INT");
            if (hp != 0) parts.Add($"+{hp} HP");
            return string.Join(", ", parts);
        }

        public static List<Item> BuildDefault()
        {
            var L5 = 5; var L10 = 10; var L15 = 15; var L20 = 20;
            var items = new List<Item>();

            // HELMETS
            items.Add(Make("Rusty Helmet", ItemCategory.Head, L5, def: 5));
            items.Add(Make("Steel Helmet", ItemCategory.Head, L10, def: 15));
            items.Add(Make("Castle Guard Helmet", ItemCategory.Head, L15, def: 20, intel: 5));
            items.Add(Make("Horde Slayer Cap", ItemCategory.Head, L20, def: 30, intel: 10, agi: 10));

            // ARMOR
            items.Add(Make("Leather Armor", ItemCategory.Armor, L5, def: 10, agi: 2, hp: 10));
            items.Add(Make("Mercenary Coat", ItemCategory.Armor, L10, def: 20, agi: 8, hp: 20));
            items.Add(Make("Castle Guard Armor", ItemCategory.Armor, L15, def: 30, agi: 4, hp: 40));
            items.Add(Make("Horde Slayer Cloak", ItemCategory.Armor, L20, def: 50, agi: 15, hp: 50));

            // RINGS
            items.Add(Make("Copper Ring", ItemCategory.Ring, L5, atk: 3, intel: 3, agi: 3));
            items.Add(Make("Silver Ring", ItemCategory.Ring, L10, atk: 6, agi: 6, hp: 15));
            items.Add(Make("Golden Ring", ItemCategory.Ring, L15, atk: 3, intel: 3, agi: 3, hp: 30));
            items.Add(Make("Diamond Ring", ItemCategory.Ring, L20, atk: 5, intel: 5, agi: 5, hp: 30));

            // NECKLACES
            items.Add(Make("Leather Strap", ItemCategory.Necklace, L5, hp: 20));
            items.Add(Make("Amber Necklace", ItemCategory.Necklace, L10, intel: 6, agi: 6));
            items.Add(Make("Abbot's Necklace", ItemCategory.Necklace, L15, atk: 5, intel: 5, agi: 5));
            items.Add(Make("Wizard's Necklace", ItemCategory.Necklace, L20, atk: 7, intel: 7, agi: 7, def: 15));

            // GLOVES
            items.Add(Make("Torn Gloves", ItemCategory.Gloves, L5, hp: 15));
            items.Add(Make("Leather Gloves", ItemCategory.Gloves, L10, agi: 10, def: 10));
            items.Add(Make("Reinforced Gloves", ItemCategory.Gloves, L15, def: 15, hp: 15));
            items.Add(Make("Heavy Gauntlets", ItemCategory.Gloves, L20, def: 30, hp: 30));

            // WEAPONS
            items.Add(Make("Rusty Sword", ItemCategory.Weapon, L5, atk: 10));
            items.Add(Make("Steel Sword", ItemCategory.Weapon, L10, atk: 15, agi: 10));
            items.Add(Make("Castle Guard Sword", ItemCategory.Weapon, L15, atk: 20, agi: 15));
            items.Add(Make("Elven Sabre", ItemCategory.Weapon, L20, atk: 30, agi: 20, hp: 30));

            // SHIELDS
            items.Add(Make("Wooden Shield", ItemCategory.Shield, L5, def: 10, hp: 10));
            items.Add(Make("Reinforced Shield", ItemCategory.Shield, L10, def: 20, hp: 20));
            items.Add(Make("Guard's Shield", ItemCategory.Shield, L15, def: 30, agi: 5, hp: 20));
            items.Add(Make("Dwarven Shield", ItemCategory.Shield, L20, def: 60, hp: 30));

            // BOOTS
            items.Add(Make("Worn Boots", ItemCategory.Boots, L5, def: 5));
            items.Add(Make("Leather Boots", ItemCategory.Boots, L10, def: 10, agi: 5));
            items.Add(Make("Castle Guard Sandals", ItemCategory.Boots, L15, def: 15, agi: 15));
            items.Add(Make("Elven Slippers", ItemCategory.Boots, L20, def: 10, agi: 20));

            // MISC
            items.Add(Make("Talisman of Chance", ItemCategory.Misc, L5, agi: 6));
            items.Add(Make("Lucky Stone", ItemCategory.Misc, L10, agi: 10, intel: 10));
            items.Add(Make("Life Stone", ItemCategory.Misc, L15, hp: 30, def: 10));
            items.Add(Make("Jade Figurine", ItemCategory.Misc, L20, intel: 10, agi: 15));

            return items
                .OrderBy(i => i.Category)
                .ThenBy(i => i.RequiredLevel)
                .ToList();
        }
    }
}
