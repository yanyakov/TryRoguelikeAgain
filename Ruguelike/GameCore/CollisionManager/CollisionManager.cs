using Ruguelike.CustomStructures;
using Ruguelike.GameSceneRepository;

namespace Ruguelike.GameCore.CollisionManager
{
    public class CollisionManager (IGameSceneRepository gameScene) : ICollisionManager
    {
        private readonly IGameSceneRepository gameScene = gameScene;

        public bool CanMove(Position position) => gameScene.GameObjects(obj => obj.Position.Equals(position)).All(obj => obj.Passable);

        public bool CheckCollision(Guid object1Id, Guid object2Id)
        {
            Position object1 = gameScene.FindById(object1Id)?.Position ?? throw new InvalidOperationException("На карте нет первого объекта");
            Position object2 = gameScene.FindById(object2Id)?.Position ?? throw new InvalidOperationException("На карте нет второго объекта");

            return object1 == object2;
        }
    }
}
