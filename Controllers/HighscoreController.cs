using Microsoft.AspNetCore.Mvc;
using MultiplayerGame.Data;
using MultiplayerGame.Models;
using System.Collections.Generic;
using System.Linq;

namespace MultiplayerGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HighscoreController : ControllerBase
    {
        private readonly GameContext _context;

        public HighscoreController(GameContext context)
        {
            _context = context;
        }

        [HttpGet("daily")]
        public ActionResult<IEnumerable<Highscore>> GetDailyHighscores()
        {
            // Implement logic to retrieve daily highscores
            var dailyHighscores = _context.Highscores.Where(h => h.Date == DateTime.Today).ToList();
            return dailyHighscores;
        }

        [HttpGet("historical")]
        public ActionResult<IEnumerable<Highscore>> GetHistoricalHighscores()
        {
            // Implement logic to retrieve historical highscores
            var historicalHighscores = _context.Highscores.OrderByDescending(h => h.Score).ToList();
            return historicalHighscores;
        }
    }
}
