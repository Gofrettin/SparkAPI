using Spark.Game.Abstraction.Battle;

namespace Spark.Game.Abstraction.Factory
{
    public interface IBuffFactory
    {
        IBuff CreateBuff(long id, long duration);
    }
}