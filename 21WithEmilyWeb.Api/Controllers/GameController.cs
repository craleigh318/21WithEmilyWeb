using _21WithEmilyWeb.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _21WithEmilyWeb.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService service;

        public GameController(GameService service)
        {
            this.service = service;
        }

        [HttpPost("new-game")]
        public int NewGame()
        {
            return service.NewGame();
        }
    }
}
