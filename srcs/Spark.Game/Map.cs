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

        private readonly IDictionary<long, IMonster> _monsters;
        private readonly IDictionary<long, INpc> _npcs;
        private readonly IDictionary<long, IMapObject> _objects;
        private readonly IDictionary<long, IPlayer> _players;
        private readonly IDictionary<int, IPortal> _portals;
        
        public Map(int id, MapData data)
        {
            Id = id;
            NameKey = data.NameKey;
            Grid = data.Grid;
            Height = BitConverter.ToInt16(Grid.Slice(0, 2));
            Width = BitConverter.ToInt16(Grid.Slice(2, 2));

            _monsters = new ConcurrentDictionary<long, IMonster>();
            _npcs = new ConcurrentDictionary<long, INpc>();
            _players = new ConcurrentDictionary<long, IPlayer>();
            _objects = new ConcurrentDictionary<long, IMapObject>();
            _portals = new ConcurrentDictionary<int, IPortal>();
        }

        public IEnumerable<IPortal> Portals => _portals.Values;

        public IEnumerable<IEntity> Entities => _monsters.Values
            .Concat<IEntity>(_npcs.Values)
            .Concat(_players.Values)
            .Concat(_objects.Values);

        public int Id { get; }
        public string NameKey { get; }
        public byte[] Grid { get; }
        public int Height { get; }
        public int Width { get; }

        public IEnumerable<IMonster> Monsters => _monsters.Values;
        public IEnumerable<IPlayer> Players => _players.Values;
        public IEnumerable<INpc> Npcs => _npcs.Values;
        public IEnumerable<IMapObject> Objects => _objects.Values;

        public IEntity GetEntity(EntityType entityType, long id)
        {
            switch (entityType)
            {
                case EntityType.Monster:
                    return _monsters.GetValueOrDefault(id);
                case EntityType.Npc:
                    return _npcs.GetValueOrDefault(id);
                case EntityType.MapObject:
                    return _objects.GetValueOrDefault(id);
                case EntityType.Player:
                    return _players.GetValueOrDefault(id);
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
                    _monsters[entity.Id] = (IMonster)entity;
                    break;
                case EntityType.Npc:
                    _npcs[entity.Id] = (INpc)entity;
                    break;
                case EntityType.MapObject:
                    _objects[entity.Id] = (IMapObject)entity;
                    break;
                case EntityType.Player:
                    _players[entity.Id] = (IPlayer)entity;
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
                    _monsters.Remove(entity.Id);
                    break;
                case EntityType.Npc:
                    _npcs.Remove(entity.Id);
                    break;
                case EntityType.MapObject:
                    _objects.Remove(entity.Id);
                    break;
                case EntityType.Player:
                    _players.Remove(entity.Id);
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
            _portals[portal.Id] = portal;
            Logger.Debug($"Portal {portal.Id} of type {portal.PortalType} added to map {Id}");
        }

        public bool IsWalkable(Vector2D vector2D)
        {
            if (vector2D.X > Height || vector2D.X < 0 || vector2D.Y > Width || vector2D.Y < 0)
            {
                return false;
            }

            byte b = Grid[4 + vector2D.Y * Height + vector2D.X];
            return b == 0 || b == 2 || (b >= 16 && b <= 19);
        }
    }
}