using NLog;
using Spark.Core.Enum;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Abstraction.Factory;
using Spark.Game.Abstraction.Inventory;
using Spark.Packet.Inventory;

namespace Spark.Processor.Inventory
{
    public class InvProcessor : PacketProcessor<Inv>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private readonly IObjectFactory _objectFactory;

        public InvProcessor(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
        }
        
        protected override void Process(IClient client, Inv packet)
        {
            ICharacter character = client.Character;
            BagType bagType = packet.BagType;
            
            foreach (ObjectInfo objectInfo in packet.Objects)
            {
                IObjectStack objectStack = _objectFactory.CreateObjectStack(bagType, objectInfo.ObjectKey, objectInfo.Slot, objectInfo.Amount);
                if (objectStack == null)
                {
                    continue;
                }
                
                character.Inventory.AddObject(objectStack);
            }

            Logger.Debug($"Inventory {bagType} successfully initialized");
            if (bagType == BagType.Costume)
            {
                Logger.Info($"{character.Name} inventory successfully initialized");
            }
        }
    }
}