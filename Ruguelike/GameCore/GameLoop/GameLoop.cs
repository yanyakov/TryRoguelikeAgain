using Ruguelike.GameCore.AiController;
using Ruguelike.GameCore.AutonomyObjectsManager;
using Ruguelike.GameCore.CollisionManager;
using Ruguelike.GameCore.EventManager;
using Ruguelike.GameCore.GameController;
using Ruguelike.GameCore.GameInitializer;
using Ruguelike.GameCore.GameRenderer;
using Ruguelike.GameSceneRepository;

namespace Ruguelike.GameCore.GameLoop
{
    public class GameLoop(  IGameConfig config, 
                            ICollisionManager collisionManager, 
                            IGameRender renderer, 
                            IGameController controller, 
                            IGameInitializer initializer, 
                            IAutonomyObjectsManager autonomyManager, 
                            IEventManager eventManager,
                            IGameSceneRepository gameScene,
                            IAiController aiController
        ) : IGameLoop
    {
        private readonly IGameConfig config = config;
        private readonly ICollisionManager collisionManager = collisionManager;
        private readonly IGameRender renderer = renderer;
        private readonly IGameController controller = controller;
        private readonly IGameInitializer initializer = initializer;
        private readonly IAutonomyObjectsManager autonomyObjectsManager = autonomyManager;
        private readonly IEventManager eventManager = eventManager;
        private readonly IGameSceneRepository sceneRepository = gameScene;
        private readonly IAiController aiController = aiController;

        public void Run()
        {
            eventManager.UpdateSenders();
            while (!config.GameOver)
            {
                CheckGameOver();
                CheckFinished();

                renderer.Render();
                autonomyObjectsManager.UpdateAll();

                var key = Console.ReadKey(true).Key;
                controller.ProcessInput(key);
                aiController.AllActions();
            }
            OnGameOver();
        }
        
        private void CheckFinished() 
        {
            if (collisionManager.CheckCollision(config.PlayerId, config.FinishId)){
                initializer.Init();
                eventManager.UpdateSenders();
            }
        }
        private void CheckGameOver()
        {
            var player = sceneRepository.FindById(config.PlayerId);

            if (player == null) { 
                return; 
            }
            if (!player.Alive) {
                config.ChangeGameStatus();
            }
        }
        private static void OnGameOver()
        {
            Console.WriteLine("Игра завершена! Поставьте 5 звезд в убер, пожалуйста!");
        }
    }
}
