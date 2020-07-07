using System;
using Spark.Database;
using Spark.Database.Data;

namespace Spark.Game.Factory.Impl
{
    public class MapFactory : IMapFactory
    {
        private readonly IGameDataProvider _gameDataProvider;

        public MapFactory(IGameDataProvider gameDataProvider) => _gameDataProvider = gameDataProvider;

        public Map CreateMap(int mapId)
        {
            MapData mapData = _gameDataProvider.GetMapData(mapId);
            if (mapData == null)
            {
                throw new InvalidOperationException($"Can't found map data for map with id {mapId}");
            }

            return new Map(mapId, mapData.NameKey, mapData.Grid);
        }
    }
}