namespace MultiplayerGame.Models
{
    public class Highscore
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        // Additional properties if needed
    }
}
