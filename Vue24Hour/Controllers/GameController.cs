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
                return BadRequest("Naam mag niet leeg zijn.");
            }
            if (string.IsNullOrEmpty(createGameRequest?.Location))
            {
                return BadRequest("Locatie mag niet leeg zijn.");
            }
            if (createGameRequest?.MaximumParticipants == 0)
            {
                return BadRequest("Vul een minimaal aantal deelnemers toe.");
            }
            if (string.IsNullOrEmpty(createGameRequest?.StartDate.ToString("yy-MM-dd")))
            {
                return BadRequest("Startdatum mag niet leeg zijn.");
            }
            if (createGameRequest.Teams == null || createGameRequest?.Teams.Length < 2)
            {
                return BadRequest("Er moeten minimaal 2 teams mee doen.");
            }
            if (string.IsNullOrEmpty(createGameRequest?.Manager))
            {
                return BadRequest("Er moet een beheerder van de game zijn.");
            }

            _gameService.CreateGame(createGameRequest);
            return Ok();
        }

        [HttpPost("activate")]
        public async Task<IActionResult> ActivateGame([FromBody] ActivateGameModel game)
        {
            _gameService.ActivateGame(new Guid(game.id));
            var activatedGame = await _gameService.GetGame(new Guid(game.id));
            return Ok(activatedGame);
        }

        [HttpPost("createTestEvents")]
        public async Task<IActionResult> CreateTestEvents([FromBody] ActivateGameModel game)
        {
            _gameService.CreateTestEvents(new Guid(game.id));
            var gameWithEvents = await _gameService.GetGame(new Guid(game.id));
            return Ok(gameWithEvents);
        }

        [HttpPost("joinTeam")]
        public async Task<IActionResult> JoinTeam([FromBody] JoinGameModel joinGameModel)
        {
            _gameService.JoinTeam(new Guid(joinGameModel.id), joinGameModel.userName, new Guid(joinGameModel.gameId));
            var game = await _gameService.GetGame(new Guid(joinGameModel.gameId));
            return Ok(game);
        }
    }

    public class ActivateGameModel
    {
        public string id { get; set; }
    }

    public class JoinGameModel
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string gameId { get; set; }
    }
}
