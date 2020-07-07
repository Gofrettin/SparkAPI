using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Spark.Network.Decoder
{
    public class LoginDecoder : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            var buffer = new byte[input.ReadableBytes];
            input.ReadBytes(buffer);

            var packet = new StringBuilder();
            foreach (byte b in buffer)
            {
                packet.Append(Convert.ToChar(b - 0xF));
            }

            packet.Remove(packet.Length - 1, 1);
            output.Add(packet.ToString());
        }
    }
}