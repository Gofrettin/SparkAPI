using System.Collections.Generic;
using System.Linq;
using NLog;
using Spark.Core.Enum;
using Spark.Game.Entities;

namespace Spark.Game
{
    public sealed class Map
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<long, Monster> _monsters;
        private readonly Dictionary<long, Npc> _npcs;
        private readonly Dictionary<long, MapObject> _objects;
        private readonly Dictionary<long, Player> _players;

        public Map(int id, string name, byte[] grid)
        {
            Id = id;
            Name = name;
            Grid = grid;

            _monsters = new Dictionary<long, Monster>();
            _npcs = new Dictionary<long, Npc>();
            _players = new Dictionary<long, Player>();
            _objects = new Dictionary<long, MapObject>();
        }

        public IEnumerable<Entity> Entities => _monsters.Values
            .Concat<Entity>(_npcs.Values)
            .Concat(_players.Values)
            .Concat(_objects.Values);

        public int Id { get; }
        public string Name { get; }
        public byte[] Grid { get; }
        public int Height { get; }
        public int Width { get; }

        public IEnumerable<Monster> Monsters => _monsters.Values;
        public IEnumerable<Player> Players => _players.Values;
        public IEnumerable<Npc> Npcs => _npcs.Values;
        public IEnumerable<MapObject> Objects => _objects.Values;

        public Entity GetEntity(EntityType entityType, long id)
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

        public void AddEntity(Entity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.Monster:
                    _monsters[entity.Id] = (Monster)entity;
                    break;
                case EntityType.Npc:
                    _npcs[entity.Id] = (Npc)entity;
                    break;
                case EntityType.MapObject:
                    _objects[entity.Id] = (MapObject)entity;
                    break;
                case EntityType.Player:
                    _players[entity.Id] = (Player)entity;
                    break;
                default:
                    Logger.Warn($"Unvalid entity type {entity.EntityType}");
                    return;
            }

            entity.Map = this;

            Logger.Debug($"Entity {entity.EntityType} with id {entity.Id} added to map {Id}");
        }

        public void RemoveEntity(Entity entity)
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