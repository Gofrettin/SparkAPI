using System;
using System.Collections.Generic;
using System.Linq;
using Spark.Core;
using Spark.Game.Abstraction;

namespace Spark.Game.Path
{
    public class AStar : IPathfinder
    {
        private readonly IMap _map;

        public AStar(IMap map)
        {
            _map = map;
        }
        
        public IEnumerable<Vector2D> Find(Vector2D origin, Vector2D destination)
        {
            int id = 0;
            var neighbours = new List<Vector2D>();
            var closed = new List<Node>();
            var opens = new List<Node>
            {
                new Node(id++, null, origin, 0, GetH(origin, destination))
            };
            
            Node current = null;
            while (true)
            {
                if (current == null)
                {
                    if (!opens.Any())
                    {
                        continue;
                    }

                    current = opens.OrderBy(x => x.F).ThenBy(x => x.H).First();

                    opens.Remove(current);
                    closed.Add(current);
                    
                    neighbours.AddRange(GetNeighbours(current.Position));
                }

                if (neighbours.Any())
                {
                    Vector2D neighbour = neighbours.First();
                    neighbours.Remove(neighbour);

                    if (neighbour.Equals(destination))
                    {
                        var path = new List<Vector2D>()
                        {
                            neighbour
                        };

                        int? parent = current.Id;
                        while (parent.HasValue)
                        {
                            Node next = closed.First(x => x.Id == parent);
                            path.Add(next.Position);
                            parent = next.Parent;
                        }

                        path.Reverse();
                        return path;
                    }
                    
                    int hFromHere = GetH(neighbour, destination);
                    int neighbourCost = current.G + hFromHere;
                    
                    Node openListItem = opens.FirstOrDefault(x => x.Id == GetExistingNode(opens, closed, true, neighbour));
                    if (openListItem != null && openListItem.F > neighbourCost)
                    {
                        openListItem.F = neighbourCost;
                        openListItem.Parent = current.Id;
                    }
                    
                    Node closedListItem = closed.FirstOrDefault(x => x.Id == GetExistingNode(opens, closed, false, neighbour));
                    if (closedListItem != null && closedListItem.F > neighbourCost)
                    {
                        closedListItem.F = neighbourCost;
                        closedListItem.Parent = current.Id;
                    }
                    
                    if (openListItem != null || closedListItem != null) continue;
                    opens.Add(new Node(id++, current.Id, neighbour, current.G, hFromHere));
                }
                else
                {
                    current = null;
                }
            }
        }
        
        private int? GetExistingNode(IEnumerable<Node> opens, IEnumerable<Node> closed, bool checkOpenList, Vector2D position)
        {
            return checkOpenList ? opens.FirstOrDefault(x => x.Position.Equals(position))?.Id : closed.FirstOrDefault(x => x.Position.Equals(position))?.Id;
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

        private int GetH(Vector2D origin, Vector2D destination)
        {
            return Math.Abs(origin.X - destination.X) + Math.Abs(origin.Y - destination.Y);
        }
    }
}