using System;
using System.Collections.Generic;
using DotNetty.Transport.Channels;
using NLog;
using Spark.Core;
using Spark.Core.Extension;
using Spark.Game;
using Spark.Game.Entities;

namespace Spark.Network.Client.Impl
{
    public class RemoteClient : ChannelDuplexHandler, IClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public RemoteClient()
        {
            Id = Guid.NewGuid();
            SelectableCharacters = new List<SelectableCharacter>();
        }

        public IChannel Channel { get; private set; }
        public string Name { get; set; }
        public List<SelectableCharacter> SelectableCharacters { get; }

        public Guid Id { get; }
        public Character Character { get; set; }
        public event Action<string> PacketReceived;

        public void SendPacket(string packet)
        {
            Channel.WriteAndFlushAsync(packet).OnException(x => { Logger.Error(x.InnerException); });
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            Channel = context.Channel;
            Logger.Info($"Channel with id {Id} is now active");
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            Logger.Info($"Channel with id {Id} is now inactive");
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            if (!(message is string packet))
            {
                Logger.Warn("Received a non string message");
                return;
            }

            packet = packet.Trim();

            PacketReceived?.Invoke(packet);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Logger.Error(exception);
        }
    }
}