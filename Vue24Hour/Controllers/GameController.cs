using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vue24Hour.Domain.Model.Requests;
using Vue24Hour.Services;

namespace Vue24Hour.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // Handles GET /api/game
        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _gameService.GetItems();
            return Ok(games);
        }

        // POST /api/game/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(Guid id)
        {
            var game = await _gameService.GetGame(id);
            return Ok(game);
        }


        // POST /api/game
        [HttpPost]
        public async Task<IActionResult> StartNewGame([FromBody] CreateGameRequest createGameRequest)
        {
            if (string.IsNullOrEmpty(createGameRequest?.Name))
            {
                return BadRequest("Name mag niet leeg zijn.");
            }

            _gameService.CreateGame(createGameRequest);
            return Ok();
        }

        [HttpPost("activate")]
        public async Task<IActionResult> ActivateGame([FromBody] ActivateGameModel game)
        {
            _gameService.ActivateGame(new Guid(game.id));
            return Ok();
        }

        [HttpPost("createTestEvents")]
        public async Task<IActionResult> CreateTestEvents([FromBody] ActivateGameModel game)
        {
            _gameService.CreateTestEvents(new Guid(game.id));
            return Ok();
        }
    }

    public class ActivateGameModel
    {
        public string id { get; set; }
    }
}
