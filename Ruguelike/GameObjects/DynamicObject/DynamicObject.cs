using Ruguelike.CustomStructures;
using Ruguelike.GameObjects.Structures;
using Ruguelike.Weapons;

namespace Ruguelike.GameObjects.DynamicObject
{
    public class DynamicObject(char sprite, string title, Position position, bool passable, int hp, IWeapon weapon) : IGameObject, IDynamicObject
    {
        private BaseStats stats = new(sprite, title, position, passable);
        public int HP { get; private set; } = hp;
#pragma warning disable CS9124
        public IWeapon Weapon { get; } = weapon;
#pragma warning restore CS9124
        public Guid Id => stats.Id;
        public string Title => stats.Title;
        public char Sprite { get => stats.Sprite; set => stats.Sprite = value; }
        public Position Position { get => stats.Position; set => stats.Position = value; }
        public bool Passable { get => stats.Passable; set => stats.Passable = value; }
        public bool Alive { get => stats.Alive; set => stats.Alive = value; }


        public void Attack(IDynamicObject target)
        {
            if (!Alive) { return; }
            Weapon.Attack(this, target);
        }
        public void Move(Direction direction, Func<Position, bool> canMove)
        {
            if (!Alive) { return; }

            Position newPosition = Position.NewPosition(direction);

            if (canMove(newPosition)) { Position = newPosition; }
        }
        public void TakeDamage(int damage)
        {
            if (!Alive) { return; }
            HP -= damage;
            if (HP < 1)
            {
                HP = 0;
                Alive = false;
                Sprite = '†';
            }
        }

        public Func<IGameObject, bool> GetTargetPredicate()
        {
            return weapon.GetTargetPredicate(Position);
        }

        public IGameObject CloneWithNewPosition(Position newPosition)
        {
            return new DynamicObject(stats.Sprite, stats.Title, newPosition, stats.Passable, HP, Weapon);
        }
    }
}
