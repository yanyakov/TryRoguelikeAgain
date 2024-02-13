using Ruguelike.CustomStructures;
using Ruguelike.GameCore.CollisionManager;
using Ruguelike.GameObjects.DynamicObject;
using Ruguelike.GameSceneRepository;

namespace Ruguelike.GameCore.AiController
{
    public class AiController(IGameSceneRepository gameScene, IGameConfig gameConfig, ICollisionManager collisionManager) : IAiController
    {
        private readonly IGameSceneRepository gameScene = gameScene;
        private readonly IGameConfig gameConfig = gameConfig;
        private readonly ICollisionManager collisionManager = collisionManager;

        public void AllActions()
        {
            var dynamicObjects = gameScene.GameObjects(obj => obj is IDynamicObject && obj.Id != gameConfig.PlayerId)
                                .Cast<IDynamicObject>();

            foreach (var dynamicObject in dynamicObjects)
            {
                if (TryAttackTarget(dynamicObject))
                {
                    continue;
                }

                TryMoveRandomly(dynamicObject);
            }
        }

        private bool TryAttackTarget(IDynamicObject attacker)
        {
            var target = gameScene.GameObjects(obj => obj.Id == gameConfig.PlayerId && attacker.GetTargetPredicate()(obj)).FirstOrDefault();

            if (target != null && target is IDynamicObject dynamicTarget)
            {
                attacker.Attack(dynamicTarget);
                return true;
            }

            return false;
        }

        private void TryMoveRandomly(IDynamicObject dynamicObject)
        {
            var directions = new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
            var currentPosition = dynamicObject.Position;

            var freeDirections = directions
                .Select(dir => new { Direction = dir, NewPosition = currentPosition.NewPosition(dir) })
                .Where(dir => collisionManager.CanMove(dir.NewPosition))
                .ToList();

            if (freeDirections.Count != 0)
            {
                var move = freeDirections[Random.Shared.Next(freeDirections.Count)];
                dynamicObject.Move(move.Direction, collisionManager.CanMove);
            }
        }
    }
}
