using Ruguelike.CustomStructures;
using Ruguelike.GameObjects.Structures;

namespace Ruguelike.GameObjects.StaticObject
{
    public class StaticObject(char sprite, string title, Position position, bool passable = false) : IGameObject, IStaticObject
    {
        private BaseStats stats = new(sprite, title, position, passable);

        public Guid Id { get => stats.Id; }
        public string Title { get => stats.Title; }
        public char Sprite { get => stats.Sprite;  private set => stats.Sprite = value; }
        public Position Position { get => stats.Position; private set => stats.Position = value; }
        public bool Passable { get => stats.Passable; private set => stats.Passable = value; }
        public bool Alive { get => stats.Alive; private set => stats.Alive = value; }

        public IGameObject CloneWithNewPosition(Position newPosition) => new StaticObject(stats.Sprite, stats.Title, newPosition, stats.Passable);
    }
}
