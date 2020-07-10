using System.Collections.Generic;
using System.Linq;
using NLog;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Game
{
    public sealed class Map : IMap
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<long, IMonster> _monsters;
        private readonly Dictionary<long, INpc> _npcs;
        private readonly Dictionary<long, IMapObject> _objects;
        private readonly Dictionary<long, IPlayer> _players;

        public Map(int id, MapData data)
        {
            Id = id;
            NameKey = data.NameKey;
            Grid = data.Grid;

            _monsters = new Dictionary<long, IMonster>();
            _npcs = new Dictionary<long, INpc>();
            _players = new Dictionary<long, IPlayer>();
            _objects = new Dictionary<long, IMapObject>();
        }

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
    }
}