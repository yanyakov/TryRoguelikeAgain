using Ruguelike.CustomStructures;

namespace Ruguelike.GameObjects.Structures
{
    public struct BaseStats(char sprite, string title, Position position, bool passable = false)
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Title { get; } = title;
        public char Sprite { get; set; } = sprite;
        public Position Position { get; set; } = position;
        public bool Passable { get; set; } = passable;
        public bool Alive { get; set; } = true;
    }
}
