namespace Ruguelike
{
    public class GameConfig(int mapWidth, int mapHeight, int zombieNum, int archerNum) : IGameConfig
    {
        public int MapWidth { get; } = mapWidth;
        public int MapHeight { get; } = mapHeight;
        public int ZombieNum { get; } = zombieNum;
        public int ArcherNum { get; } = archerNum;
        public Guid PlayerId { get; private set; }
        public Guid FinishId { get; private set; }
        public bool GameOver { get; private set; } = false;

        public void SetPlayerId(Guid playerId) => PlayerId = playerId;
        public void SetFinishId(Guid finishId) => FinishId = finishId;
        public bool ChangeGameStatus() => GameOver = !GameOver;
    }
}
