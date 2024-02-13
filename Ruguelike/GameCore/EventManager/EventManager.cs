using Ruguelike.CustomStructures;
using Ruguelike.GameObjects.DynamicObject;
using Ruguelike.GameSceneRepository;

namespace Ruguelike.GameCore.EventManager
{
    public class EventManager(IGameSceneRepository gameSceneRepository) : IEventManager
    {
        private readonly IGameSceneRepository gameSceneRepository = gameSceneRepository;
        private Action<Position, string>? onShootSubscription;
        private HashSet<IDynamicObject> subscribedObjects = [];


        public void SubscribeToShoot(Action<Position, string> subscriber)
        {
            onShootSubscription += subscriber;
        }

        public void UnsubscribeFromShoot(Action<Position, string> subscriber)
        {
            onShootSubscription -= subscriber;
        }

        public void DispatchShoot(Position position, string bulletPrototypeName)
        {
            onShootSubscription?.Invoke(position, bulletPrototypeName);
        }

        public void UpdateSenders()
        {
            var currentDynamicObjects = new HashSet<IDynamicObject>(gameSceneRepository.GameObjects(obj => obj is IDynamicObject).Cast<IDynamicObject>());

            foreach (var oldObject in subscribedObjects.Except(currentDynamicObjects))
            {
                if (oldObject.Weapon != null)
                {
                    oldObject.Weapon.OnShoot -= DispatchShoot;
                }
            }

            foreach (var newObject in currentDynamicObjects.Except(subscribedObjects))
            {
                if (newObject.Weapon != null)
                {
                    newObject.Weapon.OnShoot += DispatchShoot;
                }
            }

            subscribedObjects = currentDynamicObjects;
        }
    }
}
