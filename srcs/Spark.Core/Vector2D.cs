using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Spark.Core
{
public readonly struct Vector2D : IEquatable<Vector2D>
{
    private static readonly double Sqrt = Math.Sqrt(2);
    private static readonly Random Random = new Random();
    
    public static Vector2D Zero { get; } = new Vector2D();
    
    public int X { get; }
    public int Y { get; }

    public Vector2D(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2D GetDistanceTo(Vector2D vector2D) => new Vector2D(Math.Abs(vector2D.X - X), Math.Abs(vector2D.Y - Y));

    public double GetDistance(Vector2D destination)
    {
        int x = Math.Abs(X - destination.X);
        int y = Math.Abs(Y - destination.Y);

        int min = Math.Min(x, y);
        int max = Math.Max(x, y);

        return (int)(min * Sqrt + max - min);
    }
    
    public bool IsInRange(Vector2D position, int range)
    {
        int dx = Math.Abs(X - position.X);
        int dy = Math.Abs(Y - position.Y);
        
        return dx <= range && dy <= range && dx + dy <= range + range / 2;
    }

    public Vector2D Randomize(int range)
    {
        return new Vector2D(X + Random.Next(-range, range), Y + Random.Next(-range, range));
    }

    public bool Equals(Vector2D other) => other.X == X && other.Y == Y;

    public override string ToString() => $"{X}/{Y}";
}
}