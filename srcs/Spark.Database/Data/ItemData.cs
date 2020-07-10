namespace Spark.Database.Data
{
    public class ItemData
    {
        public static readonly ItemData Undefined = new ItemData
        {
            NameKey = "UNDEFINED",
        };
        public string NameKey { get; set; }
    }
}