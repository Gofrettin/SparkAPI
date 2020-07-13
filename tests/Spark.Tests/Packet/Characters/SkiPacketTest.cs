using Spark.Packet.Characters;

namespace Spark.Tests.Packet.Characters
{
    public class SkiPacketTest : PacketTest<Ski>
    {
        protected override string Packet { get; } = "ski 240 241 240 241 242 243 244 245 247 248 249 250 251 252 254 256 236 694|4 704|0 279 280 281 282 283";

        protected override Ski Excepted { get; } = new Ski
        {
            Skills = { 240, 241, 242, 243, 244, 245, 247, 248, 249, 250, 251, 252, 254, 256, 236, 694, 704, 279, 280, 281, 282, 283 }
        };
    }
}