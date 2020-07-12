namespace Spark.Game.Abstraction.Entities
{
    public interface INpc : ILivingEntity
    {
        int GameId { get; }
    }
}