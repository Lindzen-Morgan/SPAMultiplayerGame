using Microsoft.AspNetCore.Mvc;
using MultiplayerGame.Data;
using MultiplayerGame.Models;
using MultiplayerGame.Requests;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost("start")]
        public async Task<IActionResult> StartGame([FromBody] Models.StartGameRequest request)
        {
            var game = new Game
            {
                UserId = request.UserId,
                GameState = "initial",
                LastUpdated = DateTime.UtcNow,
                Board = "---------", // Empty board
                CurrentPlayer = "X", // X always starts
                IsFinished = false,
                Winner = null
            };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return Ok(game);
        }

        [HttpPost("move")]
        public async Task<IActionResult> MakeMove([FromBody] Models.MoveRequest request)
        {
            var game = await _context.Games.FindAsync(request.GameId);
            if (game == null || game.IsFinished)
                return BadRequest("Invalid game or game is already finished.");

            if (game.CurrentPlayer != request.Player)
                return BadRequest("It's not your turn.");

            if (game.Board[request.Position] != '-')
                return BadRequest("Invalid move.");

            // Update the board
            char[] boardArray = game.Board.ToCharArray();
            boardArray[request.Position] = request.Player[0];
            game.Board = new string(boardArray);

            // Check for winner
            if (CheckWinner(game.Board, request.Player[0]))
            {
                game.IsFinished = true;
                game.Winner = request.Player;
            }
            else if (!game.Board.Contains('-'))
            {
                game.IsFinished = true;
                game.Winner = "Draw";
            }
            else
            {
                game.CurrentPlayer = game.CurrentPlayer == "X" ? "O" : "X";
            }

            game.LastUpdated = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(game);
        }

        private bool CheckWinner(string board, char player)
        {
            // Check rows, columns, and diagonals
            int[][] winningPositions = new int[][]
            {
                new int[] { 0, 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8 }, // Rows
                new int[] { 0, 3, 6 }, new int[] { 1, 4, 7 }, new int[] { 2, 5, 8 }, // Columns
                new int[] { 0, 4, 8 }, new int[] { 2, 4, 6 }                         // Diagonals
            };

            return winningPositions.Any(position =>
                board[position[0]] == player &&
                board[position[1]] == player &&
                board[position[2]] == player);
        }
    }
}
