using System.Collections.Generic;
using System.Linq;
using Spark.Core;
using Spark.Game.Abstraction;
using Spark.Pathfinder;
using Spark.Pathfinder.Grid;

namespace Spark.Game
{
    public class Pathfinder : IPathfinder
    {
        private readonly IMap map;
        private readonly bool[][] matrix;
        
        public Pathfinder(IMap map)
        {
            this.map = map;
            matrix = new bool [map.Width][];
            
            for (int i = 0; i < map.Width; i++)
            {
                matrix[i] = new bool[map.Height];
                for (int j = 0; j < map.Height; j++)
                {
                    bool walkable = map.IsWalkable(new Vector2D(i, j));
                    matrix[i][j] = walkable;
                }
            }
        }
        
        public IEnumerable<Vector2D> Find(Vector2D origin, Vector2D destination)
        {
            BaseGrid searchGrid = new StaticGrid(map.Width, map.Height, matrix);
            var jp = new JumpPointParam(searchGrid, new GridPos(origin.X, origin.Y), new GridPos(destination.X, destination.Y), EndNodeUnWalkableTreatment.Allow);

            return JumpPointFinder.FindPath(jp).Select(x => new Vector2D(x.X, x.Y));
        }
    }
}