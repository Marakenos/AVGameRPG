using System.Collections.Generic;

namespace AVGameRPG.Models
{
    public static class SpellBook
    {
        //To wersja beta więc gracz ma dostępne tylko 2 czary, ale można by to spokojnie rozwinąć.
        //Czary wzmacniają się wraz ze wzrostem intelektu.
        public static List<Spell> GetAvailableSpells(Character caster) => new()
        {
            new Spell
            {
                Name = "Firebolt",
                Type = SpellType.Damage,
                ManaCost = 10,
                Power = 20,
                Description = "Simple fire projectile."
            },
            new Spell
            {
                Name = "Minor Heal",
                Type = SpellType.Heal,
                ManaCost = 8,
                Power = 18,
                Description = "Restore a small amount of health."
            }
        };
    }
}
