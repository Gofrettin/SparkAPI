using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Spark.Network.Encoder
{
    public class LoginEncoder : MessageToMessageEncoder<string>
    {
        protected override void Encode(IChannelHandlerContext context, string message, List<object> output)
        {
            var bytes = new byte[message.Length + 1];
            for (int i = 0; i < message.Length; i++)
            {
                bytes[i] = (byte)((message[i] ^ 0xC3) + 0xF);
            }

            bytes[^1] = 0xD8;

            output.Add(Unpooled.WrappedBuffer(bytes));
        }
    }
}