using Ruguelike.CustomStructures;
using Ruguelike.GameObjects;
using Ruguelike.GameObjects.AutonomyObject;
using Ruguelike.GameObjects.DynamicObject;
using Ruguelike.GameObjects.StaticObject;
using Ruguelike.ObjectsBuilds_API.Weapons;

namespace Ruguelike.API
{
    public class PrototypeFactory : IPrototypeFactory
    {
        private readonly IWeaponFactory weaponFactory;
        private readonly Dictionary<string, IGameObject> prototypes = [];

        public PrototypeFactory(IWeaponFactory weaponFactory)
        {
            this.weaponFactory = weaponFactory;
            InitPrototypes();
        }

        private void InitPrototypes()
        {
            prototypes["Wall"] = new StaticObject('#', "Wall",new Position(0, 0), false);
            prototypes["Finish"] = new StaticObject('F', "Finish", new Position(0, 0), true);

            prototypes["Player"] = new DynamicObject('P', "Player", new Position(0, 0), true, 100, weaponFactory.CreatePistol());
            prototypes["Zombie"] = new DynamicObject('Z', "Zombie", new Position(0, 0), true, 100, weaponFactory.CreateSword());
            prototypes["Archer"] = new DynamicObject('A', "Archer", new Position(0, 0), true, 50, weaponFactory.CreatePistol());

            prototypes["Bullet"] = new AutoObject('*', "Bullet", new Position(0, 0), true)
                .AddStageAction((gameScene, self, condition) => {
                    if (!self.CustomProperties.TryGetValue("Direction", out var directionObj) || (directionObj is not Position initialDirection))
                    {
                        var shooter = gameScene.GameObjects(obj => obj is IDynamicObject &&
                            (Math.Abs(obj.Position.X - self.Position.X) <= 1 && Math.Abs(obj.Position.Y - self.Position.Y) <= 1))
                            .LastOrDefault();
                        if (shooter != null)
                        {
                            var direction = self.Position - shooter.Position;
                            initialDirection = new Position(Math.Clamp(direction.X, -1, 1), Math.Clamp(direction.Y, -1, 1));
                            self.CustomProperties["Direction"] = initialDirection;
                        }
                        else
                        {
                            gameScene.Remove(self.Id);
                            return -1;
                        }
                    }
                    else
                    {
                        initialDirection = (Position)directionObj;
                    }

                    var nextPosition = self.Position + initialDirection;
                    var nextObject = gameScene.GameObjects(obj => obj.Position == nextPosition).FirstOrDefault();
                    if (nextObject != null)
                    {
                        if (nextObject is IDynamicObject target)
                        {
                            target.TakeDamage(200);
                        }
                        gameScene.Remove(self.Id);
                        return -1;
                    }
                    self.Position = nextPosition; 
                    return 0;
                }, obj => true);
        }

        public IGameObject Create(string prototypeKey, Position position)
        {
            if (!prototypes.TryGetValue(prototypeKey, out IGameObject? value))
            {
                throw new ArgumentException($"Нет такого прототипа '{prototypeKey}'");
            }
            return value.CloneWithNewPosition(position);
        }
    }
}
