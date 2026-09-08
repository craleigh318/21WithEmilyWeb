using _21WithEmilyWeb.Api.Data;
using _21WithEmilyWeb.Api.Models;

namespace _21WithEmilyWeb.Api.Services
{
    public class GameService
    {
        public const int GOAL = 21;
        
        private readonly AppDbContext db;

        public GameService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<GameResponse> NewGame()
        {
            var game = new Game { Score = 0 };
            db.Games.Add(game);
            await db.SaveChangesAsync();
            var response = GameToResponse(game);
            return response;
        }

        /*private static void NextTurn(Game game)
        {
            var validCounts = ValidCounts(game.Score);
            game.AllowedCounts = validCounts;
            if (validCounts == null && game.Winner == null)
                game.Winner = Player.Computer;
        }*/

        private static GameResponse GameToResponse(Game game)
        {
            var response = new GameResponse
            {
                Id = game.Id,
                Score = game.Score,
                Winner = game.Winner,
                AllowedCounts = ValidCounts(game.Score)
            };
            return response;
        }

        private static int[]? ValidCounts(int score)
        {
            int[]? countsArray;
            var countsSet = new SortedSet<int>();
            for (var i = 1; i <= 3; i++)
            {
                var count = score + i;
                if (count > GOAL)
                    break;
                countsSet.Add(count);
            }
            if (countsSet.Count > 0)
                countsArray = countsSet.ToArray();
            else
                countsArray = null;
            return countsArray;
        }
    }
}
