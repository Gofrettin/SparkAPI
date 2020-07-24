using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark.Network.Decoder
{
    public class WorldDecoder : IDecoder
    {
        private static readonly char[] Keys = { ' ', '-', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'n', };

        public IEnumerable<string> Decode(byte[] bytes, int size)
        {
            int index = 0;
            var output = new List<string>();
            string currentPacket = string.Empty;
            
            while (index < size)
            {
                byte currentByte = bytes[index++];
                if (currentByte == 0xFF)
                {
                    output.Add(currentPacket.Trim());
                    currentPacket = string.Empty;
                    continue;
                }

                int length = currentByte & 0x7F;
                if ((currentByte & 0x80) != 0)
                {
                    while (length != 0)
                    {
                        if (index <= size)
                        {
                            currentByte = bytes[index++];

                            uint firstIndex = ((currentByte & 0xF0U) >> 4) - 1;
                            if (firstIndex < Keys.Length)
                            {
                                char c = Keys[firstIndex];
                                if (c != 0x6E)
                                {
                                    currentPacket += c;
                                }
                            }

                            if (length <= 1)
                            {
                                break;
                            }

                            uint secondIndex = (currentByte & 0xFU) - 1;
                            if (secondIndex < Keys.Length)
                            {
                                char c = Keys[secondIndex];
                                if (c != 0x6E)
                                {
                                    currentPacket += c;
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
                            currentPacket += (char)(bytes[index] ^ 0xFF);
                            index++;
                        }

                        length--;
                    }
                }
            }
            return output;
        }
    }
}