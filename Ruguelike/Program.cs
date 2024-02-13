using Ruguelike;
using Ruguelike.API;
using Ruguelike.GameCore;
using Ruguelike.GameCore.AiController;
using Ruguelike.GameCore.AutonomyObjectsManager;
using Ruguelike.GameCore.CollisionManager;
using Ruguelike.GameCore.EventManager;
using Ruguelike.GameCore.GameController;
using Ruguelike.GameCore.GameInitializer;
using Ruguelike.GameCore.GameLoop;
using Ruguelike.GameCore.GameRenderer;
using Ruguelike.GameSceneRepository;
using Ruguelike.ObjectsBuilds_API.Weapons;

class Program
{
    static void Main()
    {
        IGameConfig config = new GameConfig(50, 20);

        IGameSceneRepository gameScene = new GameSceneRepository();
        IWeaponFactory weaponFactory = new WeaponFactory();
        IPrototypeFactory factory = new PrototypeFactory(weaponFactory);

        IGameInitializer initializer = new GameInitializer(config, gameScene, factory);
        initializer.Init();

        IEventManager eventManager = new EventManager(gameScene);


        IGameRender gameRender = new GameRender(gameScene, config);
        ICollisionManager collisionManager = new CollisionManager(gameScene);
        IGameController gameController = new GameController(config, gameScene, collisionManager);
        IAiController aiController = new AiController(gameScene, config, collisionManager);
        IAutonomyObjectsManager autonomyManager = new AutonomyObjectsManager(gameScene, factory, eventManager, collisionManager);
        IGameLoop gameLoop = new GameLoop(config, collisionManager, gameRender, gameController, initializer, autonomyManager, eventManager, gameScene, aiController);

        gameLoop.Run();
    }
}