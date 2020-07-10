namespace Spark.Core
{
    public readonly struct Position
    {
        public short X { get; }
        public short Y { get; }

        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"{X}/{Y}";
    }
}