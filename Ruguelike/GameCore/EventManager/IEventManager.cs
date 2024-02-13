using Ruguelike.CustomStructures;

namespace Ruguelike.GameCore.EventManager
{
    public interface IEventManager
    {
        public void SubscribeToShoot(Action<Position, string> subscriber);
        public void UnsubscribeFromShoot(Action<Position, string> subscriber);
        void UpdateSenders();
    }
}
