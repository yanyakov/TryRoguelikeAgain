using Ruguelike.API;
using Ruguelike.CustomStructures;
using Ruguelike.EntityGenerators;
using Ruguelike.GameCore.EventManager;
using Ruguelike.GameObjects;
using Ruguelike.GameSceneRepository;
using Ruguelike.MazeGenerator;

namespace Ruguelike.GameCore.GameInitializer
{
    public class GameInitializer(IGameConfig config, 
                                 IGameSceneRepository gameScene, 
                                 IPrototypeFactory factory, 
                                 IMazeGenerator mazeGenerator, 
                                 IEntityGenerator entityGenerator,
                                 IEventManager eventManager
        ) : IGameInitializer
    {
        private readonly IGameConfig config = config;
        private readonly IGameSceneRepository gameScene = gameScene;
        private readonly IPrototypeFactory factory = factory;
        private readonly IMazeGenerator mazeGenerator = mazeGenerator;
        private readonly IEntityGenerator entityGenerator = entityGenerator;
        private readonly IEventManager eventManager = eventManager;

        public void Init()
        {
            gameScene.Clear();

            IGameObject player = factory.Create("Player", new Position(1, 1));
            IGameObject finish = factory.Create("Finish", new Position(config.MapWidth - 2, config.MapHeight - 2));

            gameScene.Add(player);
            config.SetPlayerId(player.Id);

            gameScene.Add(finish);
            config.SetFinishId(finish.Id);

            mazeGenerator.Generate();
            entityGenerator.Generate(config.ZombieNum, config.ZombieNum);

            eventManager.UpdateSenders();
        }
    }
}
