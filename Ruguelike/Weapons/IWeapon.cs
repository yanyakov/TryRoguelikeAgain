using Ruguelike.CustomStructures;
using Ruguelike.GameObjects;
using Ruguelike.GameObjects.DynamicObject;

namespace Ruguelike.Weapons
{
    public interface IWeapon
    {
        public event Action<Position, string>? OnShoot;
        void Attack(IDynamicObject attacker, IDynamicObject target);
        Func<IGameObject, bool> GetTargetPredicate(Position playerPosition);
        public void Shoot(Position position, string bulletPrototypeName);

    }
}
