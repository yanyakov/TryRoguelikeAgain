using Ruguelike.CustomStructures;
using Ruguelike.GameObjects.Structures;
using Ruguelike.Weapons;

namespace Ruguelike.GameObjects.DynamicObject
{
    public class DynamicObject : IGameObject, IDynamicObject
    {
        public event Action<Position, string>? OnShoot;

        private readonly IWeapon weapon;

        private BaseStats stats;
        public int HP { get; private set; }
        public Guid Id { get => stats.Id; }
        public string Title { get => stats.Title; }
        public char Sprite { get => stats.Sprite; private set => stats.Sprite = value; }
        public Position Position { get => stats.Position; private set => stats.Position = value; }
        public bool Passable { get => stats.Passable; private set => stats.Passable = value; }
        public bool Alive { get => stats.Alive; private set => stats.Alive = value; }

        public DynamicObject(char sprite, string title, Position position, bool passable, int hp, IWeapon weapon)
        {
            stats = new BaseStats(sprite, title, position, passable);
            HP = hp;

            this.weapon = weapon ?? throw new ArgumentNullException(nameof(weapon), "Оружие не может быть null");

            this.weapon.OnShoot += (position, bulletPrototypeName) => { OnShoot?.Invoke(position, bulletPrototypeName); };
        }

        public void Attack(IDynamicObject target)
        {
            if (!Alive) 
                return;

            weapon.Attack(this, target);
        }
        public void Shoot(Position position, string bulletPrototypeName)
        {
            if (!Alive) 
                return;

            weapon.Shoot(position, bulletPrototypeName);
        }
        public void Move(Direction direction, Func<Position, bool> canMove)
        {
            if (!Alive) 
                return;

            Position newPosition = Position.NewPosition(direction);

            if (canMove(newPosition)) 
                Position = newPosition; 
        }
        public void TakeDamage(int damage)
        {
            if (!Alive) 
                return;

            HP -= damage;

            if (HP < 1)
            {
                HP = 0;
                Alive = false;
                Sprite = '†';
            }
        }

        public Func<IGameObject, bool> GetTargetPredicate() => weapon.GetTargetPredicate(Position);

        public IGameObject CloneWithNewPosition(Position newPosition) =>  new DynamicObject(stats.Sprite, stats.Title, newPosition, stats.Passable, HP, weapon);

        ~DynamicObject() =>  weapon.OnShoot -= (position, bulletPrototypeName) => { OnShoot?.Invoke(position, bulletPrototypeName); };
    }
}
