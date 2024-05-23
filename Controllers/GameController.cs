// Controllers/GameController.cs
using Microsoft.AspNetCore.Mvc;
using MultiplayerGame.Data;

namespace MultiplayerGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameContext _context;

        public GameController(GameContext context)
        {
            _context = context;
        }

        // Define your endpoints here
        // Example: [HttpGet]
        // public IActionResult GetGames() { ... }
    }
}
