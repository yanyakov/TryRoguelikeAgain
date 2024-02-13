using Ruguelike.CustomStructures;

namespace Ruguelike.GameCore.CollisionManager
{
    public interface ICollisionManager
    {
        bool CanMove(Position position);
        bool CheckCollision(Guid object1Id, Guid object2Id);
    }
}
