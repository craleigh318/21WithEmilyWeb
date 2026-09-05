namespace _21WithEmilyWeb.Api.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public Player? Winner { get; set; }
    }
}
