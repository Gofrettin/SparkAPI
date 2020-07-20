using NLog;
using Spark.Database;
using Spark.Database.Data;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;

namespace Spark.Game.Factory
{
    public class MapFactory : IMapFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IDatabase _database;

        public MapFactory(IDatabase database) => _database = database;

        public IMap CreateMap(int mapId)
        {
            MapData data = _database.Maps.GetValue(mapId);
            if (data == null)
            {
                Logger.Error($"Can't get map data with id {mapId} from database");
                return default;
            }

            var map = new Map(mapId, data);
            
            Logger.Info($"Map {map.Id} created ({map.Height} / {map.Width})");
            return map;
        }
    }
}