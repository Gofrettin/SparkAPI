using Spark.Game.Abstraction.Battle;

namespace Spark.Game.Battle
{
    public class Buff : IBuff
    {
        public long Id { get; }
        public long Duration { get; }

        public Buff(long id, long duration)
        {
            Id = id;
            Duration = duration;
        }
    }
}