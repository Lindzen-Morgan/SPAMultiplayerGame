namespace MultiplayerGame.Requests
{
    public class StartGameRequest
    {
        public string UserId { get; set; }
    }

    public class MoveRequest
    {
        public int GameId { get; set; }
        public int Position { get; set; }
        public string Player { get; set; }
    }
}
