using System.Collections.Generic;
using System.Linq;
using EpPathFinding.cs;
using Spark.Core;
using Spark.Game.Abstraction;

namespace Spark.Game
{
    public class Pathfinder : IPathfinder
    {
        private readonly IMap _map;
        private readonly bool[][] _matrix;
        
        public Pathfinder(IMap map)
        {
            _map = map;
            _matrix = new bool [map.Width][];
            
            for (int i = 0; i < map.Width; i++)
            {
                _matrix[i] = new bool[map.Height];
                for (int j = 0; j < map.Height; j++)
                {
                    bool walkable = map.IsWalkable(new Vector2D(i, j));
                    _matrix[i][j] = walkable;
                }
            }
        }
        
        public IEnumerable<Vector2D> Find(Vector2D origin, Vector2D destination)
        {
            BaseGrid searchGrid = new StaticGrid(_map.Width, _map.Height, _matrix);
            var jp = new JumpPointParam(searchGrid, new GridPos(origin.X, origin.Y), new GridPos(destination.X, destination.Y), EndNodeUnWalkableTreatment.ALLOW);

            return JumpPointFinder.GetFullPath(JumpPointFinder.FindPath(jp)).Select(x => new Vector2D(x.x, x.y));
        }
    }
}