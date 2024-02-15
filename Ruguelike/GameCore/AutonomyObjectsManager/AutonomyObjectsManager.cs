using Ruguelike.API;
using Ruguelike.CustomStructures;
using Ruguelike.GameCore.CollisionManager;
using Ruguelike.GameCore.EventManager;
using Ruguelike.GameObjects.AutonomyObject;
using Ruguelike.GameSceneRepository;

namespace Ruguelike.GameCore.AutonomyObjectsManager
{
    public class AutonomyObjectsManager : IAutonomyObjectsManager
    {
        private readonly IGameSceneRepository gameSceneRepository;
        private readonly IPrototypeFactory prototypeFactory;
        private readonly IEventManager eventManager;
        private readonly ICollisionManager collisionManager;

        public AutonomyObjectsManager(IGameSceneRepository gameSceneRepository, IPrototypeFactory prototypeFactory, IEventManager eventManager, ICollisionManager collisionManager)
        {
            this.gameSceneRepository = gameSceneRepository;
            this.prototypeFactory = prototypeFactory;
            this.eventManager = eventManager;
            this.eventManager.SubscribeToShoot(CreateAndAddBullet);
            this.collisionManager = collisionManager;
        }

        private void CreateAndAddBullet(Position position, string bulletName)
        {
            if (collisionManager.CanMove(position))
            {
                var bullet = prototypeFactory.Create(bulletName, position);
                gameSceneRepository.Add(bullet);
            }
        }

        public void UpdateAll()
        {
            var autonomousObjects = gameSceneRepository.GameObjects(obj => obj is IAutoObject).Cast<IAutoObject>().ToList();
            
            foreach (var autoObject in autonomousObjects)
               autoObject.Update(gameSceneRepository);
            
        }

        ~AutonomyObjectsManager() => eventManager.UnsubscribeFromShoot(CreateAndAddBullet);
    }
}
