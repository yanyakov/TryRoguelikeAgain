using Ruguelike.CustomStructures;
using Ruguelike.GameObjects.Structures;

namespace Ruguelike.GameObjects.StaticObject
{
    public class StaticObject(char sprite, string title, Position position, bool passable = false) : IGameObject
    {
        private BaseStats stats = new(sprite, title, position, passable);

        public Guid Id => stats.Id;
        public string Title => stats.Title;
        public char Sprite { get => stats.Sprite; set => stats.Sprite = value; }
        public Position Position { get => stats.Position; set => stats.Position = value; }
        public bool Passable { get => stats.Passable; set => stats.Passable = value; }
        public bool Alive { get => stats.Alive; set => stats.Alive = value; }

        public IGameObject CloneWithNewPosition(Position newPosition)
        {
            return new StaticObject(stats.Sprite, stats.Title, newPosition, stats.Passable);
        }
    }
}
