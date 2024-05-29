namespace MultiplayerGame.Models
{
    public class StartGameRequest
    {
        public string UserId { get; set; }
    }

    public class MoveRequest
    {
        public int GameId { get; set; }
        public string NewState { get; set; }
        public string Player { get; internal set; }
        public int Position { get; internal set; }
    }
}
