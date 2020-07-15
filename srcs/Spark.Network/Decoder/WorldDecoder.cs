using System;
using System.Collections.Generic;

namespace Spark.Network.Decoder
{
    public class WorldDecoder : IDecoder
    {
        private static readonly char[] Keys = { ' ', '-', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'n' };

        public IEnumerable<string> Decode(byte[] buffer, int size)
        {
            var packets = new List<string>();

            int index = 0;
            string packet = string.Empty;

            while (index <= size)
            {
                byte b = buffer[index];
                index++;

                if (b == 0xFF)
                {
                    packets.Add(packet.Trim());
                    packet = string.Empty;
                    continue;
                }

                int length = b & 0x7F;

                if ((b & 0x80) != 0)
                {
                    while (length > 0)
                    {
                        if (index <= size)
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
                        if (index <= size)
                        {
                            packet += Convert.ToChar(buffer[index] ^ 0xFF);
                            index++;
                        }

                        length--;
                    }
                }
            }

            return packets;
        }
    }
}