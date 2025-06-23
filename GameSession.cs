namespace AVGameRPG.Models
{
    public static class GameSession
    {
        public static Character Player { get; } = new Character
        {
            Name = "Gracz",
            MaxHP = 100,
            CurrentHP = 100,
            MinAttack = 5,
            MaxAttack = 15,
            Gold = 0,
            Experience = 0
        };
    }
}
