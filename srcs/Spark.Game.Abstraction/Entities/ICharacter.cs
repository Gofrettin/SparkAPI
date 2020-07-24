using System.Collections.Generic;
using System.Threading.Tasks;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction.Inventory;

namespace Spark.Game.Abstraction.Entities
{
    public interface ICharacter : IPlayer
    {
        IClient Client { get; }
        ICharacterInventory Inventory { get; }
        IEnumerable<ISkill> Skills { get; set; }

        int Hp { get; set; }
        int Mp { get; set; }
        int MaxHp { get; set; }
        int MaxMp { get; set; }

        void WearSpecialist();
        void UnwearSpecialist();
        void Walk(Vector2D position);
        void Walk(IEnumerable<Vector2D> path);
        void WalkInRange(Vector2D position, int range);
        void Attack(ISkill skill);
        void Attack(ILivingEntity entity);
        void Attack(ISkill skill, ILivingEntity entity);
        void Rotate(Direction direction);
        void PickUp(IMapObject mapObject);
        void UseObject(IObjectStack objectStack);
        void UseObject(int objectKey);
        void Rest();
    }
}