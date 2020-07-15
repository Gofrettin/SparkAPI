using Spark.Game.Abstraction.Battle;
using Spark.Game.Abstraction.Factory;
using Spark.Game.Battle;

namespace Spark.Game.Factory
{
    public class BuffFactory : IBuffFactory
    {
        public IBuff CreateBuff(long id, long duration)
        {
            return new Buff(id, duration);
        }
    }
}