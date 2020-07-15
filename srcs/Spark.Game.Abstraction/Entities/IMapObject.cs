namespace Spark.Game.Abstraction.Entities
{
    public interface IMapObject : IEntity
    {
        int ItemKey { get; }
        int Amount { get; }
    }
}