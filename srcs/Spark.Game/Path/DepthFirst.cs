using System.Collections.Generic;
using System.Linq;
using Spark.Core;
using Spark.Game.Abstraction;

namespace Spark.Game.Path
{
    public class DepthFirst : IPathfinder
    {
        private readonly IMap _map;
        
        public DepthFirst(IMap map)
        {
            _map = map;
        }

        public IEnumerable<Vector2D> Find(Vector2D origin, Vector2D destination)
        {
            var closed = new List<Vector2D>();
            var stack = new Stack<Vector2D>();

            stack.Push(origin);

            while (true)
            {
                Vector2D current = stack.Peek();
                if (current.Equals(destination))
                {
                    var path = stack.ToList();
                    path.Reverse();
                    return path;
                }

                IEnumerable<Vector2D> neighbours = GetNeighbours(current).Where(s => !stack.Any(x => x.Equals(s)) && !closed.Any(x => x.Equals(s)));
                if (neighbours.Any())
                { 
                    stack.Push(neighbours.First());
                }
                else
                { 
                    closed.Add(stack.Pop());
                }
            }
        }

        private IEnumerable<Vector2D> GetNeighbours(Vector2D current)
        {
            var neighbours = new List<Vector2D>
            {
                new Vector2D(current.X - 1, current.Y),
                new Vector2D(current.X + 1, current.Y),
                new Vector2D(current.X, current.Y - 1),
                new Vector2D(current.X, current.Y + 1)
            };

            return neighbours.Where(x => _map.IsWalkable(x));
        }
    }
}