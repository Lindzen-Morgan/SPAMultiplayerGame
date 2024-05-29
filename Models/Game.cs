namespace MultiplayerGame.Models
{
    public class Game
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required string GameState { get; set; }
        public DateTime LastUpdated { get; set; }
        public required string Board { get; set; }
        public required string CurrentPlayer { get; set; }
        public bool IsFinished { get; set; }
        public required string Winner { get; set; }
    }
}
