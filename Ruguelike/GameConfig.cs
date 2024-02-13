namespace Ruguelike
{
    public class GameConfig(int mapWidth, int mapHeight) : IGameConfig
    {
        public int MapWidth { get; } = mapWidth;
        public int MapHeight { get; } = mapHeight;
        public Guid PlayerId { get; private set; }
        public Guid FinishId { get; private set; }
        public bool GameOver { get; set; } = false;

        public void SetPlayerId(Guid playerId)
        {
            PlayerId = playerId;
        }
        public void SetFinishId(Guid finishId)
        {
            FinishId = finishId;
        }

        public bool ChangeGameStatus()
        {
            return GameOver = !GameOver;
        }
    }

}
