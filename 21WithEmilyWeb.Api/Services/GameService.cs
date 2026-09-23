using _21WithEmilyWeb.Api.Data;
using _21WithEmilyWeb.Api.Models;

namespace _21WithEmilyWeb.Api.Services
{
    public class GameService
    {
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
                if (game.Winner == null)
                    PlayersTurn(game, count);

                if (game.Winner == null)
                    OpponentAi.Turn(game);

                await db.SaveChangesAsync();
                return GameToResponse(game);
            }
            return null;
        }

        private static void PlayersTurn(Game game, int count)
        {
            var validCounts = Utils.ValidCounts(game.Score);
            if (!validCounts.Contains(count))
            {
                throw new ArgumentOutOfRangeException();
            }

            game.Score = count;

            if (count >= Utils.GOAL)
            {
                game.Winner = Player.Computer;
            }
        }

        private static GameResponse GameToResponse(Game game)
        {
            var response = new GameResponse
            {
                GameId = game.Id,
                Score = game.Score,
                Winner = PlayerEnumToString(game.Winner),
                AllowedCounts = Utils.ValidCounts(game.Score)
            };
            return response;
        }

        private static string? PlayerEnumToString(Player? playerEnum)
        {
            switch (playerEnum)
            {
                case Player.Player:
                    return "Player";
                case Player.Computer:
                    return "Emily";
                default:
                    return null;
            }
        }
    }
}
