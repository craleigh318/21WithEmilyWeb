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

        public async Task<int> NewGame()
        {
            var game = new Game { Score = 0 };
            db.Games.Add(game);
            await db.SaveChangesAsync();
            return game.Id;
        }
    }
}
