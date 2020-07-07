namespace Spark.Game.Abstraction.Entities
{
    public interface ICharacter : IPlayer
    {
        IClient Client { get; }
    }
}