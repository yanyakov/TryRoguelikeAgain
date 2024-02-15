using Ruguelike.CustomStructures;
using Ruguelike.GameObjects;
using Ruguelike.GameObjects.DynamicObject;

namespace Ruguelike.Weapons
{
    public class Weapon(string name, Action<IDynamicObject, IDynamicObject> attackAction, Func<Position, Func<IGameObject, bool>> getTargetPredicate) : IWeapon
    {
        public event Action<Position, string>? OnShoot;
        private readonly Action<IDynamicObject, IDynamicObject> attackAction = attackAction;
        private readonly Func<Position, Func<IGameObject, bool>> getTargetPredicate = getTargetPredicate;
        public string Name { get; } = name;

        public void Attack(IDynamicObject attacker, IDynamicObject target) => attackAction(attacker, target);
        
        public Func<IGameObject, bool> GetTargetPredicate(Position playerPosition) => getTargetPredicate(playerPosition);

        public void Shoot(Position position, string bulletPrototypeName) => OnShoot?.Invoke(position, bulletPrototypeName);
    }
}
