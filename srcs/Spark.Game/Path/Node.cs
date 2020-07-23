using Spark.Core;

namespace Spark.Game.Path
{
    public class Node
    {
        public Vector2D Position { get; set; }
        public int? Parent { get; set; }
        public int Id { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }

        public Node(int id, int? parent, Vector2D position, int g, int h)
        {
            Position = position;
            Parent = parent;
            Id = id;
            G = g;
            H = h;
            F = G + H;
        }
    }
}