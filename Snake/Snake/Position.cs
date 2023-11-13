using System;
using System.Collections.Generic;
using Snake.GameObjects;

namespace Snake;

public class Position
{
    public int Row { get; }
    public int Col { get; }

    public Position(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public Position Translate(Direction dir)
    {
        return new Position(Row + dir.RowOffset, Col + dir.ColOffset);
    }

    private sealed class RowColEqualityComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position x, Position y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Row == y.Row && x.Col == y.Col;
        }

        public int GetHashCode(Position obj)
        {
            return HashCode.Combine(obj.Row, obj.Col);
        }
    }

    public static IEqualityComparer<Position> RowColComparer { get; } = new RowColEqualityComparer();
}