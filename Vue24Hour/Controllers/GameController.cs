using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}
