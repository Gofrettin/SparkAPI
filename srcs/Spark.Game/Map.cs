using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Database.Data;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Game
{
    public sealed class Map : IMap
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IDictionary<long, IMonster> monsters;
        private readonly IDictionary<long, INpc> npcs;
        private readonly IDictionary<long, IMapObject> objects;
        private readonly IDictionary<long, IPlayer> players;
        private readonly IDictionary<int, IPortal> portals;

        public Map(int id, MapData data)
        {
            Id = id;
            NameKey = data.NameKey;
            Grid = data.Grid;
            Width = BitConverter.ToInt16(Grid.Slice(0, 2));
            Height = BitConverter.ToInt16(Grid.Slice(2, 2));
            Pathfinder = new Pathfinder(this);
            
            monsters = new ConcurrentDictionary<long, IMonster>();
            npcs = new ConcurrentDictionary<long, INpc>();
            players = new ConcurrentDictionary<long, IPlayer>();
            objects = new ConcurrentDictionary<long, IMapObject>();
            portals = new ConcurrentDictionary<int, IPortal>();
        }

        public IEnumerable<IPortal> Portals => portals.Values;

        public IEnumerable<IEntity> Entities => monsters.Values
            .Concat<IEntity>(npcs.Values)
            .Concat(players.Values)
            .Concat(objects.Values);

        public int Id { get; }
        public string NameKey { get; }
        public byte[] Grid { get; }
        public int Height { get; }
        public int Width { get; }
        public IPathfinder Pathfinder { get; }

        public IEnumerable<IMonster> Monsters => monsters.Values;
        public IEnumerable<IPlayer> Players => players.Values;
        public IEnumerable<INpc> Npcs => npcs.Values;
        public IEnumerable<IMapObject> Objects => objects.Values;

        public IEntity GetEntity(EntityType entityType, long id)
        {
            switch (entityType)
            {
                case EntityType.Monster:
                    return monsters.GetValueOrDefault(id);
                case EntityType.Npc:
                    return npcs.GetValueOrDefault(id);
                case EntityType.MapObject:
                    return objects.GetValueOrDefault(id);
                case EntityType.Player:
                    return players.GetValueOrDefault(id);
                default:
                    Logger.Warn($"Trying to get an invalid entity type {entityType}");
                    return default;
            }
        }

        public T GetEntity<T>(EntityType entityType, long id) where T : IEntity
        {
            IEntity entity = GetEntity(entityType, id);
            if (entity == null)
            {
                return default;
            }

            if (entity is T castedEntity)
            {
                return castedEntity;
            }

            Logger.Debug($"Entity {entityType} with id {id} is not of type {typeof(T).Name}");
            return default;
        }

        public void AddEntity(IEntity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.Monster:
                    monsters[entity.Id] = (IMonster)entity;
                    break;
                case EntityType.Npc:
                    npcs[entity.Id] = (INpc)entity;
                    break;
                case EntityType.MapObject:
                    objects[entity.Id] = (IMapObject)entity;
                    break;
                case EntityType.Player:
                    players[entity.Id] = (IPlayer)entity;
                    break;
                default:
                    Logger.Warn($"Unvalid entity type {entity.EntityType}");
                    return;
            }

            entity.Map = this;

            Logger.Debug($"Entity {entity.EntityType} with id {entity.Id} added to map {Id}");
        }

        public void RemoveEntity(IEntity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.Monster:
                    monsters.Remove(entity.Id);
                    break;
                case EntityType.Npc:
                    npcs.Remove(entity.Id);
                    break;
                case EntityType.MapObject:
                    objects.Remove(entity.Id);
                    break;
                case EntityType.Player:
                    players.Remove(entity.Id);
                    break;
                default:
                    Logger.Warn($"Unvalid entity type {entity.EntityType}");
                    return;
            }

            entity.Map = null;

            Logger.Debug($"Entity {entity.EntityType} with id {entity.Id} removed from map {Id}");
        }

        public void AddPortal(IPortal portal)
        {
            portals[portal.Id] = portal;
            Logger.Debug($"Portal {portal.Id} of type {portal.PortalType} added to map {Id}");
        }

        public bool IsWalkable(Vector2D vector2D)
        {
            if (vector2D.X > Width || vector2D.X < 0 || vector2D.Y > Height || vector2D.Y < 0)
            {
                return false;
            }

            byte b = Grid[4 + vector2D.Y * Width + vector2D.X];
            return b == 0 || b == 2 || (b >= 16 && b <= 19);
        }
        
        public Vector2D GetRandomPosition(Vector2D point, int max = 1)
        {
            Vector2D output;
            int attempt = 0;
            do
            {
                output = point.Randomize(max);
                attempt++;
            } 
            while (!IsWalkable(output) && attempt < max * 6);

            return attempt == (max * 6) ? point : output;
        }
    }
}