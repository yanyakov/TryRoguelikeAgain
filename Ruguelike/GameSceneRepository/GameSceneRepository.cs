using Ruguelike.GameObjects;

namespace Ruguelike.GameSceneRepository
{
    public class GameSceneRepository : IGameSceneRepository
    {
        private readonly List<IGameObject> gameObjects = [];

        public void Add(IGameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public bool Remove(Guid id)
        {
            var obj = FindById(id);
            return obj != null && gameObjects.Remove(obj);
        }

        public IGameObject? FindById(Guid id)
        {
            return gameObjects.FirstOrDefault(obj => obj.Id == id);
        }

        public IEnumerable<IGameObject> GameObjects(Func<IGameObject, bool> predicate)
        {
            return gameObjects.Where(predicate).Reverse().ToList();
        }
        public void Clear()
        {
            gameObjects.Clear();
        }
    }
}
