public class Game
{
    public int Id { get; set; }
    public string GameState { get; set; }
    public DateTime LastUpdated { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    // Other game-specific fields
}