using Ruguelike.CustomStructures;
using Ruguelike.Weapons;

namespace Ruguelike.GameObjects.DynamicObject
{
    public interface IDynamicObject : IGameObject
    {
        public int HP { get; }
        void Move(Direction direction, Func<Position, bool> canMove);
        public void Attack(IDynamicObject target);
        public IWeapon Weapon { get; }
        Func<IGameObject, bool> GetTargetPredicate();
        public void TakeDamage(int damage);
    }
}
