using Ruguelike.CustomStructures;
using Ruguelike.GameCore.CollisionManager;
using Ruguelike.GameObjects.DynamicObject;
using Ruguelike.GameSceneRepository;

namespace Ruguelike.GameCore.GameController
{
    public class GameController(IGameConfig config, IGameSceneRepository gameScene, ICollisionManager collisionManager) : IGameController
    {
        private readonly IGameConfig config = config;
        private readonly IGameSceneRepository gameScene = gameScene;
        private readonly ICollisionManager collisionManager = collisionManager;

        public void ProcessInput(ConsoleKey key)
        {
            if (gameScene.FindById(config.PlayerId) is not IDynamicObject player) 
                return;

            Direction? direction = key switch
            {
                ConsoleKey.W => Direction.Up,
                ConsoleKey.S => Direction.Down,
                ConsoleKey.A => Direction.Left,
                ConsoleKey.D => Direction.Right,
                _ => null
            };

            if (direction.HasValue)
                player.Move(direction.Value, collisionManager.CanMove);

            if (key == ConsoleKey.Spacebar)
            {
                var target = gameScene.GameObjects(player.GetTargetPredicate()).OfType<IDynamicObject>().FirstOrDefault();

                if (target == null)
                    return;

                player.Attack(target);
            }
        }
    }
}
