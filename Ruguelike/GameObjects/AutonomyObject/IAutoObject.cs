using Ruguelike.CustomStructures;
using Ruguelike.GameSceneRepository;

namespace Ruguelike.GameObjects.AutonomyObject
{
    public interface IAutoObject : IGameObject
    {
        public Dictionary<string, object> CustomProperties { get; }
        public IAutoObject AddStageAction(Func<IGameSceneRepository, IAutoObject, Func<IGameObject, bool>, int> action, Func<IGameObject, bool> condition);
        public IAutoObject AddCustomProperty(string key, object value);
        public void Update(IGameSceneRepository gameScene);
        new Position Position { get; set; }
    }
}
