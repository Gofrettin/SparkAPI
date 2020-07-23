using System;
using System.Collections.Generic;
using System.Linq;
using Spark.Core;
using Spark.Game.Abstraction;

namespace Spark.Game.Path
{
    public class AStar : IPathfinder
    {
        public Node[,] Grid { get; }

        public int Rows => Grid.GetLength(0);
        public int Columns => Grid.GetLength(1);

        public AStar(IMap map)
        {
            Grid = new Node[map.Height, map.Width];
            for (int i = 0; i < map.Height; i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    Grid[i, j] = new Node(new Vector2D(i, j), map.IsWalkable(new Vector2D(i, j)));
                }
            }
        }
        
        public Stack<Vector2D> Find(Vector2D origin, Vector2D destination)
        {
            var start = new Node(origin, true);
            var end = new Node(destination, true);
            
            var path = new Stack<Vector2D>();
            
            var open = new List<Node>();
            var closed = new List<Node>();

            Node current = start;
            
            open.Add(start);

            while (open.Count != 00 && !closed.Exists(x => x.Position.Equals(end.Position)))
            {
                current = open[0];
                open.Remove(current);
                closed.Add(current);
                
                IEnumerable<Node> adjacent = GetAdjacentNodes(current);
                foreach (Node n in adjacent.Where(n => !closed.Contains(n) && n.IsWalkable).Where(n => !open.Contains(n)))
                {
                    n.Parent = current;
                    n.Distance = Math.Abs(n.Position.X - end.Position.X) + Math.Abs(n.Position.Y - end.Position.Y);
                    n.Cost = 1 + n.Parent.Cost;
                            
                    open.Add(n);
                    open = open.OrderBy(node => node.F).ToList();
                }
            }

            if (!closed.Exists(x => x.Position.Equals(end.Position)))
            {
                return null;
            }

            Node temp = closed[closed.IndexOf(current)];
            while (temp.Parent != start && temp != null)
            {
                path.Push(temp.Position);
                temp = temp.Parent;
            }

            return path;
        }
        
        private IEnumerable<Node> GetAdjacentNodes(Node n)
        {
            var temp = new List<Node>();

            int row = n.Position.Y;
            int col = n.Position.X;

            if(row + 1 < Rows)
            {
                temp.Add(Grid[col, row + 1]);
            }
            if(row - 1 >= 0)
            {
                temp.Add(Grid[col, row - 1]);
            }
            if(col - 1 >= 0)
            {
                temp.Add(Grid[col - 1, row]);
            }
            if(col + 1 < Columns)
            {
                temp.Add(Grid[col + 1, row]);
            }

            return temp;
        }
    }
}