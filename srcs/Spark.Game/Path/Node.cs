using Spark.Core;

namespace Spark.Game.Path
{
    public class Node
    {
        public Node Parent { get; set; }
        public Vector2D Position { get; set; }
        public float Distance { get; set; }
        public float Cost { get; set; }

        public float F
        {
            get
            {
                if (Distance != -1 && Cost != -1)
                {
                    return Distance + Cost;
                }

                return -1;
            }
        }
        
        public bool IsWalkable { get; set; }

        public Node(Vector2D position, bool isWalkable)
        {
            Position = position;
            IsWalkable = isWalkable;
            Distance = -1;
            Cost = -1;
        }
    }
}