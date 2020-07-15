namespace Spark.Core.Enum
{
    /// <summary>
    ///     Represent a skill hit type
    /// </summary>
    public enum HitType
    {
        Target = 0,

        /// <summary>
        ///     Hit all enemies in AOE
        /// </summary>
        Enemies = 1,

        /// <summary>
        ///     Hit all allies in AOE
        /// </summary>
        Allies = 2,
        Special = 3
    }
}