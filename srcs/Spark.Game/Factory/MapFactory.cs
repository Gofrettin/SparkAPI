using System;
using Spark.Database;
using Spark.Database.Data;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;

namespace Spark.Game.Factory
{
    public class MapFactory : IMapFactory
    {
        private readonly IGameDataProvider _gameDataProvider;

        public MapFactory(IGameDataProvider gameDataProvider)
        {
            _gameDataProvider = gameDataProvider;
        }
        
        public IMap CreateMap(int mapId)
        {
            MapData data = _gameDataProvider.GetMapData(mapId);
            if (data == null)
            {
                throw new InvalidOperationException();
            }
            
            return new Map(mapId, data.NameKey, data.Grid);
        }
    }
}