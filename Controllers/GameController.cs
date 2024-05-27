using Microsoft.AspNetCore.Mvc;
using MultiplayerGame.Data;
using MultiplayerGame.Models;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly GameContext _context;

    public GameController(GameContext context)
    {
        _context = context;
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartGame([FromBody] StartGameRequest request)
    {
        var game = new Game
        {
            UserId = request.UserId,
            GameState = "initial",
            LastUpdated = DateTime.UtcNow
        };
        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        return Ok(game);
    }

    [HttpPost("move")]
    public async Task<IActionResult> MakeMove([FromBody] MoveRequest request)
    {
        var game = await _context.Games.FindAsync(request.GameId);
        if (game == null)
            return NotFound();

        // Update game state
        game.GameState = request.NewState;
        game.LastUpdated = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(game);
    }
}
