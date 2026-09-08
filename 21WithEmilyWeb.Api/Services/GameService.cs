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
            await db.Games.AddAsync(game);
            await db.SaveChangesAsync();
            return GameToResponse(game);
        }

        public async Task<GameResponse?> Count(int gameId, int count)
        {
            var game = await db.Games.FindAsync(gameId);
            if (game != null)
            {
                var validCounts = ValidCounts(game.Score);
                if (!validCounts.Contains(count))
                {
                    throw new ArgumentOutOfRangeException();
                }

                game.Score = count;

                if (count >= GOAL)
                {
                    game.Winner = Player.Computer;
                }

                await db.SaveChangesAsync();
                return GameToResponse(game);
            }
            return null;
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
                GameId = game.Id,
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
