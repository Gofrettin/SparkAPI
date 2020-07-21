namespace Spark.Core.Enum
{
    /// <summary>
    ///     Represent a skill hit type
    /// </summary>
    public enum HitType
    {
        TargetOnly = 0,

        /// <summary>
        ///     Hit all enemies in AOE
        /// </summary>
        EnemiesInRange = 1,

        /// <summary>
        ///     Hit all allies in AOE
        /// </summary>
        AlliesInRange = 2,
        Special = 3
    }
}