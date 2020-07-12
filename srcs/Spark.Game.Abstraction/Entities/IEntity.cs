using System;
using Spark.Core;
using Spark.Core.Enum;

namespace Spark.Game.Abstraction.Entities
{
    public interface IEntity : IEquatable<IEntity>
    {
        long Id { get; }
        EntityType EntityType { get; }
        string Name { get; set; }
        IMap Map { get; set; }
        Vector2D Position { get; set; }
    }
}