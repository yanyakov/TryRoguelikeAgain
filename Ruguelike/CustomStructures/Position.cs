
namespace Ruguelike.CustomStructures
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public struct Position(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public static Position operator +(Position a, Position b) => new(a.X + b.X, a.Y + b.Y);
      
        public static Position operator -(Position a, Position b) => new(a.X - b.X, a.Y - b.Y);
        
        public static Position operator /(Position a, int divisor) =>  new(a.X / divisor, a.Y / divisor);

        public static bool operator ==(Position a, Position b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Position a, Position b) => !(a == b);
        
        public override readonly bool Equals(object? obj)
        {
            if (obj is null) 
                return false;

            if (obj is Position p)
                return X == p.X && Y == p.Y;
     
            return false;
        }

        public override readonly int GetHashCode() =>  HashCode.Combine(X, Y);

        public readonly Position NewPosition(Direction direction)
        =>  direction switch
            {
                Direction.Up => new Position(X, Y - 1),
                Direction.Down => new Position(X, Y + 1),
                Direction.Left => new Position(X - 1, Y),
                Direction.Right => new Position(X + 1, Y),
                _ => this,
            };
    }
}
