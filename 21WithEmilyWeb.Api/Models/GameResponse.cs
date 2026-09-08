namespace _21WithEmilyWeb.Api.Models
{
    public class GameResponse
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public Player? Winner { get; set; }
        public int[]? AllowedCounts { get; set; }
    }
}
