namespace AVGameRPG.Models
{
    public enum SpellType { Damage, Heal }

    public class Spell
    {
        public string Name { get; set; } = "";
        public SpellType Type { get; set; }
        public int ManaCost { get; set; }
        public int Power { get; set; }        // bazowa moc – skaluje się przez Intelligence
        public string Description { get; set; } = "";
    }
}


