using System.Collections.Generic;
using System.Linq;
using NLog.LayoutRenderers.Wrappers;
using Spark.Core;
using Spark.Game.Abstraction;

namespace Spark.Game.Path
{
    public class BreadthFirst : IPathfinder
    {
        private readonly IMap _map;
        
        public BreadthFirst(IMap map)
        {
            _map = map;
        }
        
        public IEnumerable<Vector2D> Find(Vector2D origin, Vector2D destination)
        {
            int id = 0;
            var queue = new Queue<Node>();
            var closed = new List<Node>();
            bool found = false;
            
            queue.Enqueue(new Node(id++, null, origin, 0, 0));
            while (true)
            {
                Node current;
                if (queue.Count > 0 && !found)
                {
                    current = queue.Dequeue();
                    if (closed.Any(x => x.Position.Equals(current.Position)))
                    {
                        continue;
                    }
                    
                    closed.Add(current);

                    IEnumerable<Vector2D> neighbours = GetNeighbours(current.Position);
                    foreach (Vector2D neighbour in neighbours)
                    {
                        if (closed.Any(x => x.Position.Equals(neighbour))) continue;
                        
                        var node = new Node(id++, current.Id, neighbour, 0, 0);
                        queue.Enqueue(node);

                        if (!neighbour.Equals(destination))
                        {
                            continue;
                        }
                        
                        closed.Add(node);
                        found = true;
                    }
                }
                else
                {
                    var path = new List<Vector2D>();
                    Node step = closed.First(x => x.Position.Equals(destination));

                    while (!step.Position.Equals(origin))
                    {
                        path.Add(step.Position);
                        step = closed.First(x => x.Id == step.Parent);
                    }
                    
                    path.Add(origin);
                    path.Reverse();

                    return path;
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