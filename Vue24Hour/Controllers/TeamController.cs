using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vue24Hour.Services;

namespace Vue24Hour.Controllers
{
    [Route("api/[controller]")]
    public class TeamController : Controller
    {
        private readonly IGameService _gameService;

        public TeamController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // Handles GET /api/teams
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var games = await _gameService.GetTeams();
            return Ok(games);
        }
    }
}
