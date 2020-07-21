using System.Linq;
using System.Threading.Tasks;
using NLog;
using Spark.Core.Option;
using Spark.Core.Server;
using Spark.Game.Abstraction;
using Spark.Network;
using Spark.Packet.Login;

namespace Spark.Processor.Login
{
    public class NsTeSTProcessor : PacketProcessor<NsTeST>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly INetworkFactory _networkFactory;

        public NsTeSTProcessor(INetworkFactory networkFactory) => _networkFactory = networkFactory;

        protected override void Process(IClient client, NsTeST packet)
        {
            LoginOption option = client.GetOption<LoginOption>();
            WorldServer server = packet.Servers.FirstOrDefault(x => option.ServerSelector.Invoke(x));
            if (server == null)
            {
                Logger.Error("Can't found world server");
                return;
            }
            
            client.Network.Close();
            client.Network = _networkFactory.CreateRemoteNetwork(server.Ip, packet.EncryptionKey);

            client.SendPacket($"{packet.EncryptionKey}");
            Task.Delay(1000).ContinueWith(s =>
            {
                client.SendPacket($"{packet.Name} GF 2");
                client.SendPacket("thisifgamemode");
            });
        }
    }
}