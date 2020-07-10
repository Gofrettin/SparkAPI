using System;
using System.Collections.Generic;
using System.Text;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using NLog;

namespace Spark.Network.Decoder
{
    public class WorldDecoder : ByteToMessageDecoder
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private static readonly char[] Keys = { ' ', '-', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'n' };

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            var buffer = new byte[input.ReadableBytes];

            input.ReadBytes(buffer);

            int index = 0;
            string packet = string.Empty;

            while (index < buffer.Length)
            {
                byte b = buffer[index];
                index++;
                
                if (b == 0xFF)
                {
                    output.Add(packet.Trim());
                    packet = string.Empty;
                    continue;
                }

                int length = b & 0x7F;

                if ((b & 0x80) != 0)
                {
                    while (length != 0)
                    {
                        if (index < buffer.Length)
                        {
                            b = buffer[index];
                            index++;
                            
                            int firstIndex = ((b & 0xF0) >> 4) - 1;
                            if (firstIndex >= 0 && firstIndex < Keys.Length)
                            {
                                int first = Keys[firstIndex];
                                if (first != 0x6E)
                                {
                                    packet += Convert.ToChar(first);
                                }
                            }

                            if (length <= 1)
                            {
                                break;
                            }

                            int secondIndex = (b & 0xF) - 1;
                            if (secondIndex >= 0 && secondIndex < Keys.Length)
                            {
                                int second = Keys[secondIndex];
                                if (second != 0x6E)
                                {
                                    packet += Convert.ToChar(second);
                                }
                            }

                            length -= 2;
                        }
                        else
                        {
                            length--;
                        }
                    }
                }
                else
                {
                    while (length != 0)
                    {
                        if (index < buffer.Length)
                        {
                            packet += Convert.ToChar(buffer[index] ^ 0xFF);
                            index++;
                        }

                        length--;
                    }
                }
            }
        }
    }
}