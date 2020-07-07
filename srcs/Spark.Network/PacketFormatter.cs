using System;
using System.Collections.Generic;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Spark.Network
{
    public class PacketFormatter : MessageToMessageEncoder<string>
    {
        private static readonly Random Random = new Random();

        public PacketFormatter() => PacketId = Random.Next(30000, 50000);

        public int PacketId { get; private set; }

        protected override void Encode(IChannelHandlerContext context, string message, List<object> output)
        {
            output.Add($"{++PacketId} {message}");
        }
    }
}