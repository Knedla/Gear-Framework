using System;

namespace GearFramework.Entity
{
    [Serializable]
    readonly public struct Cell : IEquatable<Cell>
    {
        public static readonly Cell Negative = new Cell(-1, -1);
        public static readonly Cell Zero = new Cell(0, 0);
        public static bool operator ==(Cell lhs, Cell rhs) => lhs.Equals(rhs);
        public static bool operator !=(Cell lhs, Cell rhs) => !(lhs == rhs);
        public override bool Equals(object obj) => obj is Cell other && Equals(other);
        public override int GetHashCode() => (X, Y).GetHashCode();
        public bool Equals(Cell other) => X == other.X && Y == other.Y;
        public static Cell operator +(Cell a, Cell b) => new Cell(a.X + b.X, a.Y + b.Y);
        public static Cell operator -(Cell a, Cell b) => new Cell(a.X - b.X, a.Y - b.Y);
        public int X { get; }
        public int Y { get; }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString() => $"{X} {Y}";
    }
}
