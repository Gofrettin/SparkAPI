namespace Spark.Network.Encoder
{
    public interface IEncoder
    {
        byte[] Encode(string packet);
    }
}