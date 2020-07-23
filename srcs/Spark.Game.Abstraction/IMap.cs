using System.Collections.Generic;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction.Entities;

namespace Spark.Game.Abstraction
{
    public interface IMap
    {
        int Id { get; }
        string NameKey { get; }
        byte[] Grid { get; }
        int Height { get; }
        int Width { get; }
        
        IPathfinder Pathfinder { get; }

        IEnumerable<IMonster> Monsters { get; }
        IEnumerable<IPlayer> Players { get; }
        IEnumerable<INpc> Npcs { get; }
        IEnumerable<IMapObject> Objects { get; }

        IEnumerable<IPortal> Portals { get; }

        IEnumerable<IEntity> Entities { get; }

        IEntity GetEntity(EntityType entityType, long id);
        T GetEntity<T>(EntityType entityType, long id) where T : IEntity;

        void AddEntity(IEntity entity);
        void RemoveEntity(IEntity entity);

        void AddPortal(IPortal portal);

        bool IsWalkable(Vector2D vector2D);

        Vector2D FindTopDensityPosition();
        Vector2D GetRandomPosition(Vector2D point, int max = 1);
    }
}