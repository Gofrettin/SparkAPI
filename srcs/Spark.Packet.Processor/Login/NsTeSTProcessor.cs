using System.Linq;
using System.Threading.Tasks;
using NLog;
using Spark.Core.Configuration;
using Spark.Core.Server;
using Spark.Game.Abstraction;
using Spark.Network;
using Spark.Packet.Login;

namespace Spark.Packet.Processor.Login
{
    public class NsTeStProcessor : PacketProcessor<NsTeSt>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly INetworkFactory networkFactory;

        public NsTeStProcessor(INetworkFactory networkFactory) => this.networkFactory = networkFactory;

        protected override void Process(IClient client, NsTeSt packet)
        {
            LoginConfiguration option = client.GetConfiguration<LoginConfiguration>();
            if (option == null)
            {
                return;
            }
            
            WorldServer server = packet.Servers.FirstOrDefault(x => option.ServerSelector.Invoke(x));
            if (server == null)
            {
                Logger.Error("Can't found world server");
                return;
            }
            
            client.Network.Close();
            client.Network = networkFactory.CreateRemoteNetwork(server.Ip, packet.EncryptionKey);

            client.SendPacket($"{packet.EncryptionKey}");
            Task.Delay(1000).ContinueWith(s =>
            {
                client.SendPacket($"{packet.Name} GF 2");
                client.SendPacket("thisifgamemode");
            });
        }
    }
}