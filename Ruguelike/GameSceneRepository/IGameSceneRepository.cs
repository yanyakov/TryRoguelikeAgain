using Ruguelike.GameObjects;

namespace Ruguelike.GameSceneRepository
{
    public interface IGameSceneRepository
    {
        IEnumerable<IGameObject> GameObjects(Func<IGameObject, bool> predicate);
        void Add(IGameObject gameObject);
        bool Remove(Guid id);
        IGameObject? FindById(Guid id);
        void Clear();
    }
}
