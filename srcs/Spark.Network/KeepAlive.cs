using System.Timers;
using DotNetty.Transport.Channels;

namespace Spark.Network
{
    public class KeepAlive : ChannelDuplexHandler
    {
        private readonly Timer _timer;

        public KeepAlive()
        {
            Identity = 0;
            _timer = new Timer
            {
                Interval = 60000
            };
            _timer.Elapsed += (obj, e) =>
            {
                Identity++;
                Channel.WriteAndFlushAsync($"pulse {Identity * 60} 0");
            };
        }

        public IChannel Channel { get; private set; }
        public int Identity { get; private set; }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);

            Channel = context.Channel;
            _timer.Start();
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);

            _timer.Stop();
        }
    }
}