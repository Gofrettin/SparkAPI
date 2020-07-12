namespace Spark.Game.Abstraction.Factory
{
    public interface ISkillFactory
    {
        ISkill CreateSkill(int gameId);
    }
}