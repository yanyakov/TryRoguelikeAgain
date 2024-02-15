namespace Ruguelike
{
    public interface IGameConfig
    {
        int MapWidth { get; }
        int MapHeight { get; }
        public int ZombieNum { get; }
        public int ArcherNum { get; }
        Guid PlayerId { get; }
        Guid FinishId { get; }
        bool GameOver { get; }

        void SetPlayerId(Guid playerId);
        void SetFinishId(Guid playerId);
        bool ChangeGameStatus();
    }

}
