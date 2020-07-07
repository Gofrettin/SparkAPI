namespace Spark.Game.Entities
{
    public class Character : Player
    {
        public Character(long id, IClient client) : base(id) => Client = client;

        public IClient Client { get; }
    }
}