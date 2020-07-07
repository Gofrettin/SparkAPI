using System.Net;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using Spark.Network.Decoder;
using Spark.Network.Encoder;

namespace Spark.Network.Session
{
    public interface ISessionFactory
    {
        Task<ISession> CreateSession(IPEndPoint ip);
        Task<ISession> CreateSession(IPEndPoint ip, int encryptionKey);
    }
    
    public class SessionFactory : ISessionFactory
    {
        public async Task<ISession> CreateSession(IPEndPoint ip)
        {
            var session = new RemoteSession();
            Bootstrap bootstrap = new Bootstrap()
                .Channel<TcpSocketChannel>()
                .Group(new MultithreadEventLoopGroup())
                .Handler(new ActionChannelInitializer<IChannel>(x =>
                {
                    IChannelPipeline pipeline = x.Pipeline;

                    pipeline.AddLast(new LoginDecoder());
                    pipeline.AddLast(session);
                    pipeline.AddLast(new LoginEncoder());
                }));

            await bootstrap.ConnectAsync(ip);

            return session;
        }

        public async Task<ISession> CreateSession(IPEndPoint ip, int encryptionKey)
        {
            var session = new RemoteSession();
            Bootstrap bootstrap = new Bootstrap()
                .Channel<TcpSocketChannel>()
                .Group(new MultithreadEventLoopGroup())
                .Handler(new ActionChannelInitializer<IChannel>(x =>
                {
                    IChannelPipeline pipeline = x.Pipeline;

                    pipeline.AddLast(new KeepAlive());
                    pipeline.AddLast(new WorldDecoder());
                    pipeline.AddLast(session);
                    pipeline.AddLast(new WorldEncoder(encryptionKey));
                    pipeline.AddLast(new PacketFormatter());
                }));
            
            await bootstrap.ConnectAsync(ip);

            return session;
        }
    }
}