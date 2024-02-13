using Ruguelike.API;
using Ruguelike.CustomStructures;
using Ruguelike.EntityGenerators;
using Ruguelike.GameObjects;
using Ruguelike.GameSceneRepository;
using Ruguelike.MazeGenerator;

namespace Ruguelike.GameCore.GameInitializer
{
    public class GameInitializer(IGameConfig config, IGameSceneRepository gameScene, IPrototypeFactory factory) : IGameInitializer
    {
        private readonly IGameConfig config = config;
        private readonly IGameSceneRepository gameScene = gameScene;
        private readonly IPrototypeFactory factory = factory;

        public void Init()
        {
            gameScene.Clear();

            IGameObject player = factory.Create("Player", new Position(1, 1));
            IGameObject finish = factory.Create("Finish", new Position(config.MapWidth - 2, config.MapHeight - 2));

            gameScene.Add(player);
            config.SetPlayerId(player.Id);

            gameScene.Add(finish);
            config.SetFinishId(finish.Id);

            IMazeGenerator mazeGenerator = new MazeGenerator.MazeGenerator(config, gameScene, factory);
            mazeGenerator.Generate();

            IEntityGenerator entityGenerator = new EntityGenerator(config, gameScene, factory);
            entityGenerator.Generate(5, 5);
        }
    }
}
