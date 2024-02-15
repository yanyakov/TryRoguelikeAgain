using Ruguelike.CustomStructures;
using Ruguelike.GameObjects.Structures;
using Ruguelike.GameSceneRepository;

namespace Ruguelike.GameObjects.AutonomyObject
{
    public class AutoObject(char sprite, string title, Position position, bool passable = false) : IGameObject, IAutoObject
    {
        private BaseStats stats = new(sprite, title, position, passable);

        public Guid Id { get => stats.Id; }
        public string Title { get => stats.Title; }
        public char Sprite { get => stats.Sprite; private set => stats.Sprite = value; }
        public Position Position { get => stats.Position; set => stats.Position = value; }
        public bool Passable { get => stats.Passable; private set => stats.Passable = value; }
        public bool Alive { get => stats.Alive; private set => stats.Alive = value; }

        public Dictionary<string, object> CustomProperties { get; } = [];
        private readonly List<(Func<IGameSceneRepository, IAutoObject, Func<IGameObject, bool>, int> Action, Func<IGameObject, bool> Condition)> stageActions = [];
        private int currentStage = 0;

        public IAutoObject AddStageAction(Func<IGameSceneRepository, IAutoObject, Func<IGameObject, bool>, int> action, Func<IGameObject, bool> condition)
        {
            stageActions.Add((action, condition));
            return this;
        }

        public IAutoObject AddCustomProperty(string key, object value)
        {
            CustomProperties[key] = value;
            return this;
        }

        public void Update(IGameSceneRepository gameScene)
        {
            if (currentStage < stageActions.Count)
            {
                var (action, condition) = stageActions[currentStage];
                currentStage = action(gameScene, this, condition);
            }
        }

        public IGameObject CloneWithNewPosition(Position newPosition)
        {
            var clonedObject = new AutoObject(stats.Sprite, stats.Title, newPosition, stats.Passable);
            
            foreach (var action in stageActions)
                clonedObject.AddStageAction(action.Action, action.Condition);
            
            foreach (var property in CustomProperties)
                clonedObject.AddCustomProperty(property.Key, property.Value);
            
            return clonedObject;
        }

    }
}
