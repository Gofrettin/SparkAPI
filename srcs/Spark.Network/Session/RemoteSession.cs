using System;
using DotNetty.Transport.Channels;
using NLog;
using Spark.Core.Extension;

namespace Spark.Network.Session
{
    public class RemoteSession : ChannelDuplexHandler, ISession
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public IChannel Channel { get; private set; }

        public event Action<string> PacketReceived;

        public void SendPacket(string packet)
        {
            Logger.Trace($"Out: {packet}");
            Channel.WriteAndFlushAsync(packet).OnException(x => { Logger.Error(x.InnerException); });
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            Channel = context.Channel;
            Logger.Info($"Channel {Channel.Id.AsShortText()} is now active");
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            Logger.Info($"Channel {Channel.Id.AsShortText()} is now inactive");
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (!(message is string packet))
            {
                Logger.Warn("Received a non string message");
                return;
            }

            packet = packet.Trim();

            Logger.Trace($"In: {packet}");
            PacketReceived?.Invoke(packet);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Logger.Error(exception);
        }
    }
}