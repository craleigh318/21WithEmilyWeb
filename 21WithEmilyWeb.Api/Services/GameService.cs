namespace _21WithEmilyWeb.Api.Services
{
    public class GameService
    {
        private int nextId = 0;

        public int NewGame()
        {
            return nextId++;
        }
    }
}
