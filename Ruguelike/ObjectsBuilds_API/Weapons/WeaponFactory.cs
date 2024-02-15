using Ruguelike.CustomStructures;
using Ruguelike.GameObjects.DynamicObject;
using Ruguelike.Weapons;

// Тут симпл фактори просто для того, чтобы паттернов побольше

namespace Ruguelike.ObjectsBuilds_API.Weapons
{
    public class WeaponFactory : IWeaponFactory
    {
        public IWeapon CreateSword() => 
            new Weapon(
                "Sword",
                (attacker, target) =>
                {
                    target.TakeDamage(20);
                },
                playerPosition => gameObject =>
                {
                    var isNearby = Math.Abs(gameObject.Position.X - playerPosition.X) <= 1 && Math.Abs(gameObject.Position.Y - playerPosition.Y) <= 1;
                    var isNotOnPlayerPosition = gameObject.Position.X != playerPosition.X || gameObject.Position.Y != playerPosition.Y;
                    var isAlive = gameObject is IDynamicObject dynamicObject && dynamicObject.Alive;

                    return isNearby && isNotOnPlayerPosition && isAlive;
                }
            );
        

        public IWeapon CreatePistol() => 
            new Weapon(
                "Pistol",
                (attacker, target) =>
                {
                    var direction = new Position(
                        Math.Sign(target.Position.X - attacker.Position.X),
                        Math.Sign(target.Position.Y - attacker.Position.Y)
                    );
                    var bulletStartPosition = attacker.Position + direction;

                    attacker.Shoot(bulletStartPosition, "Bullet");
                },
                playerPosition => gameObject =>
                {
                    if (gameObject.Position == playerPosition || gameObject is not IDynamicObject dynamicObject)
                        return false;

                    if (!dynamicObject.Alive)
                        return false;

                    bool isOnSameLine = gameObject.Position.X == playerPosition.X || gameObject.Position.Y == playerPosition.Y;
                    bool isWithinDistance = Math.Abs(gameObject.Position.X - playerPosition.X) <= 5 && Math.Abs(gameObject.Position.Y - playerPosition.Y) <= 5;


                    return isOnSameLine && isWithinDistance;
                }
            );
    }
}
