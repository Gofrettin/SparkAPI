namespace Spark.Database.Data
{
    public class SkillData
    {
        public static readonly SkillData Undefined = new SkillData
        {
            NameKey = "UNDEFINED",
        };
        
        public string NameKey { get; set; }
    }
}