using System.Collections.Generic;
using Spark.Core;

namespace Spark.Game.Abstraction
{
    public interface IPathfinder
    {
        IEnumerable<Vector2D> Find(Vector2D origin, Vector2D destination);
    }
}