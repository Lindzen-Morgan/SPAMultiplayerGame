namespace MultiplayerGame.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }  // Add this property
        public string GameState { get; set; }  // Add this property
        public DateTime LastUpdated { get; set; }  // Add this property
    }
}
