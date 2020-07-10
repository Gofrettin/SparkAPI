namespace Spark.Database.Data
{
    public class MapData
    {
        public static readonly MapData Undefined = new MapData
        {
            NameKey = "UNDEFINED",
            Grid = new byte[999]
        };

        public string NameKey { get; set; }
        public byte[] Grid { get; set; }
    }
}