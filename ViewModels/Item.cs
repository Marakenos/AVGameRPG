namespace AVGameRPG.Models
{
    public class Item
    {
        public string Name { get; set; } = "";
        public ItemCategory Category { get; set; }
        public int RequiredLevel { get; set; }   // 5, 10, 15, 20
        public int Price { get; set; }

        // Modyfikatory
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Hp { get; set; }              // dodatkowe HP

        public string Description { get; set; } = "";
    }
}



