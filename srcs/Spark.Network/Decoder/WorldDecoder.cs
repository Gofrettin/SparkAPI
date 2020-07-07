using System;
using System.Collections.Generic;
using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;

namespace Spark.Network.Decoder
{
    public class WorldDecoder : ByteToMessageDecoder
    {
        private static readonly char[] Keys = { ' ', '-', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'n' };

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            if (!input.IsReadable())
            {
                return;
            }
            
            var buffer = new byte[input.ReadableBytes];
            input.ReadBytes(buffer);

            string currentPacket = "";
            int index = 0;

            while (index < buffer.Length)
            {
                byte currentByte = buffer[index];
                index++;

                if (currentByte == 0xFF)
                {
                    output.Add(currentPacket);
                    currentPacket = "";
                    continue;
                }

                byte length = (byte)(currentByte & 0x7F);

                if ((currentByte & 0x80) != 0)
                {
                    while (length != 0)
                    {
                        if (index < buffer.Length)
                        {
                            currentByte = buffer[index];
                            index++;

                            byte firstIndex = (byte)(((currentByte & 0xF0u) >> 4) - 1);
                            byte first = (byte)(firstIndex != 255 ? firstIndex != 14 ? Keys[firstIndex] : '\u0000' : '?');
                            if (first != 0x6E)
                            {
                                currentPacket += Convert.ToChar(first);
                            }

                            if (length <= 1)
                            {
                                break;
                            }

                            byte secondIndex = (byte)((currentByte & 0xF) - 1);
                            byte second = (byte)(secondIndex != 255 ? secondIndex != 14 ? Keys[secondIndex] : '\u0000' : '?');
                            if (second != 0x6E)
                            {
                                currentPacket += Convert.ToChar(second);
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
                            currentPacket += Convert.ToChar(buffer[index] ^ 0xFF);
                            index++;
                        }
                        else if (index == buffer.Length)
                        {
                            currentPacket += Convert.ToChar(0xFF);
                            index++;
                        }

                        length--;
                    }
                }
            }
        }
    }
}