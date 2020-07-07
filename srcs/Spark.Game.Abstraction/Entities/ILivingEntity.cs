namespace Spark.Game.Abstraction.Entities
{
    public interface ILivingEntity : IEntity
    {
        int Hp { get; set; }
        int Mp { get; set; }

        bool IsAlive => Hp > 0;
    }
}