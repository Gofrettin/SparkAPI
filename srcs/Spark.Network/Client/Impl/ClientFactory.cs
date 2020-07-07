using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Spark.Game;
using Spark.Network.Decoder;
using Spark.Network.Encoder;

namespace Spark.Network.Client.Impl
{
    public class ClientFactory : IClientFactory
    {
        public async Task<IClient> CreateClient(IPEndPoint server)
        {
            var client = new RemoteClient();
            Bootstrap bootstrap = new Bootstrap()
                .Channel<TcpSocketChannel>()
                .Group(new MultithreadEventLoopGroup())
                .Handler(new ActionChannelInitializer<IChannel>(x =>
                {
                    IChannelPipeline pipeline = x.Pipeline;

                    pipeline.AddLast(new LoginDecoder());
                    pipeline.AddLast(client);
                    pipeline.AddLast(new LoginEncoder());
                }));

            await bootstrap.ConnectAsync(server);


            return client;
        }

        public async Task<IClient> CreateClient(IPEndPoint server, string name, int encryptionKey)
        {
            var client = new RemoteClient
            {
                Name = name
            };

            Bootstrap bootstrap = new Bootstrap()
                .Channel<TcpSocketChannel>()
                .Group(new MultithreadEventLoopGroup())
                .Handler(new ActionChannelInitializer<IChannel>(x =>
                {
                    IChannelPipeline pipeline = x.Pipeline;

                    pipeline.AddLast(new KeepAlive());
                    pipeline.AddLast(new WorldDecoder());
                    pipeline.AddLast(client);
                    pipeline.AddLast(new WorldEncoder(encryptionKey));
                    pipeline.AddLast(new PacketFormatter());
                }));

            await bootstrap.ConnectAsync(server);

            return client;
        }

        public Task<IClient> CreateClient(Process process) => throw new NotImplementedException();
    }
}