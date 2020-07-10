using System;
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

        public MapFactory(IDatabase database)
        {
            _database = database;
        }

        public IMap CreateMap(int mapId)
        {
            MapData data = _database.Maps.GetValue(mapId);
            if (data == null)
            {
                Logger.Error($"Can't get map data with id {mapId} from database");
                data = MapData.Undefined;
            }

            return new Map(mapId, data.NameKey, data.Grid);
        }
    }
}