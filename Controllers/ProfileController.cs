using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiplayerGame.Data;
using MultiplayerGame.Models;
using System.Threading.Tasks;

namespace MultiplayerGame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly GameContext _context;

        public ProfileController(GameContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetProfile(string userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateProfile(string userId, User updatedUser)
        {
            if (userId != updatedUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.Id == userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
