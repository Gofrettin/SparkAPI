using System.Collections.Generic;

namespace Spark.Network.Decoder
{
    public interface IDecoder
    {
        IEnumerable<string> Decode(byte[] bytes, int size);
    }
}