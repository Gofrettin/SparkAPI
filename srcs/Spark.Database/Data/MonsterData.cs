namespace Spark.Database.Data
{
    public class MonsterData
    {
        public static readonly MonsterData Undefined = new MonsterData
        {
            NameKey = "UNDEFINED",
            Level = 1
        };
        
        public string NameKey { get; set; }
        public int Level { get; set; }
    }
}