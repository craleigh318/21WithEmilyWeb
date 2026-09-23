using _21WithEmilyWeb.Api.Models;

namespace _21WithEmilyWeb.Api.Services
{
    public class OpponentAi
    {
        public static void Turn(Game game)
        {
            var validCounts = Utils.ValidCounts(game.Score);
            int selectedCount;
            if (validCounts.Contains(Utils.GOAL))
            {
                selectedCount = Utils.GOAL;
            }
            else
            {
                var randomIndex = Random.Shared.Next(validCounts!.Length);
                selectedCount = validCounts[randomIndex];
            }

            game.Score = selectedCount;

            if (selectedCount >= Utils.GOAL)
            {
                game.Winner = Player.Player;
            }
        }
    }
}
