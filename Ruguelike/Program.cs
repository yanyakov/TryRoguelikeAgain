using Ruguelike;
using Ruguelike.API;
using Ruguelike.EntityGenerators;
using Ruguelike.GameCore.AiController;
using Ruguelike.GameCore.AutonomyObjectsManager;
using Ruguelike.GameCore.CollisionManager;
using Ruguelike.GameCore.EventManager;
using Ruguelike.GameCore.GameController;
using Ruguelike.GameCore.GameInitializer;
using Ruguelike.GameCore.GameLoop;
using Ruguelike.GameCore.GameRenderer;
using Ruguelike.GameSceneRepository;
using Ruguelike.MazeGenerator;
using Ruguelike.ObjectsBuilds_API.Weapons;

class Program
{
    static void Main()
    {
        IGameConfig config = new GameConfig(50, 20, 5, 5);

        IGameSceneRepository gameScene = new GameSceneRepository();
        IWeaponFactory weaponFactory = new WeaponFactory();
        IPrototypeFactory factory = new PrototypeFactory(weaponFactory);

        IMazeGenerator mazeGenerator = new MazeGenerator(config, gameScene, factory);
        IEntityGenerator entityGenerator = new EntityGenerator(config, gameScene, factory);

        IEventManager eventManager = new EventManager(gameScene);
        ICollisionManager collisionManager = new CollisionManager(gameScene);

        IGameController gameController = new GameController(config, gameScene, collisionManager);
        IAiController aiController = new AiController(gameScene, config, collisionManager);
        IAutonomyObjectsManager autonomyManager = new AutonomyObjectsManager(gameScene, factory, eventManager, collisionManager);

        IGameRender gameRender = new GameRender(gameScene, config);
        IGameInitializer initializer = new GameInitializer(config, gameScene, factory, mazeGenerator, entityGenerator, eventManager);
        IGameLoop gameLoop = new GameLoop(config, collisionManager, gameRender, gameController, initializer, autonomyManager, gameScene, aiController);

        gameLoop.Run();
    }
}