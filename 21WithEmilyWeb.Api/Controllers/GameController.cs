using _21WithEmilyWeb.Api.Models;
using _21WithEmilyWeb.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace _21WithEmilyWeb.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService service;

        public GameController(GameService service) : base()
        {
            this.service = service;
        }

        [HttpPost("new-game")]
        public async Task<GameResponse> NewGame()
        {
            return await service.NewGame();
        }

        [HttpPost("count")]
        public async Task<ActionResult<GameResponse>> Count(
        [FromBody] CountRequest request)
        {
            var response = await service.Count(request.GameId, request.Count);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
